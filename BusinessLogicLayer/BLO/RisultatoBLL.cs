using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        // Risultati Grezzi
        public List<IBLL.DTO.RisultatoDTO> GetRisultatiByEsamAnalId(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RisultatoDTO> riss = null;

            try
            {
                IDAL.VO.RisultatoGrezzoVO dalRes = this.dal.GetRisultatoGrezzoByEsamAnalId(id);
                riss = RisultatoMapper.AnreMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", riss.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            return riss;
        }
        public List<IBLL.DTO.RisultatoDTO> GetRisultatiByAnalId(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RisultatoDTO> riss = null;

            try
            {
                List<IDAL.VO.RisultatoVO> dalRes = this.dal.GetRisultatiByAnalId(id);
                riss = RisultatoMapper.AnreMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", riss.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return riss;
        }
        public List<IBLL.DTO.RisultatoDTO> AddRisultati(List<IBLL.DTO.RisultatoDTO> data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RisultatoDTO> riss = null;

            try
            {
                data.ForEach(p => p.anreidid = null);
                List<IDAL.VO.RisultatoVO> data_ = RisultatoMapper.AnreMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", data_.Count, data.GetType().ToString(), data_.GetType().ToString()));
                List<IDAL.VO.RisultatoVO> riss_ = dal.NewRisultati(data_);
                riss = RisultatoMapper.AnreMapper(riss_);
                log.Info(string.Format("{0} {1} mapped to {2}", riss.Count, riss_.GetType().ToString(), riss.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return riss;
        }
    }
}