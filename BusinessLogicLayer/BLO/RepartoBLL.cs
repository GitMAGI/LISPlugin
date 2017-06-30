using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public IBLL.DTO.RepartoDTO GetRepartoById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RepartoDTO repa = null;

            try
            {
                IDAL.VO.RepartoVO repa_ = this.dal.GetRepartoById(id);
                repa = RepartoMapper.RepaMapper(repa_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(repa), LibString.TypeName(repa_), LibString.TypeName(repa)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }
        public IBLL.DTO.RepartoDTO GetRepartoByNome(string nome)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RepartoDTO repa = null;

            try
            {
                IDAL.VO.RepartoVO repa_ = this.dal.GetRepartoByNome(nome);
                repa = RepartoMapper.RepaMapper(repa_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(repa), LibString.TypeName(repa_), LibString.TypeName(repa)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }
    }
}
