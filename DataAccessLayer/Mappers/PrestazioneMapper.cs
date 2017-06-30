using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class PrestazioneMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static IDAL.VO.PrestazioneVO PresMapper(DataRow row)
        {
            IDAL.VO.PrestazioneVO pres = new IDAL.VO.PrestazioneVO();

            pres.presidid = row["presidid"] != DBNull.Value ? (int)row["presidid"] : (int?)null;
            pres.preseven = row["preseven"] != DBNull.Value ? (int)row["preseven"] : (int?)null;
            pres.presques = row["presques"] != DBNull.Value ? (string)row["presques"] : null;
            pres.prescons = row["prescons"] != DBNull.Value ? (string)row["prescons"] : null;
            pres.presstat = row["presstat"] != DBNull.Value ? (short)row["presstat"] : (short?)null;
            pres.prestipo = row["prestipo"] != DBNull.Value ? (int)row["prestipo"] : (int?)null;
            pres.presurge = row["presurge"] != DBNull.Value ? (bool)row["presurge"] : (bool?)null;
            pres.prespren = row["prespren"] != DBNull.Value ? (DateTime)row["prespren"] : (DateTime?)null;
            pres.presrico = row["presrico"] != DBNull.Value ? (int)row["presrico"] : (int?)null;
            pres.presesec = row["presesec"] != DBNull.Value ? (DateTime)row["presesec"] : (DateTime?)null;
            pres.presflcc = row["presflcc"] != DBNull.Value ? (int)row["presflcc"] : (int?)null;
            pres.presconf = row["presconf"] != DBNull.Value ? (int)row["presconf"] : (int?)null;
            pres.presdmod = row["presdmod"] != DBNull.Value ? (string)row["presdmod"] : null;
            pres.presnote = row["presnote"] != DBNull.Value ? (string)row["presnote"] : null;
            pres.presdtri = row["presdtri"] != DBNull.Value ? (DateTime)row["presdtri"] : (DateTime?)null;
            pres.presdtco = row["presdtco"] != DBNull.Value ? (DateTime)row["presdtco"] : (DateTime?)null;
            pres.prespers = row["prespers"] != DBNull.Value ? (string)row["prespers"] : null;
            pres.preserog = row["preserog"] != DBNull.Value ? (short)row["preserog"] : (short?)null;
            pres.prespren2 = row["prespren2"] != DBNull.Value ? (DateTime)row["prespren2"] : (DateTime?)null;
            pres.presdimi = row["presdimi"] != DBNull.Value ? (int)row["presdimi"] : (int?)null;
            pres.presecocardio = row["presecocardio"] != DBNull.Value ? (int)row["presecocardio"] : (int?)null;
            pres.presvisicardio = row["presvisicardio"] != DBNull.Value ? (int)row["presvisicardio"] : (int?)null;
            pres.presappu = row["presappu"] != DBNull.Value ? (string)row["presappu"] : null;
            pres.presannu = row["presannu"] != DBNull.Value ? (int)row["presannu"] : (int?)null;
            pres.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            pres.hl7_msg = row["hl7_msg"] != DBNull.Value ? (string)row["hl7_msg"] : null;
            pres.prespadre = row["prespadre"] != DBNull.Value ? (int)row["prespadre"] : (int?)null;
            pres.presconscardio = row["presconscardio"] != DBNull.Value ? (int)row["presconscardio"] : (int?)null;
            pres.prespagatipo = row["prespagatipo"] != DBNull.Value ? (int)row["prespagatipo"] : (int?)null;
            pres.prespagastat = row["prespagastat"] != DBNull.Value ? (int)row["prespagastat"] : (int?)null;
            pres.prespagadata = row["prespagadata"] != DBNull.Value ? (DateTime)row["prespagadata"] : (DateTime?)null;
            pres.prespagauser = row["prespagauser"] != DBNull.Value ? (int)row["prespagauser"] : (int?)null;
            pres.prescdc = row["prescdc"] != DBNull.Value ? (string)row["prescdc"] : null;
            pres.presrefe = row["presrefe"] != DBNull.Value ? (int)row["presrefe"] : (int?)null;
            pres.presimgstat = row["presimgstat"] != DBNull.Value ? (int)row["presimgstat"] : (int?)null;
            pres.presimg = row["presimg"] != DBNull.Value ? (byte[])row["presimg"] : null;
            pres.presacqu = row["presacqu"] != DBNull.Value ? (DateTime)row["presacqu"] : (DateTime?)null;
            
            return pres;
        }
        public static List<IDAL.VO.PrestazioneVO> PresMapper(DataTable rows)
        {
            List<IDAL.VO.PrestazioneVO> data = new List<IDAL.VO.PrestazioneVO>();

            if (rows != null)
            {
                if(rows.Rows.Count > 0)
                {
                    foreach(DataRow row in rows.Rows)
                    {
                        IDAL.VO.PrestazioneVO tmp = PresMapper(row);
                        data.Add(tmp);
                    }
                }
            }

            return data;
        }
    }
}
