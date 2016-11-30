using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
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
            string ttype = refe.GetType().ToString();

            try
            {
                IDAL.VO.RefertoVO refe_ = this.dal.GetRefertoByEsamId(id);
                refe = RefertoMapper.RefeMapper(refe_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(refe), LibString.TypeName(refe_), LibString.TypeName(refe)));
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
                IDAL.VO.RefertoVO refe_ = this.dal.GetRefertoById(id);
                refe = RefertoMapper.RefeMapper(refe_);
                log.Info(string.Format("{0} {1} mapped to {2}", LibString.ItemsNumber(refe), LibString.TypeName(refe_), LibString.TypeName(refe)));
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