using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class LISBLL
    {        
        public IBLL.DTO.RefertoDTO GetRefertoByEsamId(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RefertoDTO refe = null;

            try
            {
                IDAL.VO.RefertoVO dalRes = this.dal.GetRefertoByEsamId(id);
                refe = RefertoMapper.RefeMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", refe.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            return refe;
        }
        public IBLL.DTO.RefertoDTO GetRefertoById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.RefertoDTO refe = null;

            try
            {
                IDAL.VO.RefertoVO dalRes = this.dal.GetRefertoById(id);
                refe = RefertoMapper.RefeMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", refe.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return refe;
        }
    }
}