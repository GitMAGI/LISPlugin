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
                List<IDAL.VO.LabelVO> labes_ = this.dal.GetLabelsByRichiesta(richidid);
                labes = LabelMapper.LabeMapper(labes_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(labes), LibString.TypeName(labes_), LibString.TypeName(labes)));
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
                IDAL.VO.LabelVO labe_ = this.dal.GetLabelById(labeidid);
                labe = LabelMapper.LabeMapper(labe_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(labe), LibString.TypeName(labe_), LibString.TypeName(labe)));
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

            int stored = 0;
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
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                stored = dal.SetLabel(data_);
                toReturn = GetLabelById(data.labeidid.ToString());
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
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                IDAL.VO.LabelVO stored = dal.NewLabel(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = LabelMapper.LabeMapper(stored);
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
        public int DeleteLabelById(string labeidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteLabelById(labeidid);
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
        public List<IBLL.DTO.LabelDTO> AddLabels(List<IBLL.DTO.LabelDTO> data)
        {            
            Stopwatch tw = new Stopwatch();
            tw.Start();
            
            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.LabelDTO> toReturn = null;

            try
            {
                data.ForEach(p => p.labeidid = null);
                List<IDAL.VO.LabelVO> data_ = LabelMapper.LabeMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                List<IDAL.VO.LabelVO> stored = dal.NewLabels(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = LabelMapper.LabeMapper(stored);
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
    }
}
