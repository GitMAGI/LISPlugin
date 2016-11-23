using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public IBLL.DTO.EpisodioDTO GetEpisodioById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.EpisodioDTO epis = null;

            try
            {
                IDAL.VO.EpisodioVO dalRes = this.dal.GetEpisodioById(id);
                epis = EpisodioMapper.EpisMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", epis.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            return epis;
        }
        public IBLL.DTO.EpisodioDTO AddEpisodio(IBLL.DTO.EpisodioDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));
            
            IBLL.DTO.EpisodioDTO toReturn = null;

            try
            {
                data.episidid = null;
                IDAL.VO.EpisodioVO data_ = EpisodioMapper.EpisMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                IDAL.VO.EpisodioVO stored = dal.NewEpisodio(data_);
                log.Info(string.Format("1 {0} mapped to {1}", stored.GetType().ToString(), toReturn.GetType().ToString()));
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
        public IBLL.DTO.EpisodioDTO UpdateEpisodio(IBLL.DTO.EpisodioDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;
            IBLL.DTO.EpisodioDTO toReturn = null;
            string id = data.episidid.ToString();

            try
            {
                if (id == null || GetEpisodioById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                IDAL.VO.EpisodioVO data_ = EpisodioMapper.EpisMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetEpisodio(data_);
                toReturn = GetEpisodioById(id);
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
    }
}
