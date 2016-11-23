using IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestPlugin
{
    class Program
    {
        DataAccessLayer.LISDAL dal = null;
        BusinessLogicLayer.LISBLL bll = null;

        public void init()
        {
            dal = new DataAccessLayer.LISDAL();
            bll = new BusinessLogicLayer.LISBLL(dal);
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DataAccessLayer.LISDAL dal = new DataAccessLayer.LISDAL();
            BusinessLogicLayer.LISBLL bll = new BusinessLogicLayer.LISBLL(dal);

            int esamidid = 8194053;

            RichiestaLISDTO esam = new RichiestaLISDTO()
            {
                esameven = 0,
                esamtipo = 0,
                esampren = DateTime.Now,
                hl7_stato = "SENDING"
            };
            // Insert ESAM

            List<AnalisiDTO> anals = new List<AnalisiDTO>()
            {
                new AnalisiDTO()
                {
                    analesam = esam.esamidid,
                    analcodi = "EMO",
                    analnome = "EMOCROMO",
                    analflro = 0,
                    analextb = "235",
                    hl7_stato = "SENDING"
                },
                new AnalisiDTO()
                {
                    analesam = esam.esamidid,
                    analcodi = "AMI",
                    analnome = "AMILASI",
                    analflro = 0,
                    analextb = "273",
                    hl7_stato = "SENDING"
                },
                new AnalisiDTO()
                {
                    analesam = esam.esamidid,
                    analcodi = "BIF",
                    analnome = "BILIRUBINA DIRETTA",
                    analflro = 0,
                    analextb = "281",
                    hl7_stato = "SENDING"
                },
            };

            //List<AnalisiDTO> data = bll.GetAnalisisByRichiesta("8682276");

            //string richid = bll.ScheduleNewRequest(esam, anals);
            //string richid = "8682283";
            //MirthResponseDTO result = bll.SubmitNewRequest(richid);
            //int did = bll.ChangeHL7StatusAndMessageAll(richid, "SENINDG", null);

            string esamanalid = "8194054-607448";
            List<RisultatoDTO> ress = bll.GetRisultatiByEsamAnalId(esamanalid);

            List<RisultatoDTO> inserted = bll.AddRisultati(ress);

            sw.Stop();

            Console.WriteLine("ElapsedTime {0}\n\n", sw.Elapsed);

            Console.WriteLine("Press a key to Close!");
            Console.ReadKey();
        }

        
    }
}
