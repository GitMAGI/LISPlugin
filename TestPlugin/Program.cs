using IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            RichiestaLISDTO esam = new RichiestaLISDTO()
            {
                esameven = 2182265,
                esamtipo = 0,
                esampren = DateTime.Now,
                hl7_stato = IBLL.HL7StatesRichiestaLIS.Sending,
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
                    analinvi = 0,
                    analextb = "235",
                    hl7_stato = IBLL.HL7StatesAnalisi.Sending,
                },
                new AnalisiDTO()
                {
                    analesam = esam.esamidid,
                    analcodi = "AMI",
                    analnome = "AMILASI",
                    analflro = 0,
                    analinvi = 0,
                    analextb = "273",
                    hl7_stato = IBLL.HL7StatesAnalisi.Sending,
                },
                new AnalisiDTO()
                {
                    analesam = esam.esamidid,
                    analcodi = "BIF",
                    analnome = "BILIRUBINA DIRETTA",
                    analflro = 0,
                    analinvi = 0,
                    analextb = "281",
                    hl7_stato = IBLL.HL7StatesAnalisi.Sending,
                },
            };

            string errs = null;

            LISPlugin.LIS lis = new LISPlugin.LIS();

            //string richid = lis.ScheduleNewRequest(esam, anals, ref errs);
            string evenid = "2182265";
            string richid = "8682284";
            string analid1 = "4645449";
            string analid2 = "4645450";
            string analid3 = "4645451";

            //List<RichiestaLISDTO> richs = lis.Check4Exams(evenid);
            //List<AnalisiDTO> anals_ = lis.Check4Analysis(richid);

            //MirthResponseDTO resp = lis.SubmitNewRequest(richid, ref errs);
            //List<LabelDTO> labes = lis.Check4Labels(richid);

            //List<RisultatoDTO> riss = lis.RetrieveResults(richid, ref errs);

            //List<RisultatoDTO> riss = lis.Check4Results(analid1);

            //bool cnl = lis.CheckIfCancelingIsAllowed(richid, ref errs);

            List<int> red = null;

            int r = 0;
            if (red != null && red.Count > 0)
                r = 1;

            sw.Stop();

            Console.WriteLine("ElapsedTime {0}\n\n", sw.Elapsed);

            Console.WriteLine("Press a key to Close!");
            Console.ReadKey();
        }                
    }
}
