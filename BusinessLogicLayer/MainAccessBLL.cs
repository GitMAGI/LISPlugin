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

        public int ChangeHL7StatusAndMessageAll(string richidid, string presidid, string hl7_stato, string hl7_msg = null)
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
            res += esamres;

            log.Info(string.Format("Updated {0}/{1} record!", esamres, 1));

            log.Info(string.Format("Updating PRES ..."));

            PrestazioneDTO got2 = GetPrestazioneById(presidid);
            got2.hl7_stato = hl7_stato;
            got2.hl7_msg = hl7_msg != null ? hl7_msg : got2.hl7_msg;
            PrestazioneDTO updt1 = UpdatePrestazione(got2);

            int presres = 0;
            if (updt1 != null)
                presres++;
            else
                log.Info(string.Format("An Error occurred. Record not updated! PRESIDID: {0}", got2.presidid));
            res += presres;

            log.Info(string.Format("Updated {0}/{1} record!", presres, 1));

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

            List<AnalisiDTO> updateds = null;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating ANAL ..."));
            List<AnalisiDTO> gots = GetAnalisisByIds(analidids);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (AnalisiDTO got_ in gots)
            {
                AnalisiDTO updt_ = UpdateAnalisi(got_);
                if (updt_ != null)
                {
                    if(updateds==null)
                        updateds = new List<AnalisiDTO>();
                    updateds.Add(updt_);
                    analsres++;
                }                    
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
        public PrestazioneDTO ChangeHL7StatusAndMessagePrestazione(string presidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            PrestazioneDTO updated = new PrestazioneDTO();

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating PRES ..."));

            PrestazioneDTO got = GetPrestazioneById(presidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            updated = UpdatePrestazione(got);

            int res = 0;
            if (updated != null)
            {
                res++;
            }
            else
            {
                log.Info(string.Format("An Error occurred. Record not updated! PRESIDID: {0}", got.presidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", res, 1));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updated;
        }

        public bool ValidateEven(EventoDTO even, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";
            
            if (even.evenepis == null)
            {
                string msg = "EVENEPIS is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "EVEN error: " + msg;
                else
                    errorString += "EVEN error: " + msg;
            }
            if (even.evenreri == null)
            {
                string msg = "EVENRERI is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "EVEN error: " + msg;
                else
                    errorString += "EVEN error: " + msg;
            }
            if (even.evenrees == null)
            {
                string msg = "EVENREES is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "EVEN error: " + msg;
                else
                    errorString += "EVEN error: " + msg;
            }
            if (even.evendata == null)
            {
                string msg = "EVENDATA is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "EVEN error: " + msg;
                else
                    errorString += "EVEN error: " + msg;
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
        public bool ValidatePres(PrestazioneDTO pres, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";

            if (pres.preseven == null)
            {
                string msg = "PRESEVEN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "PRES error: " + msg;
                else
                    errorString += "PRES error: " + msg;
            }            

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
        public bool ValidateEsam(RichiestaLISDTO esam, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;            

            if (errorString == null)
                errorString = "";
            /*
            string richid = esam.esamidid.ToString();
            if (esam.esamidid == null)
            {
                string msg = "ESAMIDID is Null!";
                validate = false;
                if(errorString != "")
                    errorString += "\r\n" + "ESAMIDID " + richid + ": " + msg;
                else
                    errorString += "ESAMIDID " + richid + ": " + msg;
            }*/
            if (esam.esameven == null)
            {
                string msg = "ESAMEVEN is Null!";
                validate = false;
                if (errorString != "")                    
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            if(esam.esamtipo == null)
            {
                string msg = "ESAMTIPO is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            if(esam.esampren == null)
            {
                string msg = "ESAMPREN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "ESAM error: " + msg;
                else
                    errorString += "ESAM error: " + msg;
            }
            
            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
        public bool ValidateAnals(List<AnalisiDTO> anals, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";
            
            int count = 0;
            foreach (AnalisiDTO anal in anals)
            {
                count++;
                /*
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
                */
                if (anal.analesam == null)
                {
                    string msg = "ANALESAM is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANAL (" + count + ") error: " + msg;
                    else
                        errorString += "ANAL (" + count + ") error: " + msg;
                }
                if (anal.analcodi == null)
                {
                    string msg = "ANALCODI is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANAL (" + count + ") error: " + msg;
                    else
                        errorString += "ANAL (" + count + ") error: " + msg;
                }
                if (anal.analnome == null)
                {
                    string msg = "ANALNOME is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANAL (" + count + ") error: " + msg;
                    else
                        errorString += "ANAL (" + count + ") error: " + msg;
                }
                if (anal.analinvi == null)
                {
                    string msg = "ANALINVI is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANAL (" + count + ") error: " + msg;
                    else
                        errorString += "ANAL (" + count + ") error: " + msg;
                }
                if (anal.analflro == null)
                {
                    string msg = "ANALFLRO is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "ANAL (" + count + ") error: " + msg;
                    else
                        errorString += "ANAL (" + count + ") error: " + msg;
                }
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
    }
}
