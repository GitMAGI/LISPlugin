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
        public IBLL.DTO.PrestazioneDTO GetPrestazioneById(string presidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PrestazioneDTO pres = null;

            try
            {
                IDAL.VO.PrestazioneVO pres_ = this.dal.GetPrestazioneById(presidid);
                pres =PrestazioneMapper.PresMapper(pres_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(pres), LibString.TypeName(pres_), LibString.TypeName(pres)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pres;
        }
        public IBLL.DTO.PrestazioneDTO GetPrestazioneByEvento(string evenidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PrestazioneDTO pres = null;

            try
            {
                IDAL.VO.PrestazioneVO pres_ = this.dal.GetPrestazioneByEvento(evenidid);
                pres = PrestazioneMapper.PresMapper(pres_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(pres), LibString.TypeName(pres_), LibString.TypeName(pres)));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return pres;
        }

        public IBLL.DTO.PrestazioneDTO AddPrestazione(IBLL.DTO.PrestazioneDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.PrestazioneDTO toReturn = null;

            try
            {
                data.presidid = null;
                IDAL.VO.PrestazioneVO data_ = PrestazioneMapper.PresMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                IDAL.VO.PrestazioneVO stored = dal.NewPrestazione(data_);
                log.Info(string.Format("{0} {1} items added and got back!", LibString.ItemsNumber(stored), LibString.TypeName(stored)));
                toReturn = PrestazioneMapper.PresMapper(stored);
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
        public IBLL.DTO.PrestazioneDTO UpdatePrestazione(IBLL.DTO.PrestazioneDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int stored = 0;
            IBLL.DTO.PrestazioneDTO toReturn = null;
            string id = data.presidid.ToString();

            try
            {
                if (id == null || GetPrestazioneById(id) == null)
                {
                    string msg = string.Format("No record found with the id {0}! Updating is impossible!", id);
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }
                IDAL.VO.PrestazioneVO data_ = PrestazioneMapper.PresMapper(data);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(data_), LibString.TypeName(data), LibString.TypeName(data_)));
                stored = dal.SetPrestazione(data_);
                toReturn = GetPrestazioneById(id);
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
        public int DeletePrestazioneById(string presidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeletePrestazioneById(presidid);
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
