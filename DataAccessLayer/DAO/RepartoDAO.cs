using DataAccessLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using IDAL.VO;

namespace DataAccessLayer
{
    public partial class LISDAL
    {
        public RepartoVO GetRepartoById(string repaidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RepartoVO repa = null;
            try
            {
                string connectionString = this.GRConnectionString;

                int repaidid_ = int.Parse(repaidid);
                string table = this.RepartoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "repaidid",
                            Op = DBSQL.Op.Equal,
                            Value = repaidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        repa = RepartoMapper.RepaMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(repa), LibString.TypeName(repa)));
                    }                    
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }

        public RepartoVO GetRepartoByNome(string repanome)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RepartoVO repa = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.RepartoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "repanome",
                            Op = DBSQL.Op.Equal,
                            Value = repanome,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        repa = RepartoMapper.RepaMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(repa), LibString.TypeName(repa)));
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return repa;
        }
    }
}
