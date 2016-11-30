using System;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class PazienteMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static IDAL.VO.PazienteVO PaziMapper(DataRow row)
        {
            IDAL.VO.PazienteVO pazi = new IDAL.VO.PazienteVO();

            pazi.paziidid = row["PAZIIDID"] != DBNull.Value ? (int)row["PAZIIDID"] : (int?)null;
            pazi.pazirepa = row["PAZIREPA"] != DBNull.Value ? (int)row["PAZIREPA"] : (int?)null;
            pazi.pazinome = row["PAZINOME"] != DBNull.Value ? (string)row["PAZINOME"] : null;
            pazi.pazicogn = row["PAZICOGN"] != DBNull.Value ? (string)row["PAZICOGN"] : null;
            pazi.pazisess = row["PAZISESS"] != DBNull.Value ? (string)row["PAZISESS"] : null;
            pazi.pazicofi = row["PAZICOFI"] != DBNull.Value ? (string)row["PAZICOFI"] : null;
            pazi.pazicops = row["PAZICOPS"] != DBNull.Value ? (string)row["PAZICOPS"] : null;
            pazi.pazicaps = row["PAZICAPS"] != DBNull.Value ? (string)row["PAZICAPS"] : null;
            pazi.pazidata = row["PAZIDATA"] != DBNull.Value ? (DateTime)row["PAZIDATA"] : (DateTime?)null;
            pazi.pazitele = row["PAZITELE"] != DBNull.Value ? (string)row["PAZITELE"] : null;
            pazi.pazitel2 = row["PAZITEL2"] != DBNull.Value ? (string)row["PAZITEL2"] : null;
            pazi.pazicomu = row["PAZICOMU"] != DBNull.Value ? (string)row["PAZICOMU"] : null;
            pazi.paziprov = row["PAZIPROV"] != DBNull.Value ? (string)row["PAZIPROV"] : null;
            pazi.paziviaa = row["PAZIVIAA"] != DBNull.Value ? (string)row["PAZIVIAA"] : null;
            pazi.pazibrr1 = row["PAZIBRR1"] != DBNull.Value ? (int)row["PAZIBRR1"] : (int?)null;
            pazi.pazibrr2 = row["PAZIBRR2"] != DBNull.Value ? (int)row["PAZIBRR2"] : (int?)null;
            pazi.pazibrr3 = row["PAZIBRR3"] != DBNull.Value ? (int)row["PAZIBRR3"] : (int?)null;
            pazi.pazibrr4 = row["PAZIBRR4"] != DBNull.Value ? (int)row["PAZIBRR4"] : (int?)null;
            pazi.pazibrr5 = row["PAZIBRR5"] != DBNull.Value ? (int)row["PAZIBRR5"] : (int?)null;
            pazi.paziregi = row["PAZIREGI"] != DBNull.Value ? (string)row["PAZIREGI"] : null;
            pazi.pazimedi = row["PAZIMEDI"] != DBNull.Value ? (string)row["PAZIMEDI"] : null;
            pazi.paziasll = row["PAZIASLL"] != DBNull.Value ? (string)row["PAZIASLL"] : null;
            pazi.pazistci = row["PAZISTCI"] != DBNull.Value ? (string)row["PAZISTCI"] : null;
            pazi.pazicond = row["PAZICOND"] != DBNull.Value ? (string)row["PAZICOND"] : null;
            pazi.paziposi = row["PAZIPOSI"] != DBNull.Value ? (string)row["PAZIPOSI"] : null;
            pazi.paziramo = row["PAZIRAMO"] != DBNull.Value ? (string)row["PAZIRAMO"] : null;
            pazi.pazitito = row["PAZITITO"] != DBNull.Value ? (string)row["PAZITITO"] : null;
            pazi.pazicapp = row["PAZICAPP"] != DBNull.Value ? (string)row["PAZICAPP"] : null;
            pazi.pazictnz = row["PAZICTNZ"] != DBNull.Value ? (string)row["PAZICTNZ"] : null;
            pazi.paziresi = row["PAZIRESI"] != DBNull.Value ? (string)row["PAZIRESI"] : null;
            pazi.pazicirc = row["PAZICIRC"] != DBNull.Value ? (string)row["PAZICIRC"] : null;
            pazi.pazimadr = row["PAZIMADR"] != DBNull.Value ? (int)row["PAZIMADR"] : (int?)null;
            pazi.pazirelo = row["PAZIRELO"] != DBNull.Value ? (DateTime)row["PAZIRELO"] : (DateTime?)null;
            pazi.paziisti = row["PAZIISTI"] != DBNull.Value ? (int)row["PAZIISTI"] : (int?)null;
            pazi.paziisca = row["PAZIISCA"] != DBNull.Value ? (string)row["PAZIISCA"] : null;
            pazi.hltprocess = row["HLTPROCESS"] != DBNull.Value ? (int)row["HLTPROCESS"] : (int?)null;
            pazi.pazieni = row["pazieni"] != DBNull.Value ? (string)row["pazieni"] : null;
            pazi.paziteam = row["paziteam"] != DBNull.Value ? (string)row["paziteam"] : null;
            pazi.codicepowerlab = row["codicepowerlab"] != DBNull.Value ? (string)row["codicepowerlab"] : null;
            pazi.pazimecd = row["pazimecd"] != DBNull.Value ? (string)row["pazimecd"] : null;
            pazi.pazisorg = row["pazisorg"] != DBNull.Value ? (string)row["pazisorg"] : null;
            pazi.paziidext = row["paziidext"] != DBNull.Value ? (string)row["paziidext"] : null;
            pazi.paziturn = row["paziturn"] != DBNull.Value ? (int)row["paziturn"] : (int?)null;
            pazi.paziperi = row["paziperi"] != DBNull.Value ? (int)row["paziperi"] : (int?)null;
            pazi.pazimaster = row["pazimaster"] != DBNull.Value ? (int)row["pazimaster"] : (int?)null;
            pazi.pazimergedata = row["pazimergedata"] != DBNull.Value ? (DateTime)row["pazimergedata"] : (DateTime?)null;
            pazi.pazidistr = row["pazidistr"] != DBNull.Value ? (string)row["pazidistr"] : null;
            pazi.paziaslcode = row["paziaslcode"] != DBNull.Value ? (string)row["paziaslcode"] : null;

            return pazi;
        }
    }
}
