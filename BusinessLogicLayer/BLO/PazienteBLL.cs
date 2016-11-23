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
                IDAL.VO.PazienteVO dalRes = this.dal.GetPazienteById(id);
                pazi = PazienteMapper.PaziMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", pazi.GetType().ToString()));
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
