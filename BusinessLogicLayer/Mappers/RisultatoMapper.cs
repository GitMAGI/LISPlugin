using AutoMapper;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Mappers
{
    public class RisultatoMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.RisultatoDTO AnreMapper(IDAL.VO.RisultatoVO raw)
        {
            IBLL.DTO.RisultatoDTO anre = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.RisultatoVO, IBLL.DTO.RisultatoDTO>());
                Mapper.AssertConfigurationIsValid();
                anre = Mapper.Map<IBLL.DTO.RisultatoDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return anre;
        }

        public static List<IBLL.DTO.RisultatoDTO> AnreMapper(List<IDAL.VO.RisultatoVO> raws)
        {
            List<IBLL.DTO.RisultatoDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.RisultatoDTO>();
                foreach (IDAL.VO.RisultatoVO raw in raws)
                {
                    res.Add(AnreMapper(raw));
                }
            }

            return res;
        }

        public static IDAL.VO.RisultatoVO AnreMapper(IBLL.DTO.RisultatoDTO data)
        {
            IDAL.VO.RisultatoVO anre = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.RisultatoDTO, IDAL.VO.RisultatoVO>());
                Mapper.AssertConfigurationIsValid();
                anre = Mapper.Map<IDAL.VO.RisultatoVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return anre;
        }

        public static List<IDAL.VO.RisultatoVO> AnreMapper(List<IBLL.DTO.RisultatoDTO> dtos)
        {
            List<IDAL.VO.RisultatoVO> res = null;

            if (dtos != null)
            {
                res = new List<IDAL.VO.RisultatoVO>();
                foreach (IBLL.DTO.RisultatoDTO raw in dtos)
                {
                    res.Add(AnreMapper(raw));
                }
            }

            return res;
        }

        public static List<IBLL.DTO.RisultatoDTO> AnreMapper(IDAL.VO.RisultatoGrezzoVO raw)
        {
            List<IBLL.DTO.RisultatoDTO> anres = null;

            string rowsSeparator = "££";
            string fieldsSeparator = "§§";

            try
            {
                string[] tmp = raw.esamanlid.Split('-');
                string[] rows = raw.res.Split(new string[] { rowsSeparator }, StringSplitOptions.RemoveEmptyEntries);

                string analid = tmp[1].Trim();
                string esamid = tmp[0].Trim();

                long analid_ = 0;
                if (!long.TryParse(analid, out analid_))
                {
                    log.Error(string.Format("Error during Anal ID parsing. {0} is not a long parsable string! Esam ID: {1}", analid, esamid));                    
                }                

                foreach (string row in rows)
                {
                    string[] fields = row.Split(new string[] { fieldsSeparator }, StringSplitOptions.None);

                    IBLL.DTO.RisultatoDTO anre = new IBLL.DTO.RisultatoDTO();

                    anre.anreanal = analid_;

                    int anreprog_ = 0;
                    if (!int.TryParse(fields[0].Trim(), out anreprog_))
                    {
                        log.Info(string.Format("Error during Prog Anal ID parsing. {0} is not an int parsable string! Esam ID: {1} - Anal ID: {2}", fields[0], analid, esamid));
                    }
                    anre.anreprog = anreprog_;

                    anre.anretipo = fields[1].Trim();                    
                    anre.anreidmu = fields[2].Trim();
                    anre.anredsmu = fields[3].Trim();
                    anre.anreidsn = fields[4].Trim();
                    anre.anrerisu = fields[5].Trim();
                    anre.anrerisucomm = fields[6].Trim();
                    anre.anreunim = fields[7].Trim();
                    anre.anrerife = fields[8].Trim();
                    anre.anreanom = fields[9].Trim();
                    anre.anrestat = fields[10].Trim();

                    DateTime anredata_ = default(DateTime);
                    try
                    {
                        anredata_ = DateTime.ParseExact(fields[11].Trim(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        log.Info(string.Format("Error during Anal Date parsing. {0} is not a DateTime parsable string! Esam ID: {1} - Anal ID: {2}", fields[9], analid, esamid));
                    }                    
                    anre.anredata = anredata_;

                    if (anres == null)
                        anres = new List<IBLL.DTO.RisultatoDTO>();

                    anres.Add(anre);
                }

            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error Occurred!\n{0}", ex.Message));
            }

            return anres;
        }            
    }
}
