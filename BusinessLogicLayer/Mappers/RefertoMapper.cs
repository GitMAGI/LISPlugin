using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class RefertoMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.RefertoDTO RefeMapper(IDAL.VO.RefertoVO raw)
        {
            IBLL.DTO.RefertoDTO refe = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.RefertoVO, IBLL.DTO.RefertoDTO>());
                Mapper.AssertConfigurationIsValid();
                refe = Mapper.Map<IBLL.DTO.RefertoDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return refe;
        }
        public static IDAL.VO.RefertoVO AnreMapper(IBLL.DTO.RefertoDTO data)
        {
            IDAL.VO.RefertoVO refe = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.RefertoDTO, IDAL.VO.RefertoVO>());
                Mapper.AssertConfigurationIsValid();
                refe = Mapper.Map<IDAL.VO.RefertoVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return refe;
        }
    }
}
