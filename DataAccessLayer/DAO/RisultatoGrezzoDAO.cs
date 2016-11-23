using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DataAccessLayer
{
    public partial class LISDAL
    {
        public IDAL.VO.RisultatoGrezzoVO GetRisultatoGrezzoById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RisultatoGrezzoVO risG = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long id_ = long.Parse(id);
                string table = this.RisultatoGrezzoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "id",
                            Op = DBSQL.Op.Equal,
                            Value = id_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    risG = Mappers.RisultatoMapper.AnreTrashMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", risG.GetType().ToString()));
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return risG;
        }

        public IDAL.VO.RisultatoGrezzoVO GetRisultatoGrezzoByEsamAnalId(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RisultatoGrezzoVO risG = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.RisultatoGrezzoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "esamanalid",
                            Op = DBSQL.Op.Equal,
                            Value = id,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null)
                {
                    risG = Mappers.RisultatoMapper.AnreTrashMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", risG.GetType().ToString()));
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return risG;
        }

    }
}
