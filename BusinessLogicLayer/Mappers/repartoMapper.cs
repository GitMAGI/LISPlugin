using AutoMapper;

namespace BusinessLogicLayer.Mappers
{
    public class RepartoMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.RepartoDTO RepaMapper(IDAL.VO.RepartoVO raw)
        {
            IBLL.DTO.RepartoDTO repa = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.RepartoVO, IBLL.DTO.RepartoDTO>());
                Mapper.AssertConfigurationIsValid();
                repa = Mapper.Map<IBLL.DTO.RepartoDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return repa;
        }

    }
}
