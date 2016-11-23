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
        public RefertoVO GetRefertoById(string refeidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RefertoVO refe = null;
            try
            {
                string connectionString = this.GRConnectionString;

                int refeidid_ = int.Parse(refeidid);
                string table = this.RefertoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "refeidid",
                            Op = DBSQL.Op.Equal,
                            Value = refeidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    refe = RefertoMapper.RefeMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", refe.GetType().ToString()));
                }

                log.Info(string.Format("Query Executed! Retrieved {0} records!", data.Rows.Count));

                if (data != null && data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];

                    refe = RefertoMapper.RefeMapper(row);
                                        
                    log.Info(string.Format("Record mapped to {0}", refe.GetType().ToString()));
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

            return refe;
        }
        public RefertoVO GetRefertoByEsamId(string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            RefertoVO refe = null;
            try
            {
                string connectionString = this.GRConnectionString;

                int refeesam_ = int.Parse(esamidid);
                string table = this.RefertoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "refeesam",
                            Op = DBSQL.Op.Equal,
                            Value = refeesam_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    refe = RefertoMapper.RefeMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", refe.GetType().ToString()));
                }

                log.Info(string.Format("Query Executed! Retrieved {0} records!", data.Rows.Count));

                if (data != null && data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];

                    refe = RefertoMapper.RefeMapper(row);

                    log.Info(string.Format("Record mapped to {0}", refe.GetType().ToString()));
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

            return refe;
        }
        public int SetReferto(RefertoVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RefertoTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string refeidid = data.refeidid.HasValue ? data.refeidid.Value.ToString() : null;

                if (refeidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data);
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
                                Key = "REFEIDID",
                                Value = refeidid,
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
        public int DeleteRefertoById(string refeidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RefertoTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                int refeidid_ = int.Parse(refeidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "refeidid",
                                Value = refeidid_,
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
