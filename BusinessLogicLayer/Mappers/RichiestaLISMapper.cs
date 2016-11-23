using AutoMapper;
using System.Collections.Generic;

namespace BusinessLogicLayer.Mappers
{
    public class RichiestaLISMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.RichiestaLISDTO RichMapper(IDAL.VO.RichiestaLISVO raw)
        {
            IBLL.DTO.RichiestaLISDTO rich = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.RichiestaLISVO, IBLL.DTO.RichiestaLISDTO>());
                Mapper.AssertConfigurationIsValid();
                rich = Mapper.Map<IBLL.DTO.RichiestaLISDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return rich;
        }
        public static List<IBLL.DTO.RichiestaLISDTO> RichMapper(List<IDAL.VO.RichiestaLISVO> raws)
        {
            List<IBLL.DTO.RichiestaLISDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.RichiestaLISDTO>();
                foreach (IDAL.VO.RichiestaLISVO raw in raws)
                {
                    res.Add(RichMapper(raw));
                }
            }

            return res;
        }
        public static IDAL.VO.RichiestaLISVO RichMapper(IBLL.DTO.RichiestaLISDTO dto)
        {
            IDAL.VO.RichiestaLISVO rich = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.RichiestaLISDTO, IDAL.VO.RichiestaLISVO>());
                Mapper.AssertConfigurationIsValid();
                rich = Mapper.Map<IDAL.VO.RichiestaLISVO>(dto);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return rich;
        }
        public static List<IDAL.VO.RichiestaLISVO> RichMapper(List<IBLL.DTO.RichiestaLISDTO> dtos)
        {
            List<IDAL.VO.RichiestaLISVO> res = null;

            if (dtos != null)
            {
                res = new List<IDAL.VO.RichiestaLISVO>();
                foreach (IBLL.DTO.RichiestaLISDTO dto in dtos)
                {
                    res.Add(RichMapper(dto));
                }
            }

            return res;
        }

    }
}
