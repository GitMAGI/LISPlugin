using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class PazienteMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.PazienteDTO PaziMapper(IDAL.VO.PazienteVO raw)
        {
            IBLL.DTO.PazienteDTO pazi = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.PazienteVO, IBLL.DTO.PazienteDTO>());
                Mapper.AssertConfigurationIsValid();
                pazi = Mapper.Map<IBLL.DTO.PazienteDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return pazi;
        }

    }
}
