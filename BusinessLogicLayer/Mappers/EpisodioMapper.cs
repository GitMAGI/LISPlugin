using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class EpisodioMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.EpisodioDTO EpisMapper(IDAL.VO.EpisodioVO raw)
        {
            IBLL.DTO.EpisodioDTO epis = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.EpisodioVO, IBLL.DTO.EpisodioDTO>());
                Mapper.AssertConfigurationIsValid();
                epis = Mapper.Map<IBLL.DTO.EpisodioDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return epis;
        }

        public static IDAL.VO.EpisodioVO EpisMapper(IBLL.DTO.EpisodioDTO raw)
        {
            IDAL.VO.EpisodioVO epis = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.EpisodioDTO, IDAL.VO.EpisodioVO>());
                Mapper.AssertConfigurationIsValid();
                epis = Mapper.Map<IDAL.VO.EpisodioVO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return epis;
        }

    }
}
