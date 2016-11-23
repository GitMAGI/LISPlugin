using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class RisultatoMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static IDAL.VO.RisultatoGrezzoVO AnreTrashMapper(DataRow row)
        {
            IDAL.VO.RisultatoGrezzoVO anreT = new IDAL.VO.RisultatoGrezzoVO();

            anreT.esamanlid = row["esamanalid"] != DBNull.Value ? (string)row["esamanalid"] : null;
            anreT.res = row["res"] != DBNull.Value ? (string)row["res"] : null;            

            return anreT;
        }

        public static IDAL.VO.RisultatoVO AnreMapper(DataRow row)
        {
            IDAL.VO.RisultatoVO anre = new IDAL.VO.RisultatoVO();

            anre.anreidid = row["anreidid"] != DBNull.Value ? (long)row["anreidid"] : (long?)null;
            anre.anreanal = row["anreanal"] != DBNull.Value ? (long)row["anreanal"] : (long?)null;
            anre.anreprog = row["anreprog"] != DBNull.Value ? (int)row["anreprog"] : (int?)null;
            anre.anretipo = row["anretipo"] != DBNull.Value ? (string)row["anretipo"] : null;
            anre.anreidsn = row["anreidsn"] != DBNull.Value ? (string)row["anreidsn"] : null;
            anre.anreidsn = row["anredsmu"] != DBNull.Value ? (string)row["anredsmu"] : null;
            anre.anreidmu = row["anreidmu"] != DBNull.Value ? (string)row["anreidmu"] : null;
            anre.anrerisu = row["anrerisu"] != DBNull.Value ? (string)row["anrerisu"] : null;
            anre.anrerisucomm = row["anrerisucomm"] != DBNull.Value ? (string)row["anrerisucomm"] : null;
            anre.anreunim = row["anreunim"] != DBNull.Value ? (string)row["anreunim"] : null;
            anre.anrerife = row["anrerife"] != DBNull.Value ? (string)row["anrerife"] : null;
            anre.anreanom = row["anreanom"] != DBNull.Value ? (string)row["anreanom"] : null;
            anre.anrestat = row["anrestat"] != DBNull.Value ? (string)row["anrestat"] : null;
            anre.anredata = row["anredata"] != DBNull.Value ? (DateTime)row["anredata"] : (DateTime?)null;

            return anre;
        }

        public static List<IDAL.VO.RisultatoVO> AnreMapper(DataTable rows)
        {
            List<IDAL.VO.RisultatoVO> riss = null;

            foreach(DataRow row in rows.Rows)
            {
                IDAL.VO.RisultatoVO ris = AnreMapper(row);
                if (riss == null)
                    riss = new List<IDAL.VO.RisultatoVO>();
                riss.Add(ris);
            }

            return riss;
        }
    }
}
