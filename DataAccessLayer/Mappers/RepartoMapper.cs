using System;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class RepartoMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        

        public static IDAL.VO.RepartoVO RepaMapper(DataRow row)
        {
            IDAL.VO.RepartoVO repa = new IDAL.VO.RepartoVO();

            repa.repaidid = row["repaidid"] != DBNull.Value ? (int?)row["repaidid"] : null;
            repa.repaazie = row["repaazie"] != DBNull.Value ? (int?)row["repaazie"] : null;
            repa.repanome = row["repanome"] != DBNull.Value ? (string)row["repanome"] : null;
            repa.repapsps = row["repapsps"] != DBNull.Value ? (string)row["repapsps"] : null;
            repa.repatipo = row["repatipo"] != DBNull.Value ? (string)row["repatipo"] : null;
            repa.repamenu = row["repamenu"] != DBNull.Value ? (int?)row["repamenu"] : null;
            repa.repapreo = row["repapreo"] != DBNull.Value ? (int?)row["repapreo"] : null;
            repa.repadisu = row["repadisu"] != DBNull.Value ? (int?)row["repadisu"] : null;
            repa.repadisd = row["repadisd"] != DBNull.Value ? (int?)row["repadisd"] : null;
            repa.repapeag = row["repapeag"] != DBNull.Value ? (int?)row["repapeag"] : null;
            repa.repadtag = row["repadtag"] != DBNull.Value ? (DateTime?)row["repadtag"] : null;
            repa.repacod1 = row["repacod1"] != DBNull.Value ? (string)row["repacod1"] : null;
            repa.repacod2 = row["repacod2"] != DBNull.Value ? (string)row["repacod2"] : null;
            repa.repanlet = row["repanlet"] != DBNull.Value ? (int?)row["repanlet"] : null;
            repa.repadest = row["repadest"] != DBNull.Value ? (string)row["repadest"] : null;
            repa.repaanrp = row["repaanrp"] != DBNull.Value ? (string)row["repaanrp"] : null;
            repa.reparela = row["reparela"] != DBNull.Value ? (int?)row["reparela"] : null;
            repa.repaceco = row["repaceco"] != DBNull.Value ? (string)row["repaceco"] : null;
            repa.repaserv = row["repaserv"] != DBNull.Value ? (bool?)row["repaserv"] : null;
            repa.repaescs = row["repaescs"] != DBNull.Value ? (bool?)row["repaescs"] : null;
            repa.repacdc = row["repacdc"] != DBNull.Value ? (string)row["repacdc"] : null;
            repa.repacdc2 = row["repacdc2"] != DBNull.Value ? (string)row["repacdc2"] : null;
            repa.repauocc = row["repauocc"] != DBNull.Value ? (string)row["repauocc"] : null;
            repa.repaceconp = row["repaceconp"] != DBNull.Value ? (string)row["repaceconp"] : null;
            repa.repaturn = row["repaturn"] != DBNull.Value ? (bool?)row["repaturn"] : null;
            repa.repacecoold = row["repacecoold"] != DBNull.Value ? (string)row["repacecoold"] : null;
            repa.repacecooldata = row["repacecooldata"] != DBNull.Value ? (DateTime?)row["repacecooldata"] : null;

            return repa;
        }
        
    }
}
