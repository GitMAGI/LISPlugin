using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer
{    
    public class DBSQL
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string HidePwd(string connectionString)
        {
            string result = connectionString;
            string hiding = "****";
            try
            {
                string toFind = "password=";
                int offset = toFind.Length;
                int start = -1;
                start = connectionString.IndexOf(toFind, StringComparison.CurrentCultureIgnoreCase);
                if(start == -1)
                {
                    toFind = "password =";
                    offset = toFind.Length;
                    start = connectionString.IndexOf(toFind, StringComparison.CurrentCultureIgnoreCase);
                }

                if (start != -1)
                {
                    start = start + offset;
                    int end = connectionString.IndexOf(';', start);
                    if (end != -1)
                    {
                        string word = connectionString.Substring(start, end - start);
                        result = connectionString.Replace(word, hiding);
                    }
                }
            }
            catch(Exception)
            {
                log.Warn("Error during Connectio String Pwd Hiding!");
            }            

            return result;
        }

        static public DataTable ExecuteQuery(string connectionString, string sql)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            DataTable dataTable = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", sql));
                    log.Info(string.Format("Query execution starting ..."));

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataTable = new DataTable();
                        dataTable.Load(reader);
                    }
                    log.Info(string.Format("Query executed! Result count: {0}", LibString.ItemsNumber(dataTable)));
                    connection.Close();
                    log.Info("Connection Closed!");
                }
            }
            catch(Exception ex)
            {
                log.Info("Excepion detected!");
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }            

            return dataTable;
        }
        static public DataTable ExecuteQueryWithParams(string connectionString, string sql, Dictionary<string, object> atts)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            DataTable dataTable = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    foreach (KeyValuePair<string, object> entry in atts)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                    log.Info(string.Format("Query execution starting ..."));
                    using (var reader = cmd.ExecuteReader())
                    {
                        dataTable = new DataTable();
                        dataTable.Load(reader);
                    }
                    log.Info(string.Format("Query executed! Result count: {0}", LibString.ItemsNumber(dataTable)));
                    connection.Close();
                    log.Info("Connection Closed!");

                }
            }
            catch (Exception ex)
            {
                log.Info("Excepion detected!");
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return dataTable;
        }

        static public int ExecuteNonQuery(string connectionString, string sql)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            int result = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", sql));
                    log.Info(string.Format("Query execution starting ..."));
                    result = cmd.ExecuteNonQuery();
                    log.Info(string.Format("Query executed! Result count: {0}", result));
                    connection.Close();
                    log.Info("Connection Closed!");
                }
            }
            catch (Exception ex)
            {
                log.Info("Excepion detected!");
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }            

            return result;
        }
        static public int ExecuteNonQueryWithParams(string connectionString, string sql, Dictionary<string, object> atts)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            int result = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    foreach (KeyValuePair<string, object> entry in atts)
                    {
                        if(entry.Value == null)
                            cmd.Parameters.AddWithValue(entry.Key, DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                    log.Info(string.Format("Query execution starting ..."));
                    result = cmd.ExecuteNonQuery();
                    log.Info(string.Format("Query executed! Result count: {0}", result));
                    connection.Close();
                    log.Info("Connection Closed!");
                }
            }
            catch (Exception ex)
            {
                log.Info("Excepion detected!");
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return result;
        }

        public static class Op
        {
            readonly static public string Lower = " < ";
            readonly static public string Greater = " > ";
            readonly static public string LowerEqual = " <= ";
            readonly static public string GreaterEqual = " >= ";
            readonly static public string Equal = " = ";
            readonly static public string NotEqual = " <> ";
            readonly static public string Is = " is ";
            readonly static public string IsNot = " is not ";
            readonly static public string Like = " like ";
        };
        public static class Conj
        {
            readonly static public string And = "and";
            readonly static public string Or = " or ";
            readonly static public string None = "";
        };
        public struct QueryCondition
        {
            public string Key;
            public string Op;
            public object Value;
            public string Conj;
        }
        
        static public DataTable SelectOperation(string connectionString, string tabName, Dictionary<string, QueryCondition> conditions=null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            DataTable data = null;

            try
            {
                if (conditions != null)
                {
                    string query = "SELECT * FROM " + tabName + " WHERE " +
                        string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                    Dictionary<string, object> pars = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                    {
                        pars[entry.Key] = entry.Value.Value;
                    }

                    log.Info(string.Format("Query: {0}", query));
                    log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                    data = ExecuteQueryWithParams(connectionString, query, pars);
                }
                else
                {
                    string query = "SELECT * FROM " + tabName;

                    log.Info(string.Format("Query: {0}", query));                    

                    data =ExecuteQuery(connectionString, query);
                }
                    

                log.Info(string.Format("Query Executed! Retrieved {0} records!", LibString.ItemsNumber(data)));

                return data;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public int InsertOperation(string connectionString, string tabName, object dataVO, List<string> autoincrement = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            if (autoincrement == null)
                autoincrement = new List<string>() { };

            int result = -1;

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();

                foreach (var prop in dataVO.GetType().GetProperties())
                {
                    pars[prop.Name] = prop.GetValue(dataVO, null);                       
                }

                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", pars.Where(l => !autoincrement.Select(d => d.ToLower()).ToList().Contains(l.Key.ToLower())).Select(x => x.Key).ToArray()) +
                    ") VALUES (" +
                    string.Join(", ", pars.Where(l => !autoincrement.Select(d => d.ToLower()).ToList().Contains(l.Key.ToLower())).Select(x => "@" + x.Key).ToArray()) +
                    ")";

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public DataTable InsertBackOperation(string connectionString, string tabName, object dataVO, List<string> pk, List<string> autoincrement = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            if (autoincrement == null)
                autoincrement = new List<string>() { };

            DataTable data = null;

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();
                Dictionary<string, object> pars_ = new Dictionary<string, object>();

                foreach (var prop in dataVO.GetType().GetProperties())
                {
                    pars[prop.Name] = prop.GetValue(dataVO, null);
                }

                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", pars.Where(l => !autoincrement.Select(d=>d.ToLower()).ToList().Contains(l.Key.ToLower())).Select(x => x.Key).ToArray()) +
                    ") " +
                    "OUTPUT " + string.Join(", ", pk.Select(p => "INSERTED." + p).ToArray()) + " VALUES "
                    + "(" +
                    string.Join(", ", pars.Where(l => !autoincrement.Select(d => d.ToLower()).ToList().Contains(l.Key.ToLower())).Select(x => "@" + x.Key.ToLower()).ToArray()) +
                    ")";
                                
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (KeyValuePair<string, object> entry in pars)
                    {
                        if(!autoincrement.Select(d => d.ToLower()).ToList().Contains(entry.Key.ToLower()))
                        {
                            object dat = entry.Value != null ? entry.Value : DBNull.Value;
                            cmd.Parameters.AddWithValue(entry.Key, dat);
                        }                            
                    }
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                    log.Info(string.Format("Query execution starting ..."));
                    DataTable dataTable = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {                        
                        dataTable.Load(reader);
                    }
                    log.Info(string.Format("Query Insert executed! Result count: {0}", LibString.ItemsNumber(dataTable)));
                                        
                    DataRow row = dataTable.Rows[0];
                    foreach (string k in pk)
                    {
                        if (row[k] != null)
                        {
                            pars_[k] = row[k];
                        }
                    }
                    row = null;

                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    log.Info("Connection Closed!");
                }

                string query2 = "SELECT * FROM " + tabName + " WHERE " + string.Join(" AND ", pars_.Select(x => x.Key + " = " + "@" + x.Key + " ").ToArray());

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + " = " + x.Value).ToArray())));

                data = ExecuteQueryWithParams(connectionString, query2, pars_);
                
                log.Info(string.Format("Query Executed! Retrieved {0} records!", LibString.ItemsNumber(data)));

                return data;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public int MultiInsertOperation<T>(string connectionString, string tabName, List<T> dataVOs, List<string> autoincrement = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                List<Dictionary<string, object>> pars_ = new List<Dictionary<string, object>>();

                Dictionary<string, object> into = new Dictionary<string, object>();

                int i = 0;
                foreach (object dataVO in dataVOs)
                {
                    Dictionary<string, object> pars = new Dictionary<string, object>();

                    foreach (var prop in dataVO.GetType().GetProperties())
                    {
                        if (!autoincrement.Select(e => e.ToLower()).ToList().Contains(prop.Name.ToLower()))
                        {
                            string keyOfPar = string.Format("{0}{1}", prop.Name, i);
                            pars[keyOfPar] = prop.GetValue(dataVO, null);
                            //Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(chiamata, null));
                            if (i == 0)
                            {
                                into[prop.Name] = prop.GetValue(dataVO, null);
                            }
                        }
                    }
                    i++;
                    pars_.Add(pars);
                }

                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", into.Select(x => x.Key).ToArray()) +
                    ") VALUES ";

                into = null;

                Dictionary<string, object> parss = new Dictionary<string, object>();

                int j = 1;
                foreach (Dictionary<string, object> pars in pars_)
                {
                    query += "(" +
                    string.Join(", ", pars.Select(x => "@" + x.Key).ToArray()) +
                    ")";
                    parss = parss.Union(pars).ToDictionary(k => k.Key, v => v.Value);
                    if (j < pars_.Count)
                        query += ", ";
                    j++;
                }

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", parss.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = ExecuteNonQueryWithParams(connectionString, query, parss);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }                      
        }
        static public DataTable MultiInsertBackOperation<T>(string connectionString, string tabName, List<T> dataVOs, List<string> pk, List<string> autoincrement = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            if (autoincrement == null)
                autoincrement = new List<string>() { };

            DataTable data = null;

            try
            {
                List<Dictionary<string, object>> pars_ = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> pars__ = new List<Dictionary<string, object>>();
                Dictionary<string, object> into = new Dictionary<string, object>();

                int i = 0;
                foreach (object dataVO in dataVOs)
                {
                    Dictionary<string, object> pars = new Dictionary<string, object>();

                    foreach (var prop in dataVO.GetType().GetProperties())
                    {
                        if (!autoincrement.Select(e=>e.ToLower()).ToList().Contains(prop.Name.ToLower()))
                        {
                            string keyOfPar = string.Format("{0}{1}", prop.Name, i);
                            pars[keyOfPar] = prop.GetValue(dataVO, null);
                            if (i == 0)
                            {
                                into[prop.Name] = prop.GetValue(dataVO, null);
                            }
                        }                                        
                    }
                    i++;
                    pars__.Add(pars);
                }
                
                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", into.Select(x => x.Key).ToArray()) +
                    ") " + "OUTPUT " + string.Join(", ", pk.Select(p => "INSERTED." + p).ToArray()) + " VALUES ";

                into = null;

                Dictionary<string, object> parss = new Dictionary<string, object>();

                int j = 1;
                foreach (Dictionary<string, object> pars in pars__)
                {
                    query += "(" +
                    string.Join(", ", pars.Select(x => "@" + x.Key).ToArray()) +
                    ")";
                    parss = parss.Union(pars).ToDictionary(k => k.Key, v => v.Value);
                    if (j < pars__.Count)
                        query += ", ";
                    j++;
                }
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (KeyValuePair<string, object> entry in parss)
                    {
                        object dat = entry.Value != null ? entry.Value : DBNull.Value;
                        cmd.Parameters.AddWithValue(entry.Key, dat);
                    }
                    log.Info(string.Format("Opening Connection on '{0}' ...", HidePwd(connectionString)));
                    connection.Open();
                    log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                    log.Info(string.Format("Query execution starting ..."));
                    DataTable dataTable = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    log.Info(string.Format("Query Insert executed! Result count: {0}", LibString.ItemsNumber(dataTable)));

                    int t = 0;
                    foreach(DataRow row in dataTable.Rows)
                    {
                        Dictionary<string, object> tmp = new Dictionary<string, object>();
                        foreach (string k in pk)
                        {
                            if (row[k] != null)
                            {
                                tmp[k + t.ToString()] = row[k];
                            }
                        }
                        pars_.Add(tmp);
                        t++;
                    }
                    
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    log.Info("Connection Closed!");
                }

                Dictionary<string, object> pars2 = new Dictionary<string, object>();
                string query2 = "SELECT * FROM " +
                    tabName + " WHERE " +
                    "(" + string.Join(", ", pk.Select(x => x)) + ") " +
                    "IN (";
                int r = 0;
                string cond_ = "";
                foreach (Dictionary<string, object> tmp in pars_)
                {
                    cond_ += "(";
                    cond_ += string.Join(", ", tmp.Select(z => "@" + z.Key));
                    cond_ += ")";
                    if (r < pars_.Count - 1)
                        cond_ += ", ";
                    r++;
                    pars2 = pars2.Concat(tmp).ToDictionary(x => x.Key, x => x.Value);
                }
                query2 += cond_;
                query2 += ")";

                log.Info(string.Format("Query: {0}", query2));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars2.Select(x => x.Key + " = " + x.Value).ToArray())));

                data = ExecuteQueryWithParams(connectionString, query2, pars2);

                log.Info(string.Format("Query Executed! Retrieved {0} records!", LibString.ItemsNumber(data)));

                return data;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public int DeleteOperation(string connectionString, string tabName, Dictionary<string, QueryCondition> conditions)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                string query = "DELETE FROM " + tabName + " WHERE " +
                    string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                Dictionary<string, object> pars = new Dictionary<string, object>();
                foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                {
                    pars[entry.Key] = entry.Value.Value;
                }

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }     
            
        }
        static public int UpdateOperation(string connectionString, string tabName, object dataVO, Dictionary<string, QueryCondition> conditions)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in dataVO.GetType().GetProperties())
                {
                    data[prop.Name] = prop.GetValue(dataVO, null);
                }

                string query = "UPDATE " + tabName +
                    " SET " +
                    string.Join(", ", data.Select(x => x.Key + " = " + "@" + x.Key).ToArray()) +                    
                    " WHERE " +
                    string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                Dictionary<string, object> conditions_ = new Dictionary<string, object>();
                foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                {
                    conditions_[entry.Key] = entry.Value.Value;
                }

                Dictionary<string, object> pars = data.Concat(conditions_).ToDictionary(x => x.Key, x => x.Value);

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = DBSQL.ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));
               
                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }            
        }
        static public int UpdateOperation(string connectionString, string tabName, object dataVO, Dictionary<string, QueryCondition> conditions, List<string> autoincrement = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in dataVO.GetType().GetProperties())
                {                    
                    data[prop.Name] = prop.GetValue(dataVO, null);
                }

                string query = "UPDATE " + tabName +
                    " SET " +
                    string.Join(", ", data.Where(l => !autoincrement.Select(d => d.ToLower()).ToList().Contains(l.Key.ToLower())).Select(x => x.Key + " = " + "@" + x.Key).ToArray()) +
                    " WHERE " +
                    string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                Dictionary<string, object> conditions_ = new Dictionary<string, object>();
                foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                {
                    conditions_[entry.Key] = entry.Value.Value;
                }

                Dictionary<string, object> pars = data.Concat(conditions_).ToDictionary(x => x.Key, x => x.Value);

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
    }
}