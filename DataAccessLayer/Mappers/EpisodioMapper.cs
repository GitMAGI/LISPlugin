using System;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class EpisodioMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        

        public static IDAL.VO.EpisodioVO EpisMapper(DataRow row)
        {
            IDAL.VO.EpisodioVO epis = new IDAL.VO.EpisodioVO();

            epis.episidid = row["EPISIDID"] != DBNull.Value ? (int)row["EPISIDID"] : (int?)null;
            epis.epispazi = row["EPISPAZI"] != DBNull.Value ? (int)row["EPISPAZI"] : (int?)null;
            epis.epistipo = row["EPISTIPO"] != DBNull.Value ? (int)row["EPISTIPO"] : (int?)null;
            epis.episdain = row["EPISDAIN"] != DBNull.Value ? (DateTime)row["EPISDAIN"] : (DateTime?)null;
            epis.episdafi = row["EPISDAFI"] != DBNull.Value ? (DateTime)row["EPISDAFI"] : (DateTime?)null;
            epis.epischiu = row["EPISCHIU"] != DBNull.Value ? (bool)row["EPISCHIU"] : (bool?)null;
            epis.episrepa = row["EPISREPA"] != DBNull.Value ? (int)row["EPISREPA"] : (int?)null;
            epis.episreap = row["EPISREAP"] != DBNull.Value ? (int)row["EPISREAP"] : (int?)null;
            epis.epislett = row["EPISLETT"] != DBNull.Value ? (int)row["EPISLETT"] : (int?)null;
            epis.episprec = row["EPISPREC"] != DBNull.Value ? (int)row["EPISPREC"] : (int?)null;
            epis.episcart = row["EPISCART"] != DBNull.Value ? (int)row["EPISCART"] : (int?)null;
            epis.episisti = row["EPISISTI"] != DBNull.Value ? (int)row["EPISISTI"] : (int?)null;
            epis.episisca = row["EPISISCA"] != DBNull.Value ? (string)row["EPISISCA"] : null;
            epis.episckck = row["EPISCKCK"] != DBNull.Value ? (int)row["EPISCKCK"] : (int?)null;
            epis.episscre = row["EPISSCRE"] != DBNull.Value ? (bool)row["EPISSCRE"] : (bool?)null;
            epis.episscdt = row["EPISSCDT"] != DBNull.Value ? (DateTime)row["EPISSCDT"] : (DateTime?)null;
            epis.episscpe = row["EPISSCPE"] != DBNull.Value ? (string)row["EPISSCPE"] : null;
            epis.epissnot = row["EPISSnot"] != DBNull.Value ? (string)row["EPISSnot"] : null;
            epis.send_to_ris = row["SEND_TO_RIS"] != DBNull.Value ? (int)row["SEND_TO_RIS"] : (int?)null;
            epis.episoldpazi = row["EPISOLDPAZI"] != DBNull.Value ? (int)row["EPISOLDPAZI"] : (int?)null;
            epis.episintens = row["episintens"] != DBNull.Value ? (int)row["episintens"] : (int?)null;
            epis.episdataintens = row["EPISDATAINTENS"] != DBNull.Value ? (DateTime)row["EPISDATAINTENS"] : (DateTime?)null;
            epis.episutenintes = row["EPISUTENINTES"] != DBNull.Value ? (string)row["EPISUTENINTES"] : null;

            return epis;
        }
        
    }
}
