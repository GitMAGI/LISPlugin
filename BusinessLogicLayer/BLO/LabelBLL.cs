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
        public List<IBLL.DTO.LabelDTO> GetLabelsByRichiesta(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.LabelDTO> labes = null;

            try
            {
                List<IDAL.VO.LabelVO> dalRes = this.dal.GetLabelsByRichiesta(richidid);
                labes = LabelMapper.LabeMapper(dalRes);
                log.Info(string.Format("{0} VOs mapped to {0}", labes.Count, labes.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return labes;
        }
        public IBLL.DTO.LabelDTO GetLabelById(string labeidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.LabelDTO labe = null;

            try
            {
                IDAL.VO.LabelVO dalRes = this.dal.GetLabelById(labeidid);
                labe = LabelMapper.LabeMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", labe.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return labe;
        }
        public IBLL.DTO.LabelDTO UpdatLabel(IBLL.DTO.LabelDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;
            IBLL.DTO.LabelDTO toReturn = null;
            string id = data.labeidid.ToString();

            try
            {
                if(id == null || GetLabelById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }                
                IDAL.VO.LabelVO data_ = LabelMapper.LabeMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetLabel(data_);
                toReturn = GetLabelById(data.labeidid.ToString());
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
        public IBLL.DTO.LabelDTO AddLabel(IBLL.DTO.LabelDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));
                        
            IBLL.DTO.LabelDTO toReturn = null;

            try
            {
                data.labeidid = null;
                IDAL.VO.LabelVO data_ = LabelMapper.LabeMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));                
                IDAL.VO.LabelVO stored = dal.NewLabel(data_);
                toReturn = LabelMapper.LabeMapper(stored);
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
        public int DeleteLabelById(string labeidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteLabelById(labeidid);
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
        public List<IBLL.DTO.LabelDTO> AddLabels(List<IBLL.DTO.LabelDTO> data)
        {            
            Stopwatch tw = new Stopwatch();
            tw.Start();
            
            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.LabelDTO> labes = null;

            try
            {
                data.ForEach(p => p.labeidid = null);
                List<IDAL.VO.LabelVO> data_ = LabelMapper.LabeMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", data_.Count, data.GetType().ToString(), data_.GetType().ToString()));
                List<IDAL.VO.LabelVO> labes_ = dal.NewLabels(data_);
                labes = LabelMapper.LabeMapper(labes_);
                log.Info(string.Format("{0} {1} mapped to {2}", labes.Count, labes_.GetType().ToString(), labes.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return labes;
        }
    }
}
