using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public IBLL.DTO.PazienteDTO GetPazienteById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PazienteDTO pazi = null;

            try
            {
                IDAL.VO.PazienteVO pazi_ = this.dal.GetPazienteById(id);
                pazi = PazienteMapper.PaziMapper(pazi_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(pazi), LibString.TypeName(pazi_), LibString.TypeName(pazi)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pazi;
        }
    }
}
