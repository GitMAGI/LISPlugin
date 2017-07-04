using GeneralPurposeLib;
using IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LISPlugin
{
    public class LIS : ILISPlugin.ILIS
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataAccessLayer.LISDAL dal;
        private BusinessLogicLayer.LISBLL bll;

        public LIS()
        {
            dal = new DataAccessLayer.LISDAL();
            bll = new BusinessLogicLayer.LISBLL(dal);
        }

        public string ScheduleNewRequest(EventoDTO even, PrestazioneDTO pres, RichiestaLISDTO esam, List<AnalisiDTO> anals, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string hl7_stato = IBLL.HL7StatesRichiestaLIS.Idle;
            string evenIdStr = null;
            string esamIdStr = null;
            string presIdStr = null;

            EventoDTO evenInserted = null;
            PrestazioneDTO presInserted = null;
            RichiestaLISDTO esamInserted = null;            
            List<AnalisiDTO> analsInserted = null;

            if (errorString == null)
                errorString = "";

            try
            {
                if (even == null || esam == null || anals == null || (anals != null && anals.Count == 0))
                    throw new Exception("Error! Request a null or void insertion of EVEN and/or ESAM and/or ANAL.");

                // Validation Even!!!!
                if (!bll.ValidateEven(even, ref errorString))
                {
                    string msg = "Validation Even Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                // Check if Epis exists!!!!
                log.Info(string.Format("Checking EPIS '{0}' exists ...", even.evenepis));
                EpisodioDTO epis = bll.GetEpisodioById(even.evenepis.ToString());
                if (epis == null)
                {
                    string msg = string.Format("EPIS Checking Failure! EPIS '{0}' not Found!", even.evenepis);
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }
                epis = null;
                log.Info(string.Format("Checking EPIS '{0}' succeded!", even.evenepis));

                // Create Even
                log.Info(string.Format("EVEN Insertion ..."));
                even.evenidid = null;
                evenInserted = bll.AddEvento(even);
                if (evenInserted == null)
                    throw new Exception("Error during EVEN writing into the DB.");
                log.Info(string.Format("EVEN Inserted. Got {0} EVENIDID!", evenInserted.evenidid));

                evenIdStr = evenInserted.evenidid.ToString();

                pres.preseven = evenInserted.evenidid;
                log.Info(string.Format("PRES foreign key PRESEVEN updated!"));
                // Validation Pres!!!!
                if (!bll.ValidatePres(pres, ref errorString))
                {
                    string msg = "Validation Pres Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }                               
                
                pres.hl7_stato = hl7_stato;

                // Create Pres
                log.Info(string.Format("PRES Insertion ..."));
                presInserted = bll.AddPrestazione(pres);
                if (presInserted == null)
                    throw new Exception("Error during PRES writing into the DB.");
                log.Info(string.Format("PRES Inserted. Got {0} PRESIDID!", presInserted.presidid));

                presIdStr = presInserted.presidid.ToString();

                esam.esameven = evenInserted.evenidid;
                // Validation Esam!!!!
                if (!bll.ValidateEsam(esam, ref errorString))
                {
                    string msg = "Validation Esam Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }
                
                log.Info(string.Format("ESAM foreign key ESAMEVEN updated!"));
                esam.hl7_stato = hl7_stato;

                // Create Esam
                log.Info(string.Format("ESAM Insertion ..."));
                esamInserted = bll.AddRichiestaLIS(esam);
                if (esamInserted == null)
                    throw new Exception("Error during ESAM writing into the DB.");
                log.Info(string.Format("ESAM Inserted. Got {0} ESAMIDID!", esamInserted.esamidid));

                esamIdStr = esamInserted.esamidid.ToString();

                anals.ForEach(p => { p.analesam = int.Parse(esamIdStr); p.hl7_stato = hl7_stato; });

                // Validation Anals!!!!
                if (!bll.ValidateAnals(anals, ref errorString))
                {
                    string msg = "Validation Anals Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                // Create Anals
                log.Info(string.Format("Insertion of {0} ANAL requested. Processing ...", anals.Count));
                analsInserted = bll.AddAnalisis(anals);
                if ((analsInserted == null) || (analsInserted != null && analsInserted.Count != anals.Count))
                    throw new Exception("Error during ANALs writing into the DB.");
                log.Info(string.Format("Inserted {0} ANAL successfully!", analsInserted.Count));

                // Log Total Number Of Records Inserted! 1 for Esam, 1 for PRES and n for ANAL(s)
                log.Info(string.Format("Inserted {0} records successfully!", analsInserted.Count + 1 + 1));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);

                if (errorString == "")
                    errorString = msg + "\r\n" + ex.Message;
                else
                    errorString += "\r\n" + msg + "\r\n" + ex.Message;

                int evenRB = 0;
                int presRB = 0;
                int esamRB = 0;
                int analsRB = 0;

                log.Info(string.Format("Rolling Back of the Insertions due an error occured ..."));
                // Rolling Back
                if (evenIdStr != null)
                {
                    evenRB = bll.DeleteEventoById(evenIdStr);
                    log.Info(string.Format("Rolled Back {0} EVEN record. EVENIDID was {1}!", evenRB, evenIdStr));
                }
                if (presIdStr != null)
                {
                    presRB = bll.DeletePrestazioneById(presIdStr);
                    log.Info(string.Format("Rolled Back {0} PRES record. PRESIDID was {1}!", presRB, presIdStr));
                }
                if (esamIdStr != null)
                {
                    esamRB = bll.DeleteRichiestaLISById(esamIdStr);
                    log.Info(string.Format("Rolled Back {0} ESAM record. ESAMIDID was {1}!", esamRB, esamIdStr));
                    analsRB = bll.DeleteAnalisiByRichiesta(esamIdStr);
                    log.Info(string.Format("Rolled Back {0} ANAL records. ANALESAM was {1}!", analsRB, esamIdStr));
                }

                log.Info(string.Format("Rolled Back {0} records of {1} requested!", evenRB + presRB + esamRB + analsRB, anals.Count + 1 + 1 + 1));
                esamIdStr = null;
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            if (errorString == "")
                errorString = null;

            return esamIdStr;
        }    
        public MirthResponseDTO SubmitNewRequest(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            if (errorString == null)
                errorString = "";

            try
            {
                // 0. Check if richid is a numeric Value
                int richid_int = 0;
                if (!int.TryParse(richid, out richid_int))
                {
                    string msg = string.Format("ID of the riquest is not an integer string. {0} is not a valid ID for this context!", richid);
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 1. Check if ESAM and ANAL exist
                RichiestaLISDTO chkEsam = bll.GetRichiestaLISById(richid);
                List<AnalisiDTO> chkAnals = bll.GetAnalisisByRichiesta(richid);
                if (chkEsam == null || chkAnals == null || (chkAnals != null && chkAnals.Count == 0))
                {
                    string msg = "Error! No Esam or Anal records found referring to EsamID " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 2. Check if PRES exists
                if (!chkEsam.esameven.HasValue)
                {
                    string msg = "Error! Found an EsamEven NULL referring to EsamID " + richid + "!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                PrestazioneDTO chkPres = bll.GetPrestazioneByEvento(chkEsam.esameven.Value.ToString());
                if (chkPres == null)
                {
                    string msg = "Error! No Pres record found referring to EvenID " + chkEsam.esameven.Value.ToString() + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                string presidid = chkPres.presidid.Value.ToString();

                // 3. Settare Stato a "SEDNING"
                int res = bll.ChangeHL7StatusAndMessageAll(richid, presidid, IBLL.HL7StatesRichiestaLIS.Sending, "");

                // 4. Invio a Mirth
                string hl7orl = bll.SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 3.e1 Cambiare stato in errato
                    int err = bll.ChangeHL7StatusAndMessageAll(richid, presidid, IBLL.HL7StatesRichiestaLIS.Errored, msg);
                    // 3.e2 Restituire null
                    return null;
                }
                // 4.1 Settare a SENT
                int snt = bll.ChangeHL7StatusAndMessageAll(richid, presidid, IBLL.HL7StatesRichiestaLIS.Sent, "");

                // 5. Estrarre i dati dalla risposta di Mirth                
                log.Info("Mirth Data Response Extraction ...");
                data = bll.ORLParser(hl7orl);
                if (data == null)
                {
                    string emsg = "Mirth Data Response Extraction failed!";
                    if (errorString == "")
                        errorString = emsg;
                    else
                        errorString += "\n\r" + emsg;
                    log.Info(emsg);
                    log.Error(emsg);

                }                    
                else
                    log.Info("Mirth Data Response Successfully extracted!");

                // 6. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaLIS.Sent;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaLIS.Errored;
                else
                {
                    if (data.Labes != null)
                    {
                        status = IBLL.HL7StatesRichiestaLIS.Labelled;
                    }
                    else
                    {
                        string msg = "An Error Occurred! No Lables Retrieved By the Remote LAB!";
                        errorString = msg;
                        log.Info(msg);
                        log.Error(msg);
                        return null;
                    }
                }
                RichiestaLISDTO RichUpdt = bll.ChangeHL7StatusAndMessageRichiestaLIS(richid, status, data.ACKDesc);
                PrestazioneDTO PresUpdt = bll.ChangeHL7StatusAndMessagePrestazione(presidid, status, data.ACKDesc);

                List<ORCStatus> orcs = data.ORCStatus;
                if (orcs != null)
                    foreach (ORCStatus orc in orcs)
                    {
                        string desc = orc.Description;
                        string stat = orc.Status;
                        string analid = orc.AnalID;
                        List<AnalisiDTO> AnalUpdts = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { analid }, stat, desc);
                    }

                // 7. Scrivere Labels nel DB
                if (data.Labes != null)
                {
                    data.Labes.ForEach(p => p.labeesam = richid_int);
                    List<LabelDTO> stored = bll.StoreLabels(data.Labes);
                    if (stored == null)
                    {
                        string msg = "An Error Occurred! Labels successfully retrieved by the remote LAB, but they haven't been sotred into the local DB! The EsamIDID is " + richid;
                        errorString = msg;
                        log.Info(msg);
                        log.Error(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            // 8. Restituire il DTO
            return data;
        }

        public List<RisultatoDTO> Check4Results(string analid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RisultatoDTO> riss = null;

            riss = bll.GetRisultatiByAnalId(analid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return riss;
        }
        public List<AnalisiDTO> Check4Analysis(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<AnalisiDTO> anals = null;

            anals = bll.GetAnalisisByRichiesta(richid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anals;
        }
        public List<RichiestaLISDTO> Check4Richs(string evenid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RichiestaLISDTO> exams = null;

            exams = bll.GetRichiesteLISByEven(evenid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return exams;
        }
        public RefertoDTO Check4Report(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RefertoDTO refe = null;

            refe = bll.GetRefertoByEsamId(richid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return refe;
        }
        public List<LabelDTO> Check4Labels(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<LabelDTO> labes = null;

            labes = bll.GetLabelsByRichiesta(richid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return labes;
        }

        public RichiestaLISDTO RetrieveRich(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RichiestaLISDTO rich = null;

            rich = bll.GetRichiestaLISById(richid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public EventoDTO RetrieveEven(string evenid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            EventoDTO even = null;

            even = bll.GetEventoById(evenid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return even;
        }
        public PrestazioneDTO RetrievePresByEven(string evenid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            PrestazioneDTO pres = null;

            pres = bll.GetPrestazioneByEvento(evenid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pres;
        }
        public PrestazioneDTO RetrievePres(string presid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            PrestazioneDTO pres = null;

            pres = bll.GetPrestazioneById(presid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pres;
        }
        public RepartoDTO RetrieveRepa(string repaid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RepartoDTO repa = null;

            repa = bll.GetRepartoById(repaid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }
        public RepartoDTO RetrieveRepaByNome(string repanome)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RepartoDTO repa = null;

            repa = bll.GetRepartoByNome(repanome);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }
        public PazienteDTO RetrievePazi(string paziid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            PazienteDTO pazi = null;

            pazi = bll.GetPazienteById(paziid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pazi;
        }
        public AnalisiDTO RetrieveAnal(string analid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            AnalisiDTO anal = null;

            anal = bll.GetAnalisiById(analid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anal;
        }

        public MirthResponseDTO CancelRequest(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            try
            {
                // 0. Check if richid is a numeric Value
                int richid_int = 0;
                if (!int.TryParse(richid, out richid_int))
                {
                    string msg = string.Format("ID of the riquest is not an integer string. {0} is not a valid ID for this context!", richid);
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 1. Check if Canceling is allowed
                if (CheckIfCancelingIsAllowed(richid, ref errorString))
                {
                    string msg = string.Format("Canceling of the request with id {0} is denied! errorString: {1}", richid, errorString);
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 2. Check if ESAM and ANAL exist
                RichiestaLISDTO chkEsam = bll.GetRichiestaLISById(richid);
                List<AnalisiDTO> chkAnals = bll.GetAnalisisByRichiesta(richid);
                if (chkEsam == null || chkAnals == null || (chkAnals != null && chkAnals.Count == 0))
                {
                    string msg = "Error! No Esam or Anal records found referring to EsamID " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 3. Check if PRES exists
                if (!chkEsam.esameven.HasValue)
                {
                    string msg = "Error! Found an EsamEven NULL referring to EsamID " + richid + "!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                PrestazioneDTO chkPres = bll.GetPrestazioneByEvento(chkEsam.esameven.Value.ToString());
                if (chkPres == null)
                {
                    string msg = "Error! No Pres record found referring to EvenID " + chkEsam.esameven.Value.ToString() + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                string presidid = chkPres.presidid.Value.ToString();

                // 4. Settare Stato a "DELETNG"
                int res = bll.ChangeHL7StatusAndMessageAll(richid, presidid, IBLL.HL7StatesRichiestaLIS.Deleting);

                // 5. Invio a Mirth
                string hl7orl = bll.SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 4.e1 Cambiare stato in errato
                    int err = bll.ChangeHL7StatusAndMessageAll(richid, presidid, IBLL.HL7StatesRichiestaLIS.Errored, msg);
                    // 4.e2 Restituire null
                    return null;
                }

                // 6. Estrarre i dati dalla risposta di Mirth
                data = bll.ORLParser(hl7orl);

                // 7. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaLIS.Deleted;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaLIS.Errored;
                RichiestaLISDTO RichUpdt = bll.ChangeHL7StatusAndMessageRichiestaLIS(richid, status, data.ACKDesc);
                PrestazioneDTO PresUpdt = bll.ChangeHL7StatusAndMessagePrestazione(presidid, status, data.ACKDesc);

                List<ORCStatus> orcs = data.ORCStatus;
                if (orcs != null)
                    foreach (ORCStatus orc in orcs)
                    {
                        string desc = orc.Description;
                        string stat = orc.Status;
                        string analid = orc.AnalID;
                        List<AnalisiDTO> AnalUpdts = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { analid }, stat, desc);
                    }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return data;
        }
        public bool CheckIfCancelingIsAllowed(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool res = true;

            if (errorString == null)
                errorString = "";

            RefertoDTO refe = bll.GetRefertoByEsamId(richid);

            if (refe == null)
            {
                List<AnalisiDTO> anals = bll.GetAnalisisByRichiesta(richid);
                foreach (AnalisiDTO anal in anals)
                {
                    List<RisultatoDTO> riss = bll.GetRisultatiByAnalId(anal.analidid.Value.ToString());
                    if (riss != null)
                    {
                        string report = string.Format("Analisi {0} già eseguita! Impossibile Cancellare!", anal.analidid.Value.ToString());
                        res = false;
                        if (errorString != "")
                            errorString += "\r\n" + report;
                        else
                            errorString += report;
                    }
                }
            }
            else
            {
                string report = string.Format("Esame {0} già refertato! Id referto {1}!", richid, refe.refeidid);
                res = false;
                if (errorString != "")
                    errorString += "\r\n" + report;
                else
                    errorString += report;
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
        
        /*
        public List<RisultatoDTO> RetrieveResults_(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RisultatoDTO> riss = null;

            try
            {
                log.Info("Searching for Analysis' related to Request ID " + richid + " ...");
                List<AnalisiDTO> anals = bll.GetAnalisisByRichiesta(richid);
                log.Info(string.Format("Found {0} Analysis' related to Request ID {1}.", anals != null ? anals.Count : 0, richid));
                foreach (AnalisiDTO anal in anals)
                {
                    log.Info("Searching for Results related to Analysis ID " + anal.analidid.Value.ToString() + " ...");
                    List<RisultatoDTO> anres = bll.GetRisultatiByAnalId(anal.analidid.Value.ToString());
                    if (anres != null && anres.Count > 0)
                    {
                        log.Info(string.Format("Found {0} Results related to Analysis ID {1}.", anres, anal.analidid.Value.ToString()));
                        //1. Check if Analisi is "Executed"
                        //1.1 If not, Update Analisi to "Executed"
                        log.Info(string.Format("HL7 Status of Analysis with ID {0}, is '{1}'. HL7 Message is '{2}'.", anal.analidid.Value.ToString(), anal.hl7_stato, anal.hl7_msg));
                        if (anal.hl7_stato != IBLL.HL7StatesAnalisi.Executed)
                        {
                            List<AnalisiDTO> tmp = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { anal.analidid.Value.ToString() }, IBLL.HL7StatesAnalisi.Executed, "Risultati Ottenuti");
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, has been updated to '{1}'.", anal.analidid.Value.ToString(), tmp != null ? tmp.First().hl7_stato : "--error occurred--"));
                        }
                        //2. Add to Collection
                        if (riss == null)
                            riss = new List<RisultatoDTO>();
                        riss.AddRange(anres);
                        log.Info(string.Format("{0} Results related to Analysis ID {1}, has been added to the Results Collection (actually {2} total items).", anres.Count, anal.analidid.Value.ToString(), riss.Count));
                    }
                    else
                    {
                        log.Info(string.Format("Found No Results related to Analysis ID {0}.", anal.analidid.Value.ToString()));
                        log.Info("Searching for Raw Results related to Request ID - Analysis ID " + richid + "-" + anal.analidid.Value.ToString() + " ...");
                        List<RisultatoDTO> anresNew = bll.GetRisultatiByEsamAnalId(richid + "-" + anal.analidid.Value.ToString());
                        log.Info(string.Format("Found {0} Raw Results related to Request ID - Analysis ID : {1}-{2}.", anresNew != null ? anresNew.Count : 0, richid, anal.analidid.Value.ToString()));
                        if (anresNew != null && anresNew.Count > 0)
                        {
                            //1. Add new Risultato as Executed                            
                            List<RisultatoDTO> news = bll.AddRisultati(anresNew);
                            log.Info(string.Format("{0} Raw Results Converted and Written into DB. They are Related to Analysis ID {1}. ANRE ID are '{2}'.", news != null ? news.Count : 0, anal.analidid.Value.ToString(), news != null ? string.Join(", ", news.Select(p => p.anreidid).ToList().ToArray()) : ""));
                            //2. Update Analisi to "Executed"                            
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, is '{1}'. HL7 Message is '{2}'.", anal.analidid.Value.ToString(), anal.hl7_stato, anal.hl7_msg));
                            List<AnalisiDTO> tmp = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { anal.analidid.Value.ToString() }, IBLL.HL7StatesAnalisi.Executed, "Risultati Ottenuti");
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, has been updated to '{1}'.", anal.analidid.Value.ToString(), tmp != null ? tmp.First().hl7_stato : "--error occurred--"));
                            //3. Add to Collection                        
                            if (news != null && news.Count > 0)
                            {
                                if (riss == null)
                                    riss = new List<RisultatoDTO>();
                                riss.AddRange(news);
                                log.Info(string.Format("{0} Results related to Analysis ID {1}, has been added to the Results Collection (actually {2} total items).", news.Count, anal.analidid.Value.ToString(), riss.Count));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return riss;
        }
        */

        public List<RisultatoDTO> RetrieveResults(string richid_, ref string errorString, bool? forceUpdating = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RisultatoDTO> riss = null;

            try
            {
                RichiestaLISDTO rich = bll.GetRichiestaLISById(richid_);
                if (rich == null)
                {
                    string msg = string.Format("No Rich with ID: {0} found. The operation will be aborted!", richid_);
                    log.Info(msg);
                    throw new Exception(msg);
                }

                rich = null;

                log.Info("Searching for Analysis' related to Request ID " + richid_ + " ...");
                List<AnalisiDTO> anals = bll.GetAnalisisByRichiesta(richid_);
                log.Info(string.Format("Found {0} Analysis' related to Request ID {1}.", anals != null ? anals.Count : 0, richid_));
                foreach (AnalisiDTO anal in anals)
                {
                    log.Info("Searching for Results related to Analysis ID " + anal.analidid.Value.ToString() + " ...");
                    List<RisultatoDTO> anres = bll.GetRisultatiByAnalId(anal.analidid.Value.ToString());
                    if (anres != null && anres.Count > 0)
                    {
                        log.Info(string.Format("Found {0} Results related to Analysis ID {1}.", anres.Count, anal.analidid.Value.ToString()));
                        //0. Check if updating is set to Forced!
                        if (forceUpdating != null)
                        {
                            //0.1 If it is Updating is true
                            if (forceUpdating.Value)
                            {
                                log.Info("Requested a forced updating of the Raw Results!");

                                //1. Get Updated Risultati
                                log.Info("Searching for Raw Results related to Request ID - Analysis ID " + richid_ + "-" + anal.analidid.Value.ToString() + " ...");
                                List<RisultatoDTO> anresUpdt = bll.GetRisultatiByEsamAnalId(richid_ + "-" + anal.analidid.Value.ToString());
                                log.Info(string.Format("Found {0} Raw Results related to Request ID - Analysis ID : {1}-{2}.", anresUpdt != null ? anresUpdt.Count : 0, richid_, anal.analidid.Value.ToString()));
                                if (anresUpdt != null && anresUpdt.Count > 0)
                                {
                                    //2. Delete Old Risultati       
                                    int removedRes = bll.DeleteRisultatiByIdAnalisi(anal.analidid.Value.ToString());
                                    log.Info(string.Format("Removed {0} Result items related to AnalId: {1}", removedRes, anal.analidid.Value.ToString()));
                                    //3. Write New Risultati
                                    List<RisultatoDTO> updts = bll.AddRisultati(anresUpdt);
                                    log.Info(string.Format("{0} Raw Results Converted and Written into DB. They are Related to Analysis ID {1}. ANRE ID are '{2}'.", updts != null ? updts.Count : 0, anal.analidid.Value.ToString(), updts != null ? string.Join(", ", updts.Select(p => p.anreidid).ToList().ToArray()) : ""));
                                }

                                log.Info("Forced updating of Results Completed!");
                            }
                        }

                        //1. Check if Analisi is "Executed"
                        //1.1 If not, Update Analisi to "Executed"
                        log.Info(string.Format("HL7 Status of Analysis with ID {0}, is '{1}'. HL7 Message is '{2}'.", anal.analidid.Value.ToString(), anal.hl7_stato, anal.hl7_msg));
                        if (anal.hl7_stato != IBLL.HL7StatesAnalisi.Executed)
                        {
                            List<AnalisiDTO> tmp = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { anal.analidid.Value.ToString() }, IBLL.HL7StatesAnalisi.Executed, "Risultati Ottenuti");
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, has been updated to '{1}'.", anal.analidid.Value.ToString(), tmp != null ? tmp.First().hl7_stato : "--error occurred--"));
                        }
                        //2. Add to Collection
                        if (riss == null)
                            riss = new List<RisultatoDTO>();
                        riss.AddRange(anres);
                        log.Info(string.Format("{0} Results related to Analysis ID {1}, has been added to the Results Collection (actually {2} total items).", anres.Count, anal.analidid.Value.ToString(), riss.Count));
                    }
                    else
                    {
                        log.Info(string.Format("Found No Results related to Analysis ID {0}.", anal.analidid.Value.ToString()));
                        log.Info("Searching for Raw Results related to Request ID - Analysis ID " + richid_ + "-" + anal.analidid.Value.ToString() + " ...");
                        List<RisultatoDTO> anresNew = bll.GetRisultatiByEsamAnalId(richid_ + "-" + anal.analidid.Value.ToString());
                        log.Info(string.Format("Found {0} Raw Results related to Request IDExt - Analysis ID : {1}-{2}.", anresNew != null ? anresNew.Count : 0, richid_, anal.analidid.Value.ToString()));
                        if (anresNew != null && anresNew.Count > 0)
                        {
                            //1. Add new Risultato as Executed                            
                            List<RisultatoDTO> news = bll.AddRisultati(anresNew);
                            log.Info(string.Format("{0} Raw Results Converted and Written into DB. They are Related to Analysis ID {1}. ANRE ID are '{2}'.", news != null ? news.Count : 0, anal.analidid.Value.ToString(), news != null ? string.Join(", ", news.Select(p => p.anreidid).ToList().ToArray()) : ""));
                            //2. Update Analisi to "Executed"                            
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, is '{1}'. HL7 Message is '{2}'.", anal.analidid.Value.ToString(), anal.hl7_stato, anal.hl7_msg));
                            List<AnalisiDTO> tmp = bll.ChangeHL7StatusAndMessageAnalisis(new List<string>() { anal.analidid.Value.ToString() }, IBLL.HL7StatesAnalisi.Executed, "Risultati Ottenuti");
                            log.Info(string.Format("HL7 Status of Analysis with ID {0}, has been updated to '{1}'.", anal.analidid.Value.ToString(), tmp != null ? tmp.First().hl7_stato : "--error occurred--"));
                            //3. Add to Collection                        
                            if (news != null && news.Count > 0)
                            {
                                if (riss == null)
                                    riss = new List<RisultatoDTO>();
                                riss.AddRange(news);
                                log.Info(string.Format("{0} Results related to Analysis ID {1}, has been added to the Results Collection (actually {2} total items).", news.Count, anal.analidid.Value.ToString(), riss.Count));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return riss;
        }
    }
}
