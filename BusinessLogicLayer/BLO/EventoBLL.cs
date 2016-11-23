using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public IBLL.DTO.EventoDTO GetEventoById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.EventoDTO epis = null;

            try
            {
                IDAL.VO.EventoVO dalRes = this.dal.GetEventoById(id);
                epis = EventoMapper.EvenMapper(dalRes);
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
        public IBLL.DTO.EventoDTO AddEvento(IBLL.DTO.EventoDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));
            
            IBLL.DTO.EventoDTO toReturn = null;

            try
            {
                data.evenidid = null;
                IDAL.VO.EventoVO data_ = EventoMapper.EvenMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                IDAL.VO.EventoVO stored = dal.NewEvento(data_);
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
        public IBLL.DTO.EventoDTO UpdateEvento(IBLL.DTO.EventoDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;
            IBLL.DTO.EventoDTO toReturn = null;
            string id = data.evenidid.ToString();

            try
            {
                if (id == null || GetEventoById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                IDAL.VO.EventoVO data_ = EventoMapper.EvenMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetEvento(data_);
                toReturn = GetEventoById(id);
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
