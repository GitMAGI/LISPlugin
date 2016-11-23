using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccessLayer.Mappers
{
    public class LabelMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static IDAL.VO.LabelVO LabeMapper(DataRow row)
        {
            IDAL.VO.LabelVO labe = new IDAL.VO.LabelVO();

            labe.labeidid = row["LABEIDID"] != DBNull.Value ? (long)row["LABEIDID"] : (long?)null;
            labe.labebarcode = row["LABEBARCODE"] != DBNull.Value ? (string)row["LABEBARCODE"] : null;
            labe.labeidcont = row["LABEIDCONT"] != DBNull.Value ? (string)row["LABEIDCONT"] : null;
            labe.labedesc = row["LABEDESC"] != DBNull.Value ? (string)row["LABEDESC"] : null;
            labe.labeidlab = row["LABEIDLAB"] != DBNull.Value ? (string)row["LABEIDLAB"] : null;
            labe.labeesam = row["LABEESAM"] != DBNull.Value ? (long)row["LABEESAM"] : (long?)null;
            labe.labeesamacce = row["LABEESAMACCE"] != DBNull.Value ? (DateTime)row["LABEESAMACCE"] : (DateTime?)null;
            labe.labepaziid = row["LABEPAZIID"] != DBNull.Value ? (long)row["LABEPAZIID"] : (long?)null;
            labe.labereri = row["LABERERI"] != DBNull.Value ? (int)row["LABERERI"] : (int?)null;
            labe.labrerinome = row["LABRERINOME"] != DBNull.Value ? (string)row["LABRERINOME"] : null;
            labe.labeacceid = row["LABEACCEID"] != DBNull.Value ? (string)row["LABEACCEID"] : null;
            labe.labemateid = row["LABEMATEID"] != DBNull.Value ? (string)row["LABEMATEID"] : null;
            labe.labeelenanal = row["LABEELENANAL"] != DBNull.Value ? (string)row["LABEELENANAL"] : null;
            labe.labesectid = row["LABESECTID"] != DBNull.Value ? (string)row["LABESECTID"] : null;
            labe.labesectnome = row["LABESECTNOME"] != DBNull.Value ? (string)row["LABESECTNOME"] : null;
            labe.labedaorprel = row["LABEDAORPREL"] != DBNull.Value ? (DateTime)row["LABEDAORPREL"] : (DateTime?)null;

            return labe;
        }
        public static List<IDAL.VO.LabelVO> LabeMapper(DataTable rows)
        {
            List<IDAL.VO.LabelVO> data = new List<IDAL.VO.LabelVO>();

            if (rows != null)
            {
                if(rows.Rows.Count > 0)
                {
                    foreach(DataRow row in rows.Rows)
                    {
                        IDAL.VO.LabelVO tmp = LabeMapper(row);
                        data.Add(tmp);
                    }
                }
            }

            return data;
        }
        public static IDAL.VO.LabelVO LabeMapper(object[] data)
        {
            IDAL.VO.LabelVO labe = new IDAL.VO.LabelVO();

            try
            {
                labe.labeesam = long.Parse((string)data[0]);
            }
            catch (Exception)
            {
                labe.labeesam = null;
            }
            labe.labebarcode = data[1] != null ? (string)data[1] : null;
            labe.labedesc = data[2] != null ? (string)data[2] : null;
            labe.labeidcont = data[3] != null ? (string)data[3] : null;
            labe.labeidlab = data[6] != null ? (string)data[6] : null;
            //labe.idReq = (string)data[7];            
            try
            {
                labe.labeesamacce = DateTime.ParseExact(((string)data[8]), "yyyyMMddHHmm", null);
            }
            catch (Exception)
            {
                labe.labeesamacce = null;
            }
            try
            {
                labe.labereri = int.Parse((string)data[17]);
            }
            catch (Exception)
            {
                labe.labereri = null;
            }
            labe.labrerinome = data[18] != null ? (string)data[18] : null;
            labe.labeacceid = data[19] != null ? (string)data[19] : null;
            labe.labemateid = data[20] != null ? (string)data[20] : null;
            labe.labeelenanal = data[22] != null ? (string)data[22] : null;
            labe.labesectid = data[23] != null ? (string)data[23] : null;
            labe.labesectnome = data[24] != null ? (string)data[24] : null;
            try
            {
                labe.labedaorprel = DateTime.ParseExact(((string)data[25]), "yyyyMMddHHmm", null);
            }
            catch (Exception)
            {
                labe.labedaorprel = null;
            }

            return labe;
        }
        public static List<IDAL.VO.LabelVO> LabeMapper(string raw)
        {
            List<IDAL.VO.LabelVO> data = null;

            if (raw != null)
            {
                try
                {
                    data = new List<IDAL.VO.LabelVO>();

                    int st = raw.IndexOf("ZET");
                    string ZETsubstring = raw.Substring(st);

                    string[] tmp = ZETsubstring.Split(new string[] { "ZET" }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> res = null;
                    res = new List<string>(tmp);
                    res = res.Select(str => str.Replace(Environment.NewLine, "")).ToList();

                    foreach (string r in res)
                    {
                        object[] tmp2 = r.Split('|');
                        IDAL.VO.LabelVO tmp3 = LabeMapper(tmp2);
                        data.Add(tmp3);
                    }
                }
                catch (Exception)
                {

                }
            }
            return data;
        }
    }
}
