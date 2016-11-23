using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Mappers;
using System.Diagnostics;
using GeneralPurposeLib;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public List<IBLL.DTO.RichiestaLISDTO> GetRichiesteLISByEven(string evenid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.RichiestaLISDTO> richs = null;

            try
            {
                List<IDAL.VO.RichiestaLISVO> dalRes = this.dal.GetRichiesteByEven(evenid);
                richs = RichiestaLISMapper.RichMapper(dalRes);
                log.Info(string.Format("{0} VO mapped to {1}", richs.Count, richs.First().GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }            

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return richs;
        }
        public IBLL.DTO.RichiestaLISDTO GetRichiestaLISById(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RichiestaLISDTO rich = null;

            try
            {
                IDAL.VO.RichiestaLISVO dalRes = this.dal.GetRichiestaById(richidid);
                rich = RichiestaLISMapper.RichMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", rich.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return rich;
        }
        public IBLL.DTO.RichiestaLISDTO AddRichiestaLIS(IBLL.DTO.RichiestaLISDTO esam)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RichiestaLISDTO toReturn = null;

            try
            {
                esam.esamidid = null;
                IDAL.VO.RichiestaLISVO data_ = RichiestaLISMapper.RichMapper(esam);
                log.Info(string.Format("1 {0} mapped to {1}", esam.GetType().ToString(), data_.GetType().ToString()));
                IDAL.VO.RichiestaLISVO stored = dal.NewRichiesta(data_);
                toReturn = RichiestaLISMapper.RichMapper(stored);
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
        public IBLL.DTO.RichiestaLISDTO UpdateRichiestaLIS(IBLL.DTO.RichiestaLISDTO esam)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;
            IBLL.DTO.RichiestaLISDTO toReturn = null;
            string id = esam.esamidid.ToString();

            try
            {
                if (id == null || GetRichiestaLISById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                IDAL.VO.RichiestaLISVO data_ = RichiestaLISMapper.RichMapper(esam);
                log.Info(string.Format("1 {0} mapped to {1}", esam.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetRichiesta(data_);
                toReturn = GetRichiestaLISById(id);
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
        public int DeleteRichiestaLISById(string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteRichiestaById(esamidid);
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
