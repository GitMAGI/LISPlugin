using System;
using System.Collections.Generic;
using IBLL.DTO;
using System.Diagnostics;
using GeneralPurposeLib;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public MirthResponseDTO ORLParser(string raw)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = new MirthResponseDTO();

            log.Info(string.Format("HL7 Message To Process:\n{0}", raw));

            log.Info(string.Format("HL7 Message Processing ... "));

            try
            {
                
                log.Info(string.Format("MSA Recovering ..."));
                // 1. Get MSA Segment
                string msa = LibString.GetAllValuesSegments(raw, "MSA")[0];
                string[] msaobj = msa.Split('|');
                data.ACKCode = msaobj[1];
                data.MsgID = msaobj[2];
                data.ACKDesc = msaobj.Length > 3 ? msaobj[2] : null;
                switch (data.ACKCode)
                {
                    case "AA":
                        data.Errored = false;
                        data.Accepted = true;
                        data.Refused = false;
                        break;
                    case "AE":
                        data.Errored = true;
                        data.Accepted = false;
                        data.Refused = false;
                        break;
                    case "AR":
                        data.Errored = false;
                        data.Accepted = false;
                        data.Refused = true;
                        break;
                }
                log.Info(string.Format("MSA Recovered"));
                
                // 2. Get ERR Segment            
                log.Info(string.Format("ERR Recovering ..."));
                List<string> errs = LibString.GetAllValuesSegments(raw, "ERR");
                if (errs != null)
                    data.ERRMsg = errs[0];
                log.Info(string.Format("ERR Recovered"));
                // 3. Get ORC Segment
                log.Info(string.Format("ORC Recovering ..."));
                List<string> orcs = LibString.GetAllValuesSegments(raw, "ORC");
                if (orcs != null)
                {
                    foreach (string orc in orcs)
                    {
                        try
                        {
                            string[] ocrobj = orc.Split('|');
                            ORCStatus ORC = new ORCStatus();
                            string[] esIdanId = ocrobj[2].Split('-');
                            ORC.EsamID = esIdanId[0];
                            ORC.AnalID = esIdanId[1];
                            ORC.Status = ocrobj[1];
                            string desc = null;
                            switch (ORC.Status)
                            {
                                case "OK":
                                    desc = "Inserimento/Cancellazione eseguito con successo";
                                    break;
                                case "RQ":
                                    desc = "Modifica Eseguita con successo";
                                    break;
                                case "UA":
                                    desc = "Impossibile Inserire";
                                    break;
                                case "UC":
                                    desc = "Impossibile Cancellare";
                                    break;
                                case "UM":
                                    desc = "Impossibile Modificare";
                                    break;
                            }
                            ORC.Description = desc;
                            if (data.ORCStatus == null)
                                data.ORCStatus = new List<ORCStatus>();
                            data.ORCStatus.Add(ORC);
                        }
                        catch (Exception)
                        {
                            string msg = "Exception During ORC info processing! HL7 Segment errored: " + orc;
                            throw new Exception(msg);
                        }
                    }                    
                }
                log.Info(string.Format("ORC Recovered", data.ORCStatus.Count));
                // 4. Get Labels
                log.Info(string.Format("ZET Recovering ..."));
                List<LabelDTO> labes = Mappers.LabelMapper.LabeMapper(raw);
                data.Labes = labes;
                log.Info(string.Format("ZET Recovered", data.ORCStatus.Count));

                log.Info(string.Format("HL7 Message processing Complete! A DTO object has been built!"));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }
        public string SendMirthRequest(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            // 0. Check if esam exists and labels do
            RichiestaLISDTO esam = GetRichiestaLISById(richidid);
            List<AnalisiDTO> anals = GetAnalisisByRichiesta(richidid);

            if ((esam == null || anals == null) || (anals != null && anals.Count == 0))
            {
                string msg = string.Format("An Error occured! No ESAM or ANAL related to id {0} found into the DB. Operation Aborted!", richidid);
                log.Info(msg);
                log.Error(msg);
                return null;
            }

            string data = null;

            try
            {
                // 1. Call DAL.SendMirthREquest()
                data = this.dal.SendLISRequest(richidid);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }
        public List<LabelDTO> StoreLabels(List<LabelDTO> labes)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<LabelDTO> labes_ = AddLabels(labes);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            return labes_;
        }
        public int ChangeHL7StatusAndMessageAll(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int res = 0;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));
            log.Info(string.Format("Updating ESAM ..."));

            RichiestaLISDTO got = GetRichiestaLISById(richidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            RichiestaLISDTO updt = UpdateRichiestaLIS(got);

            int esamres = 0;
            if (updt != null)
                esamres++;
            else
                log.Info(string.Format("An Error occurred. Record not updated! ESAMIDID: {0}", got.esamidid));
            res = esamres;

            log.Info(string.Format("Updated {0}/{1} record!", esamres, 1));

            log.Info(string.Format("Updating ANAL ..."));
            List<AnalisiDTO> gots = GetAnalisisByRichiesta(richidid);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (AnalisiDTO got_ in gots)
            {
                AnalisiDTO updt_ = UpdateAnalisi(got_);
                if (updt_ != null)
                    analsres++;
                else
                    log.Info(string.Format("An Error occurred. Record not updated! ANALIDID: {0}", got_.analidid));
            }
            res += analsres;
            log.Info(string.Format("Updated {0}/{1} record!", analsres, gots.Count));

            log.Info(string.Format("Updated {0} record overall!", res));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
        public List<AnalisiDTO> ChangeHL7StatusAndMessageAnalisis(List<string> analidids, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            List<AnalisiDTO> updateds = new List<AnalisiDTO>();

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating ANAL ..."));

            log.Info(string.Format("Updating ANAL ..."));
            List<AnalisiDTO> gots = GetAnalisisByIds(analidids);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (AnalisiDTO got_ in gots)
            {
                AnalisiDTO updt_ = UpdateAnalisi(got_);
                if (updt_ != null)
                    analsres++;
                else
                    log.Info(string.Format("An Error occurred. Record not updated! ANALIDID: {0}", got_.analidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", analsres, gots.Count));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updateds;
        }
        public RichiestaLISDTO ChangeHL7StatusAndMessageRichiestaLIS(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            RichiestaLISDTO updated = new RichiestaLISDTO();

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating ESAM ..."));

            RichiestaLISDTO got = GetRichiestaLISById(richidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            updated = UpdateRichiestaLIS(got);

            int res = 0;
            if (updated != null)
            {
                res++;                
            }
            else
            {
                log.Info(string.Format("An Error occurred. Record not updated! ESAMIDID: {0}", got.esamidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", res, 1));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updated;
        }

        private static bool Validate(RichiestaLISDTO esam, List<AnalisiDTO> anals, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            string richid = esam.esamidid.ToString();

            if (errorString == null)
                errorString = "";

            if (esam.esamidid == null)
            {
                string msg = "ESAMIDID is Null!";
                validate = false;
                if(errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }
            if(esam.esameven == null)
            {
                string msg = "ESAMEVEN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }
            if(esam.esamtipo == null)
            {
                string msg = "ESAMTIPO is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }
            if(esam.esampren == null)
            {
                string msg = "ESAMPREN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }

            foreach(AnalisiDTO anal in anals)
            {
                string analid = anal.analidid.ToString();
                if (anal.analidid == null)
                {
                    string msg = "";
                    validate = false;                    
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                if (anal.analesam == null)
                {
                    string msg = "";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                if (anal.analcodi == null)
                {
                    string msg = "";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                if (anal.analnome == null)
                {
                    string msg = "";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                if (anal.analinvi == null)
                {
                    string msg = "";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
                if (anal.analflro == null)
                {
                    string msg = "";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANALIDID " + analid + ": " + msg;
                    else
                        errorString += "ANALIDID " + analid + ": " + msg;
                }
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }

        public string ScheduleNewRequest(RichiestaLISDTO esam, List<AnalisiDTO> anals, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));
                        
            string hl7_stato = "IDLE";
            string res = null;

            RichiestaLISDTO esamInserted = null;
            List<AnalisiDTO> analsInserted = null;

            if (errorString == null)
                errorString = "";

            try
            {
                if (esam == null || anals == null || (anals != null && anals.Count == 0))
                    throw new Exception("Error! Request a null or void insertion of ESAM and/or ANAL");

                // Validation!!!!
                if (!Validate(esam, anals, ref errorString))
                {
                    string msg = "Validation Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                // Check if Even Exists
                string evenid = esam.esameven.ToString();
                EventoDTO even = this.GetEventoById(evenid);
                if(even == null)
                {
                    string msg = "Error! Even with evenidid: " + evenid + " doesn't exist! the Scheduling of the request will be aborted!";
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }
                even = null;

                esam.hl7_stato = hl7_stato;
                log.Info(string.Format("ESAM Insertion ..."));
                esamInserted = AddRichiestaLIS(esam);
                if (esamInserted == null)
                    throw new Exception("Error during ESAM writing into the DB");
                log.Info(string.Format("ESAM Inserted. Got {0} ESAMIDID!", esamInserted.esamidid));

                res = esamInserted.esamidid.ToString();

                anals.ForEach(p => { p.analesam = int.Parse(res); p.hl7_stato = hl7_stato; });

                log.Info(string.Format("Insertion of {0} ANAL requested. Processing ...", anals.Count));
                analsInserted = AddAnalisis(anals);
                if ((analsInserted == null) || (analsInserted != null && analsInserted.Count != anals.Count))
                    throw new Exception("Error during ANALs writing into the DB");
                log.Info(string.Format("Inserted {0} ANAL successfully!", analsInserted.Count));
                log.Info(string.Format("Inserted {0} records successfully!", analsInserted.Count + 1));                
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);

                if(errorString=="")
                    errorString = msg + "\r\n" + ex.Message;
                else
                    errorString += "\r\n" + msg + "\r\n" + ex.Message;

                int esamRB = 0;
                int analsRB = 0;

                log.Info(string.Format("Rolling Back of the Insertions due an error occured ..."));
                // Rolling Back
                if (res != null)
                {
                    esamRB = DeleteRichiestaLISById(res);
                    log.Info(string.Format("Rolled Back {0} ESAM record. ESAMIDID was {1}!", esamRB, res));
                    analsRB = DeleteAnalisiByRichiesta(res);
                    log.Info(string.Format("Rolled Back {0} ANAL records. ANALESAM was {1}!", analsRB, res));
                }
                log.Info(string.Format("Rolled Back {0} records of {1} requested!", esamRB + analsRB, anals.Count + 1));
                res = null;
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            if (errorString == "")
                errorString = null;

            return res;
        }
        public MirthResponseDTO SubmitNewRequest(string richid, ref string errorString)
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

                // 1. Check if ESAM and ANAL exist
                RichiestaLISDTO chkEsam = this.GetRichiestaLISById(richid);
                List<AnalisiDTO> chkAnals = this.GetAnalisisByRichiesta(richid);
                if(chkEsam == null || chkAnals == null || (chkAnals != null && chkAnals.Count == 0))
                {
                    string msg = "Error! No Esam or Anal records found referring to EsamID " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 2. Settare Stato a "SEDNING"
                int res = ChangeHL7StatusAndMessageAll(richid, "SENDING");

                // 3. Invio a Mirth
                string hl7orl = SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 2.e1 Cambiare stato in errato
                    int err = ChangeHL7StatusAndMessageAll(richid, "ERRORED", msg);
                    // 2.e2 Restituire null
                    return null;
                }

                // 4. Estrarre i dati dalla risposta di Mirth
                data = ORLParser(hl7orl);

                // 5. Settare Stato a seconda della risposta
                string status = "SENT";
                if (data.ACKCode != "AA")
                    status = "ERRORED";
                else
                {
                    if (data.Labes != null)
                    {
                        status = "LABEL_RECEIVED";
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
                RichiestaLISDTO RichUpdt = ChangeHL7StatusAndMessageRichiestaLIS(richid, status, data.ACKDesc);

                List<ORCStatus> orcs = data.ORCStatus;
                if (orcs != null)
                    foreach (ORCStatus orc in orcs)
                    {
                        string desc = orc.Description;
                        string stat = orc.Status;
                        string analid = orc.AnalID;
                        List<AnalisiDTO> AnalUpdts = ChangeHL7StatusAndMessageAnalisis(new List<string>() { analid }, stat, desc);
                    }

                // 6. Scrivere Labels nel DB
                if (data.Labes != null)
                {
                    data.Labes.ForEach(p => p.labeesam = richid_int);
                    List<LabelDTO> stored = StoreLabels(data.Labes);
                    if(stored == null)
                    {
                        string msg = "An Error Occurred! Labels successfully retrieved by the remote LAB, but they haven't been sotred into the local DB! The EsamIDID is " + richid;
                        errorString = msg;
                        log.Info(msg);
                        log.Error(msg);
                    }
                }                
            }
            catch(Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            // 7. Restituire il DTO
            return data;
        }
    }
}
