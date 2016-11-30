using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace DataAccessLayer
{
    public partial class LISDAL
    {
        public string SendLISRequest(string richidid)
        {
            string result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            try
            {
                log.Info("Building XML Request for Mirth ... ");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Mymsg>" + "<esamidid>" + richidid + "</esamidid>" + "</Mymsg>");
                string xmlreq = LibString.XML2String(doc);
                log.Info("XML Request built and stringyfied! XML: " + xmlreq);

                log.Info("Mirth client instatiating ... ");
                using (MirthWS.DefaultAcceptMessageClient client = new MirthWS.DefaultAcceptMessageClient())
                {
                    string addr = client.Endpoint.Address.Uri.AbsoluteUri;
                    int port = client.Endpoint.Address.Uri.Port;
                    string name = client.Endpoint.Name;
                    string clientInfo = string.Format("Address: {0} | Port: {1} | Name: {2}", addr, port, name);
                    log.Info("Mirth client instatiated! Endpoint Info -> " + clientInfo);

                    log.Info("Querying the client ...");
                    try
                    {
                        result = client.acceptMessage(xmlreq);
                        log.Info("Response got!");
                    }
                    catch(Exception ex)
                    {
                        result = null;
                        string err = "Error during the communication to the MIRTH. No response got!";
                        log.Info(err);
                        log.Error(err + " Exception detected: " + ex.Message);
                    }                    
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

            return result;
        }
    }
}
