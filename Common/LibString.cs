using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace GeneralPurposeLib
{
    public static class LibString
    {
        public static string TimeSpanToTimeHmsms(TimeSpan ts)
        {
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds);

            return elapsedTime;
        }

        static public bool IsNumericType(this object o)
        {
            bool result = false;
            var type = Type.GetTypeCode(o.GetType());
            switch (type)
            {                
                case TypeCode.DateTime:
                    result=false;
                    break;
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    result=true;
                    break;
                default:
                    result=false;
                    break;
            }
            return result;
        }

        static public string SQLCommand2String(SqlCommand cmd)
        {
            string result = "";
            if (cmd.CommandType == System.Data.CommandType.StoredProcedure)
            {
                result = SQLCommandSP2String(cmd);
            }
            else
            {
                result = SQLCommandQuery2String(cmd);
            }
            return result;
        }

        static private string SQLCommandQuery2String(SqlCommand cmd)
        {
            string result = "";

            result = cmd.CommandText;
            foreach (SqlParameter p in cmd.Parameters)
            {
                string val_ = "NULL";
                if (p.Value != DBNull.Value && p.Value != null)
                    val_ = p.Value.IsNumericType() ? p.Value.ToString() : string.Format("'{0}'", p.Value.ToString());
                string par_ = "@" + p.ParameterName;
                
                result = ReplaceFirstOccurrance(result, par_, val_);                
            }
            return result;
        }

        static private string SQLCommandSP2String(SqlCommand cmd)
        {
            string result = "";

            result = "exec " + cmd.CommandText + " ";
            int _l = 1;
            foreach (SqlParameter p in cmd.Parameters)
            {
                string pv = "NULL";
                if (p.Value != DBNull.Value && p.Value != null)
                    pv = p.Value.IsNumericType() ? p.ParameterName + " = " + p.Value.ToString() : p.ParameterName + " = '" + p.Value.ToString() + "'";
                result += pv;
                if (_l < cmd.Parameters.Count)
                    result += ", ";
                _l++;
            }
            return result;
        }

        public static string ReplaceFirstOccurrance(string original, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;
            if (string.IsNullOrEmpty(oldValue))
                return original;
            if (string.IsNullOrEmpty(newValue))
                newValue = string.Empty;
            int loc = original.IndexOf(oldValue);
            if (loc == -1)
                return original;
            return original.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }

        public static string XML2String(XmlDocument xmlDoc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public static List<string> GetAllValuesSegments(string data, string value)
        {
            List<string> res = null;

            List<int> indexesStart = new List<int>();
            List<int> indexesEnd = new List<int>();
            for (int indexStart = 0; ; indexStart += value.Length)
            {
                indexStart = data.IndexOf(value, indexStart);
                if (indexStart == -1)
                    break;
                indexesStart.Add(indexStart);

                int indexEnd = data.IndexOf("\r\n", indexStart);
                if (indexEnd == -1)
                    indexEnd = data.Length;
                indexesEnd.Add(indexEnd);
            }

            for (int i = 0; i < indexesStart.Count; i++)
            {
                string tmp = data.Substring(indexesStart[i], indexesEnd[i] - indexesStart[i]);
                if (res == null)
                    res = new List<string>();
                res.Add(tmp);
            }

            return res;
        }
    }
}
