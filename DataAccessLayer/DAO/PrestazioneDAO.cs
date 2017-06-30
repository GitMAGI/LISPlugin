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
        public IDAL.VO.PrestazioneVO GetPrestazioneById(string presidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.PrestazioneVO pres = null;
            try
            {
                string connectionString = this.GRConnectionString;
                                
                string table = this.PrestazioneTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "presidid",
                            Op = DBSQL.Op.Equal,
                            Value = presidid,
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
                        pres = PrestazioneMapper.PresMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(pres), LibString.TypeName(pres)));
                    }                    
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

            return pres;
        }
        public IDAL.VO.PrestazioneVO GetPrestazioneByEvento(string evenidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.PrestazioneVO pres = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.PrestazioneTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "preseven",
                            Op = DBSQL.Op.Equal,
                            Value = evenidid,
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
                        pres = PrestazioneMapper.PresMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(pres), LibString.TypeName(pres)));
                    }
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

            return pres;
        }

        public int SetPrestazione(IDAL.VO.PrestazioneVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PrestazioneTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string presidid = data.presidid.HasValue ? data.presidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "presidid" };

                if (presidid == null)
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
                                Key = "presidid",
                                Value = presidid,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions, new List<string>() { "presidid" });
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
        public IDAL.VO.PrestazioneVO NewPrestazione(IDAL.VO.PrestazioneVO data)
        {
            IDAL.VO.PrestazioneVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PrestazioneTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "PRESIDID" };
                List<string> autoincrement = new List<string>() { "pResIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if (res.Rows.Count > 0)
                    {
                        result = PrestazioneMapper.PresMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.presidid));
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
        public int DeletePrestazioneById(string presidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.PrestazioneTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long presidid_ = long.Parse(presidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "presidid",
                                Value = presidid_,
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