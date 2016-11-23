using System;
using System.Collections.Generic;
using BusinessLogicLayer.Mappers;
using System.Diagnostics;
using GeneralPurposeLib;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {
        public List<IBLL.DTO.AnalisiDTO> GetAnalisisByRichiesta(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.AnalisiDTO> anals = null;

            try
            {
                List<IDAL.VO.AnalisiVO> dalRes = dal.GetAnalisisByRichiesta(richidid);
                anals = AnalisiMapper.AnalMapper(dalRes);
                log.Info(string.Format("{0} VOs mapped to {0}", anals.Count, anals.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anals;
        }
        public IBLL.DTO.AnalisiDTO GetAnalisiById(string analidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.AnalisiDTO anal = null;

            try
            {
                IDAL.VO.AnalisiVO dalRes = this.dal.GetAnalisiById(analidid);
                anal = AnalisiMapper.AnalMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", anal.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anal;
        }
        public List<IBLL.DTO.AnalisiDTO> GetAnalisisByIds(List<string> analidids)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.AnalisiDTO> anals = null;

            try
            {
                List<IDAL.VO.AnalisiVO> anals_ = dal.GetAnalisisByIds(analidids);
                anals = AnalisiMapper.AnalMapper(anals_);
                log.Info(string.Format("{0} {1} mapped to {2}", anals.Count, anals_.GetType().ToString(), anals.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anals;
        }
        public IBLL.DTO.AnalisiDTO UpdateAnalisi(IBLL.DTO.AnalisiDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.AnalisiDTO toReturn = null;

            try
            {
                IDAL.VO.AnalisiVO data_ = AnalisiMapper.AnalMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));                
                int stored = dal.SetAnalisi(data_);
                toReturn = GetAnalisiById(data.analidid.ToString());                
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
        public IBLL.DTO.AnalisiDTO AddAnalisi(IBLL.DTO.AnalisiDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.AnalisiDTO toReturn = null;

            try
            {
                data.analidid = null;
                IDAL.VO.AnalisiVO data_ = AnalisiMapper.AnalMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                IDAL.VO.AnalisiVO stored = dal.NewAnalisi(data_);
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
        public List<IBLL.DTO.AnalisiDTO> AddAnalisis(List<IBLL.DTO.AnalisiDTO> data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.AnalisiDTO> anals = null;

            try
            {
                data.ForEach(p => p.analidid = null);
                List<IDAL.VO.AnalisiVO> data_ = AnalisiMapper.AnalMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", data_.Count, data.GetType().ToString(), data_.GetType().ToString()));
                List<IDAL.VO.AnalisiVO> labes_ = dal.NewAnalisi(data_);
                anals = AnalisiMapper.AnalMapper(labes_);
                log.Info(string.Format("{0} {1} mapped to {2}", anals.Count, labes_.GetType().ToString(), anals.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return anals;
        }
        public int DeleteAnalisiById(string analidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteAnalisiById(analidid);
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
        public int DeleteAnalisiByRichiesta(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteAnalisiById(richidid);
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
