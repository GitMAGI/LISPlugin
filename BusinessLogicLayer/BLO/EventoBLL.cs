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
                log.Info(string.Format("{0} VOs mapped to {1}", LibString.ItemsNumber(epis), LibString.TypeName(epis)));
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
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                IDAL.VO.EventoVO stored = dal.NewEvento(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = EventoMapper.EvenMapper(stored);
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
        public IBLL.DTO.EventoDTO UpdateEvento(IBLL.DTO.EventoDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int stored = 0;
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
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                stored = dal.SetEvento(data_);
                toReturn = GetEventoById(id);
                log.Info(string.Format("{0} {1} items added and {2} {3} retrieved back!", stored, LibString.TypeName(data_), LibString.ItemsNumber(toReturn), LibString.TypeName(toReturn)));
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
