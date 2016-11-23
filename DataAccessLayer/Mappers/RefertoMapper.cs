using System;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class RefertoMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        

        public static IDAL.VO.RefertoVO RefeMapper(DataRow row)
        {
            IDAL.VO.RefertoVO refe = new IDAL.VO.RefertoVO();

            refe.refeidid = row["refeidid"] != DBNull.Value ? (int)row["refeidid"] : (int?)null;
            refe.refedocu = row["refedocu"] != DBNull.Value ? (string)row["refedocu"] : null;
            refe.refemeno = row["refemeno"] != DBNull.Value ? (string)row["refemeno"] : null;
            refe.refemecf = row["refemecf"] != DBNull.Value ? (string)row["refemecf"] : null;
            refe.refedata = row["refedata"] != DBNull.Value ? (DateTime)row["refedata"] : (DateTime?)null;
            refe.refeidid = row["refeesam"] != DBNull.Value ? (int)row["refeesam"] : (int?)null;

            return refe;
        }
        
    }
}
