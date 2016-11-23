using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Mappers
{
    public class LabelMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.LabelDTO LabeMapper(IDAL.VO.LabelVO raw)
        {
            IBLL.DTO.LabelDTO labe = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.LabelVO, IBLL.DTO.LabelDTO>());
                Mapper.AssertConfigurationIsValid();
                labe = Mapper.Map<IBLL.DTO.LabelDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return labe;
        }
        public static List<IBLL.DTO.LabelDTO> LabeMapper(List<IDAL.VO.LabelVO> raws)
        {
            List<IBLL.DTO.LabelDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.LabelDTO>();
                foreach (IDAL.VO.LabelVO raw in raws)
                {
                    res.Add(LabeMapper(raw));
                }
            }

            return res;
        }
        public static List<IDAL.VO.LabelVO> LabeMapper(List<IBLL.DTO.LabelDTO> raws)
        {
            List<IDAL.VO.LabelVO> res = null;

            if (raws != null)
            {
                res = new List<IDAL.VO.LabelVO>();
                foreach (IBLL.DTO.LabelDTO raw in raws)
                {
                    res.Add(LabeMapper(raw));
                }
            }

            return res;
        }
        public static IDAL.VO.LabelVO LabeMapper(IBLL.DTO.LabelDTO data)
        {
            IDAL.VO.LabelVO labe = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.LabelDTO, IDAL.VO.LabelVO>());
                Mapper.AssertConfigurationIsValid();
                labe = Mapper.Map<IDAL.VO.LabelVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return labe;
        }
        public static IBLL.DTO.LabelDTO LabeMapper(object[] data)
        {
            IBLL.DTO.LabelDTO labe = new IBLL.DTO.LabelDTO();

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
        public static List<IBLL.DTO.LabelDTO> LabeMapper(string raw)
        {
            List<IBLL.DTO.LabelDTO> data = null;

            if (raw != null)
            {
                try
                {
                    data = new List<IBLL.DTO.LabelDTO>();
                    /*
                    int st = raw.IndexOf("ZET");
                    string ZETsubstring = raw.Substring(st);

                    string[] tmp = ZETsubstring.Split(new string[] { "ZET" }, StringSplitOptions.RemoveEmptyEntries);
                    */

                    List<string> res = GeneralPurposeLib.LibString.GetAllValuesSegments(raw, "ZET");
                    //res = new List<string>(tmp);
                    //res = res.Select(str => str.Replace(Environment.NewLine, "")).ToList();

                    foreach (string r in res)
                    {
                        object[] tmp2 = r.Split('|');
                        IBLL.DTO.LabelDTO tmp3 = LabeMapper(tmp2);
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
