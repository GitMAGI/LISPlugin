using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class AnalisiMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static IDAL.VO.AnalisiVO AnalMapper(DataRow row)
        {
            IDAL.VO.AnalisiVO anal = new IDAL.VO.AnalisiVO();

            anal.analidid = row["analidid"] != DBNull.Value ? (int)row["analidid"] : (int?)null;
            anal.analesam = row["analesam"] != DBNull.Value ? (int)row["analesam"] : (int?)null;
            anal.analcodi = row["analcodi"] != DBNull.Value ? (string)row["analcodi"] : null;
            anal.analnome = row["analnome"] != DBNull.Value ? (string)row["analnome"] : null;
            anal.analinvi = row["analinvi"] != DBNull.Value ? (short)row["analinvi"] : (short?)null;
            anal.analflro = row["analflro"] != DBNull.Value ? (short)row["analflro"] : (short?)null;
            anal.analextb = row["analextb"] != DBNull.Value ? (string)row["analextb"] : null;
            anal.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            anal.hl7_msg = row["hl7_msg"] != DBNull.Value ? (string)row["hl7_msg"] : null;

            return anal;
        }
        public static List<IDAL.VO.AnalisiVO> AnalMapper(DataTable rows)
        {
            List<IDAL.VO.AnalisiVO> data = new List<IDAL.VO.AnalisiVO>();

            if (rows != null)
            {
                if(rows.Rows.Count > 0)
                {
                    foreach(DataRow row in rows.Rows)
                    {
                        IDAL.VO.AnalisiVO tmp = AnalMapper(row);
                        data.Add(tmp);
                    }
                }
            }

            return data;
        }
    }
}
