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
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(riss), LibString.TypeName(riss)));
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
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(riss), LibString.TypeName(riss)));
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

            List<IBLL.DTO.RisultatoDTO> toReturn = null;

            try
            {
                data.ForEach(p => p.anreidid = null);
                List<IDAL.VO.RisultatoVO> data_ = RisultatoMapper.AnreMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                List<IDAL.VO.RisultatoVO> stored = dal.NewRisultati(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = RisultatoMapper.AnreMapper(stored);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(toReturn), LibString.TypeName(stored), LibString.TypeName(toReturn)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return toReturn;
        }
        public int DeleteRisultatiByIdAnalisi(string analid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteRisultatiByAnalisi(analid);
                log.Info(string.Format("{0} items Deleted!", result));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
    }
}