using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DataAccessLayer.Mappers;

namespace DataAccessLayer
{
    public partial class LISDAL
    {        
        // NO ENTITYFRAMEWORK
        public IDAL.VO.LabelVO GetLabelById(string labeidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.LabelVO labe = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long labelidid_ = long.Parse(labeidid);
                string table = this.LabelTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "labeidid",
                            Op = DBSQL.Op.Equal,
                            Value = labelidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;                
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    labe = LabelMapper.LabeMapper(data.Rows[0]);
                    log.Info(string.Format("Record mapped to {0}", labe.GetType().ToString()));
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

            return labe;
        }
        public List<IDAL.VO.LabelVO> GetLabelsByRichiesta(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.LabelVO> labes = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long richidid_ = long.Parse(richidid);
                string table = this.LabelTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "labeesam",
                            Op = DBSQL.Op.Equal,
                            Value = richidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                int count = data != null ? 0 : data.Rows.Count;
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", count));
                if (data != null && data.Rows.Count == 1)
                {
                    labes = LabelMapper.LabeMapper(data);
                    log.Info(string.Format("Record mapped to {0}", labes.GetType().ToString()));
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

            return labes;
        }
        public int SetLabel(IDAL.VO.LabelVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.LabelTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string labeidid = data.labeidid.HasValue ? data.labeidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "labeidid" };

                if (labeidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    long labeidid_ = long.Parse(labeidid);                    
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "labeidid",
                                Value = labeidid_,
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
        public IDAL.VO.LabelVO NewLabel(IDAL.VO.LabelVO data)
        {
            IDAL.VO.LabelVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.LabelTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "LABEIDID" };
                List<string> autoincrement = new List<string>() { "labeIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                    result = LabelMapper.LabeMapper(res.Rows[0]);
                log.Info(string.Format("Inserted new record with ID: {0}!", result.labeidid));                
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
        public List<IDAL.VO.LabelVO> NewLabels(List<IDAL.VO.LabelVO> data)
        {
            List<IDAL.VO.LabelVO> results = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.LabelTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "LABEIDID" };
                List<string> autoincrement = new List<string>() { "lAbeIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.MultiInsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                {
                    results = LabelMapper.LabeMapper(res);
                }                
                if(results!=null && results.Count > 0)
                {
                    string tmp = "";
                    int o = 0;
                    foreach(IDAL.VO.LabelVO tmp_ in results)
                    {
                        tmp += tmp_.labeidid.Value.ToString();
                        if (o < results.Count - 1)
                            tmp += ", ";
                        o++;
                    }
                    log.Info(string.Format("Inserted new records with IDs: {0}!", tmp));
                }
                else
                {
                    log.Info(string.Format("No records Inserted!"));
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

            return results;
        }
        public int DeleteLabelById(string labeidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.LabelTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long labeidid_ = long.Parse(labeidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "labeidid",
                                Value = labeidid_,
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
        public int DeleteLabelByRichiesta(string richidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.LabelTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long labeesam_ = long.Parse(richidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "labeesam",
                                Value = labeesam_,
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