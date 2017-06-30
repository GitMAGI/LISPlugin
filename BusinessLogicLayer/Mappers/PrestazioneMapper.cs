using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class PrestazioneMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.PrestazioneDTO PresMapper(IDAL.VO.PrestazioneVO raw)
        {
            IBLL.DTO.PrestazioneDTO pres = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.PrestazioneVO, IBLL.DTO.PrestazioneDTO>());
                Mapper.AssertConfigurationIsValid();
                pres = Mapper.Map<IBLL.DTO.PrestazioneDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return pres;
        }
        public static IDAL.VO.PrestazioneVO PresMapper(IBLL.DTO.PrestazioneDTO raw)
        {
            IDAL.VO.PrestazioneVO pres = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.PrestazioneDTO, IDAL.VO.PrestazioneVO>());
                Mapper.AssertConfigurationIsValid();
                pres = Mapper.Map<IDAL.VO.PrestazioneVO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return pres;
        }
    }
}
