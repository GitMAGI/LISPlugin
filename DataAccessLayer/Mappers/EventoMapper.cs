using System;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class EventoMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        

        public static IDAL.VO.EventoVO EvenMapper(DataRow row)
        {
            IDAL.VO.EventoVO even = new IDAL.VO.EventoVO();

            even.evenidid = row["evenidid"] != DBNull.Value ? (int)row["evenidid"] : (int?)null;
            even.evenepis = row["evenepis"] != DBNull.Value ? (int)row["evenepis"] : (int?)null;
            even.eventipo = row["eventipo"] != DBNull.Value ? (int)row["eventipo"] : (int?)null;
            even.evenpepr = row["evenpepr"] != DBNull.Value ? (int)row["evenpepr"] : (int?)null;
            even.evenperi = row["evenperi"] != DBNull.Value ? (int)row["evenperi"] : (int?)null;
            even.evenreri = row["evenreri"] != DBNull.Value ? (int)row["evenreri"] : (int?)null;
            even.evenpees = row["evenpees"] != DBNull.Value ? (int)row["evenpees"] : (int?)null;
            even.evenrees = row["evenrees"] != DBNull.Value ? (int)row["evenrees"] : (int?)null;
            even.evendata = row["evendata"] != DBNull.Value ? (DateTime)row["evendata"] : (DateTime?)null;
            even.evenflst = row["evenflst"] != DBNull.Value ? (short)row["evenflst"] : (short?)null;
            even.evendasc = row["evendasc"] != DBNull.Value ? (DateTime)row["evendasc"] : (DateTime?)null;
            even.evenrepp = row["evenrepp"] != DBNull.Value ? (int)row["evenrepp"] : (int?)null;
            even.evencart = row["evencart"] != DBNull.Value ? (int)row["evencart"] : (int?)null;
            even.evencaps = row["evencaps"] != DBNull.Value ? (string)row["evencaps"] : null;
            even.evendaef = row["evendaef"] != DBNull.Value ? (DateTime)row["evendaef"] : (DateTime?)null;

            return even;
        }
        
    }
}
