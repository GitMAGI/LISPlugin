using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class EventoMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.EventoDTO EvenMapper(IDAL.VO.EventoVO raw)
        {
            IBLL.DTO.EventoDTO epis = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.EventoVO, IBLL.DTO.EventoDTO>());
                Mapper.AssertConfigurationIsValid();
                epis = Mapper.Map<IBLL.DTO.EventoDTO>(raw);
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
        public static IDAL.VO.EventoVO EvenMapper(IBLL.DTO.EventoDTO raw)
        {
            IDAL.VO.EventoVO epis = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.EventoDTO, IDAL.VO.EventoVO>());
                Mapper.AssertConfigurationIsValid();
                epis = Mapper.Map<IDAL.VO.EventoVO>(raw);
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
