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
        public EpisodioVO GetEpisodioById(string episidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            EpisodioVO epis = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long episidid_ = long.Parse(episidid);
                string table = this.EpisodioTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "episidid",
                            Op = DBSQL.Op.Equal,
                            Value = episidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    epis = EpisodioMapper.EpisMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", epis.GetType().ToString()));
                }

                log.Info(string.Format("Query Executed! Retrieved {0} records!", data.Rows.Count));

                if (data != null && data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];

                    epis = EpisodioMapper.EpisMapper(row);
                                        
                    log.Info(string.Format("Record mapped to {0}", epis.GetType().ToString()));
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

            return epis;
        }
        public int SetEpisodio(EpisodioVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EpisodioTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string episidid = data.episidid.HasValue ? data.episidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "episidid" };

                if (episidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "EPISIDID",
                                Value = episidid,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions);
                    log.Info(string.Format("Updated {0} records!", result));
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

            return result;
        }
        public EpisodioVO NewEpisodio(EpisodioVO data)
        {
            EpisodioVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.AnalisiTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "EPISIDID" };
                List<string> autoincrement = new List<string>() { "episIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                    result = EpisodioMapper.EpisMapper(res.Rows[0]);
                log.Info(string.Format("Inserted new record with ID: {0}!", result.episidid));
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
        public int DeleteEpisodioById(string episidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EpisodioTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long episidid_ = long.Parse(episidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "episidid",
                                Value = episidid_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                result = DBSQL.DeleteOperation(connectionString, table, conditions);
                log.Info(string.Format("Deleted {0} records!", result));
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
