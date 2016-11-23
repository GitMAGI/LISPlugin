using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace TestCalling
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int esamidid = 8194053;
            //int esamidid = 8194054;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Mymsg>" + "<esamidid>" + esamidid + "</esamidid>" + "</Mymsg>");

                using (TestMirth.MirthWS.DefaultAcceptMessageClient client = new TestMirth.MirthWS.DefaultAcceptMessageClient())
                {
                    string response = client.acceptMessage(xml2String(doc));

                    string msgResponse = null;
                    if (response == null)
                    {
                        msgResponse = "NULL - An Error Occurred!";
                    }
                    else
                    {
                        msgResponse = response;
                    }
                    Console.WriteLine("Mirth Response:\n{0}\n", msgResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred! Exception catched:\n{0}", ex.Message);
            }
            
            sw.Stop();

            Console.WriteLine("ElapsedTime {0}\n\n", sw.Elapsed);

            Console.WriteLine("Press a key to Close!");
            Console.ReadKey();
        }

        public static string xml2String(XmlDocument xmlDoc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

    }
}
