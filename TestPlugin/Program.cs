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

            EventoDTO even = new EventoDTO()
            {
                evenepis = 112986,
                evenreri = 17,
                eventipo = 1,
                evenperi = 306,
                evenrees = 22,
                evendata = Convert.ToDateTime("2017-01-01T00:10:34"),
                evenrepp = 0,
                evendaef = Convert.ToDateTime("2017-01-01T02:45:01"),
            };

            PrestazioneDTO pres = new PrestazioneDTO()
            {
                presstat = 0,
                prestipo = 217,
                presurge = true,
                prespren = Convert.ToDateTime("2017-01-06T18:00:00"),
                presrico = 6,
                presflcc = 0,
                prespers = "MARTELLA LUCIANO",
                preserog = 0,
                presannu = 0,
            };

            RichiestaLISDTO esam = new RichiestaLISDTO()
            {                
                esamtipo = 0,
                esampren = DateTime.Now,                
            };            

            List<AnalisiDTO> anals = new List<AnalisiDTO>()
            {
                new AnalisiDTO()
                {                    
                    analcodi = "EMO",
                    analnome = "EMOCROMO",
                    analflro = 0,
                    analinvi = 0,
                    analextb = "235",                    
                },
                new AnalisiDTO()
                {                    
                    analcodi = "AMI",
                    analnome = "AMILASI",
                    analflro = 0,
                    analinvi = 0,
                    analextb = "273",                    
                },
                new AnalisiDTO()
                {                    
                    analcodi = "BIF",
                    analnome = "BILIRUBINA DIRETTA",
                    analflro = 0,
                    analinvi = 0,
                    analextb = "281",                    
                },
            };

            string errs = null;

            LISPlugin.LIS lis = new LISPlugin.LIS();

            string richid = lis.ScheduleNewRequest(even, pres, esam, anals, ref errs);
            //string richid = "8682285";

            MirthResponseDTO resp = lis.SubmitNewRequest(richid, ref errs);

            //PrestazioneDTO pres_ = lis.RetrievePresByEven("2182273");

            //PazienteDTO pazi = lis.RetrievePazi("571010");

            List<RisultatoDTO> riss = lis.RetrieveResults(richid, ref errs, true);

            /*
            string evenid = "2182265";
            string richid = "8682284";
            string analid1 = "4645449";
            string analid2 = "4645450";
            string analid3 = "4645451";
            */

            //List<RichiestaLISDTO> richs = lis.Check4Exams(evenid);
            //List<AnalisiDTO> anals_ = lis.Check4Analysis(richid);

            //MirthResponseDTO resp = lis.SubmitNewRequest(richid, ref errs);
            //List<LabelDTO> labes = lis.Check4Labels(richid);

            //List<RisultatoDTO> riss = lis.RetrieveResults(richid, ref errs);

            //List<RisultatoDTO> riss = lis.Check4Results(analid1);

            //bool cnl = lis.CheckIfCancelingIsAllowed(richid, ref errs);

            /*
            List<int> red = null;

            int r = 0;
            if (red != null && red.Count > 0)
                r = 1;
            */

            //RefertoDTO refe = lis.Check4Report(richid);

            sw.Stop();

            Console.WriteLine("ElapsedTime {0}\n\n", sw.Elapsed);

            Console.WriteLine("Press a key to Close!");
            Console.ReadKey();
        }                
    }
}
