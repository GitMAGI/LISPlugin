using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DataAccessLayer
{
    public partial class LISDAL
    {
        public IDAL.VO.RisultatoVO GetRisultatoById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.RisultatoVO ris = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long id_ = long.Parse(id);
                string table = this.RisultatoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "anreidid",
                            Op = DBSQL.Op.Equal,
                            Value = id_,
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
                        ris = Mappers.RisultatoMapper.AnreMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(ris), LibString.TypeName(ris)));
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

            return ris;
        }
        public List<IDAL.VO.RisultatoVO> GetRisultatiByAnalId(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.RisultatoVO> riss = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.RisultatoTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "anreanal",
                            Op = DBSQL.Op.Equal,
                            Value = id,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    riss = Mappers.RisultatoMapper.AnreMapper(data);
                    log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(riss), LibString.TypeName(riss)));
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

            return riss;
        }
        public IDAL.VO.RisultatoVO NewRisultato(IDAL.VO.RisultatoVO data)
        {
            IDAL.VO.RisultatoVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RisultatoTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "ANREIDID" };
                List<string> autoincrement = new List<string>() { "anreIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if (res.Rows.Count > 0)
                    {
                        result = Mappers.RisultatoMapper.AnreMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.anreidid));
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
        public List<IDAL.VO.RisultatoVO> NewRisultati(List<IDAL.VO.RisultatoVO> data)
        {
            List<IDAL.VO.RisultatoVO> results = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.RisultatoTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "ANREIDID" };
                List<string> autoincrement = new List<string>() { "aNreIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.MultiInsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                {
                    results = Mappers.RisultatoMapper.AnreMapper(res);
                }
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        string tmp = "";
                        int o = 0;
                        foreach (IDAL.VO.RisultatoVO tmp_ in results)
                        {
                            tmp += tmp_.anreidid.Value.ToString();
                            if (o < results.Count - 1)
                                tmp += ", ";
                            o++;
                        }
                        log.Info(string.Format("Inserted {0} new records with IDs: {1}!", LibString.ItemsNumber(results), tmp));
                    }                    
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
    }
}