using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace ZETDecoder
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string path = "C:\\Users\\Paglonico\\Desktop\\HLT 7 Saggese\\Test_20161114";
            //string filename = "rispostaZETAMIBIFBIL.txt";
            string filename = "rispostaZETEMO.txt";

            string fileContent = "";

            try
            {
                fileContent = System.IO.File.ReadAllText(path + "\\" + filename);

                //Console.WriteLine(fileContent);
                //List<string> data_ = GetAllZETsSegments(fileContent);
                //List<ZETHL7> data = ZETMapper(data_);
                List<string> data_ = GeneralPurposeLib.LibString.GetAllValuesSegments(fileContent, "MPH");
                Console.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during File Reading. Exception: " + ex.Message);
            }

            sw.Stop();

            Console.WriteLine("ElapsedTime {0}\n\n", sw.Elapsed);

            Console.WriteLine("Press a key to Close!");
            Console.ReadKey();
        }
        
        


        static List<string> GetAllZETsSegments(string data)
        {
            List<string> res = null;

            int st = data.IndexOf("ZET");
            string ZETsubstring = data.Substring(st);
            try
            {
                string[] tmp = ZETsubstring.Split(new string[] { "ZET" }, StringSplitOptions.RemoveEmptyEntries);
                res = new List<string>(tmp);
                res = res.Select(str=>str.Replace(Environment.NewLine, "")).ToList();       
            }
            catch(Exception)
            {
                res = null;
            }

            Console.WriteLine(ZETsubstring);

            return res;
        }

        public static List<ZETHL7> ZETMapper(List<string> data)
        {
            List<ZETHL7> res = new List<ZETHL7>();

            foreach(string r in data)
            {
                object[] tmp = r.Split('|');
                ZETHL7 tmp2 = ZETMapper(tmp);
                res.Add(tmp2);
            }

            return res;
        }

        public static ZETHL7 ZETMapper(object[] data)
        {
            ZETHL7 res = new ZETHL7();

            res.barcode = (string)data[1];
            res.desc = (string)data[2];
            res.idContainer = (string)data[3];
            res.idLab = (string)data[6];
            res.idReq = (string)data[7];
            //res.dateAcce = (DateTime)data[8];
            res.dateAcce = DateTime.ParseExact(((string)data[8]), "yyyyMMddHHmm", null);
            res.idRepa = (string)data[17];
            res.nameRepa = (string)data[18];
            res.idAcce = (string)data[19];
            res.idMate = (string)data[20];
            res.analList = ((string)data[22]).Split(' ').ToList();
            res.idSect = (string)data[23];
            res.nameSect = (string)data[24];
            //res.datePrel = (DateTime)data[25];
            res.datePrel = DateTime.ParseExact(((string)data[25]), "yyyyMMddHHmm", null);

            return res;
        }

        public class ZETHL7
        {
            public string barcode { get; set; }
            public string desc { get; set; }
            public string idContainer { get; set; }
            public string idLab { get; set; }
            public string idReq { get; set; }
            public DateTime dateAcce { get; set; }
            public string idRepa { get; set; }
            public string nameRepa { get; set; }
            public string idAcce { get; set; }
            public string idMate { get; set; }
            public List<string> analList { get; set; }
            public string idSect { get; set; }
            public string nameSect { get; set; }
            public DateTime datePrel { get; set; }
        }
    }
}