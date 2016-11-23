using AutoMapper;
using System.Collections.Generic;

namespace BusinessLogicLayer.Mappers
{
    public class AnalisiMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.AnalisiDTO AnalMapper(IDAL.VO.AnalisiVO raw)
        {
            IBLL.DTO.AnalisiDTO esam = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.AnalisiVO, IBLL.DTO.AnalisiDTO>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<IBLL.DTO.AnalisiDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }

        public static List<IBLL.DTO.AnalisiDTO> AnalMapper(List<IDAL.VO.AnalisiVO> raws)
        {
            List<IBLL.DTO.AnalisiDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.AnalisiDTO>();
                foreach (IDAL.VO.AnalisiVO raw in raws)
                {
                    res.Add(AnalMapper(raw));
                }
            }

            return res;
        }

        public static IDAL.VO.AnalisiVO AnalMapper(IBLL.DTO.AnalisiDTO data)
        {
            IDAL.VO.AnalisiVO esam = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.AnalisiDTO, IDAL.VO.AnalisiVO>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<IDAL.VO.AnalisiVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }

        public static List<IDAL.VO.AnalisiVO> AnalMapper(List<IBLL.DTO.AnalisiDTO> dtos)
        {
            List<IDAL.VO.AnalisiVO> res = null;

            if (dtos != null)
            {
                res = new List<IDAL.VO.AnalisiVO>();
                foreach (IBLL.DTO.AnalisiDTO raw in dtos)
                {
                    res.Add(AnalMapper(raw));
                }
            }

            return res;
        }

    }
}
