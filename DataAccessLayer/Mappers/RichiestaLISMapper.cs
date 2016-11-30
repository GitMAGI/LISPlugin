using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class RichiestaLISMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<IDAL.VO.RichiestaLISVO> RichMapper(DataTable rows)
        {
            List<IDAL.VO.RichiestaLISVO> rich = null;
            if (rows != null)
            {
                rich = new List<IDAL.VO.RichiestaLISVO>();
                foreach (DataRow row in rows.Rows)
                {
                    rich.Add(RichMapper(row));
                }
            }
            return rich;
        }
        public static IDAL.VO.RichiestaLISVO RichMapper(DataRow row)
        {
            IDAL.VO.RichiestaLISVO esam = new IDAL.VO.RichiestaLISVO();

            esam.esamidid = row["esamidid"] != DBNull.Value ? (int)row["esamidid"] : (int?)null;
            esam.esameven = row["esameven"] != DBNull.Value ? (int)row["esameven"] : (int?)null;
            esam.esamdapr = row["esamdapr"] != DBNull.Value ? (DateTime)row["esamdapr"] : (DateTime?)null;
            esam.esamorpr = row["esamorpr"] != DBNull.Value ? (DateTime)row["esamorpr"] : (DateTime?)null;
            esam.esamurge = row["esamurge"] != DBNull.Value ? (int)row["esamurge"] : (int?)null;
            esam.esamrout = row["esamrout"] != DBNull.Value ? (string)row["esamrout"] : null;
            esam.esamesec = row["esamesec"] != DBNull.Value ? (string)row["esamesec"] : null;
            esam.esamtipo = row["esamtipo"] != DBNull.Value ? (int)row["esamtipo"] : (int?)null;
            esam.esampren = row["esampren"] != DBNull.Value ? (DateTime)row["esampren"] : (DateTime?)null;
            esam.esamrico = row["esamrico"] != DBNull.Value ? (int)row["esamrico"] : (int?)null;
            esam.esamconf = row["esamconf"] != DBNull.Value ? (int)row["esamconf"] : (int?)null;
            esam.esamdmod = row["esamdmod"] != DBNull.Value ? (string)row["esamdmod"] : null;
            esam.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;

            return esam; ;
        }
    }
}