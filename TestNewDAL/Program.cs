using System;
using GeneralPurposeLib;
using System.Collections.Generic;

namespace TestNewDAL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inizio Test ... ");
                        
            DataAccessLayer.LISDAL dal = new DataAccessLayer.LISDAL();
            IDAL.VO.AnalisiVO data = new IDAL.VO.AnalisiVO();
            data.analcodi = "32434";
            data.analesam = 123;
            data.analnome = "Gattica";

            IDAL.VO.AnalisiVO data2 = new IDAL.VO.AnalisiVO();
            data2.analcodi = "544544";
            data2.analesam = 983;
            data2.analnome = "Delezione Rettale";

            IDAL.VO.AnalisiVO data3 = new IDAL.VO.AnalisiVO();
            data3.analcodi = "101020";
            data3.analesam = 3430;
            data3.analnome = "Gesù";

            List<IDAL.VO.AnalisiVO> datas =  dal.NewAnalisi(new List<IDAL.VO.AnalisiVO>() { data, data2, data3 });
            

            /*
            string v1 = "refew";
            bool v2 = true;
            double v3 = 23.4432;

            SqlParameter p = new SqlParameter();
            p.Value = "cieiroeieor";

            bool isNum1 = LibString.IsNumericType(v1);
            bool isNum2 = LibString.IsNumericType(v2);
            bool isNum3 = LibString.IsNumericType(v3);
            bool isNum4 = LibString.IsNumericType(p.Value);
            */

            Console.WriteLine("Premere un tasto per continuare ... ");
            Console.ReadKey();
            Console.WriteLine("Test Concluso!");
        }
    }
}
