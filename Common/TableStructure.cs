using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeneralPurposeLib
{
    public abstract class TableStructure
    {
        public List<ColumnStructure> table = null;

        public TableStructure()
        {
            this.table = Initialization();
        }

        protected abstract List<ColumnStructure> Initialization();

        public ColumnStructure? GetStructureByColName(string colName)
        {
            if (this.table == null)
            {
                throw new NullReferenceException("Table Structure Not Initialized!");
            }

            ColumnStructure? found = null;
            foreach (ColumnStructure col in this.table)
            {
                if (col.colName == colName)
                {
                    found = col;
                }
            }

            return found;
        }

        public static bool TryConvertFromString<TR>(string input, out TR output)
        {
            bool result = false;
            try
            {
                output = (TR)Convert.ChangeType(input, typeof(TR));
                result = true;
            }
            catch (InvalidCastException)
            {
                output = default(TR);
                //Conversion is not unsupported
            }
            catch (FormatException)
            {
                output = default(TR);
                //string input value was in incorrect format
            }
            catch (OverflowException)
            {
                output = default(TR);
                //narrowing conversion between two numeric types results in loss of data
            }
            return result;
        }

        public bool ParsingValidationRow(Dictionary<string, string> dataRow, ref Dictionary<string, object> dataRowConverted, ref List<string> errReport, ref List<string> warnReport, ref List<string> infoReport)
        {
            bool result = true;

            if (dataRowConverted == null)
                dataRowConverted = new Dictionary<string, object>();
            if (warnReport == null)
                warnReport = new List<string>();
            if (errReport == null)
                errReport = new List<string>();
            if (infoReport == null)
                infoReport = new List<string>();

            foreach (KeyValuePair<string, string> entry in dataRow)
            {
                ColumnStructure? colStruct = GetStructureByColName(entry.Key);
                if (colStruct == null)
                {
                    string msg = string.Format("Waring! An unaxpected dataRow index! '{0}' not found into TableStructure definition! This item will be ignored!", entry.Key);                    
                    warnReport.Add(msg);                  
                    continue; // Jump at the next iteration !!                    
                }

                if(entry.Value == null)
                {
                    dataRowConverted.Add(entry.Key, null);
                    continue; // Jump at the next iteration !!  
                }

                // PARSING VALIDATION
                object[] args = new object[2];
                args[0] = entry.Value;

                /*
                MethodInfo method = typeof(TableStructure).GetMethod("ValidationDataTypeAndParsing");
                MethodInfo generic = method.MakeGenericMethod(colStruct.Value.colType);
                object genericResult = generic.Invoke(this, args);
                */

                MethodInfo method = typeof(TableStructure).GetMethod("TryConvertFromString");
                MethodInfo generic = method.MakeGenericMethod(colStruct.Value.colType);
                object genericResult = generic.Invoke(null, args);

                bool tmpRes = (bool)genericResult;

                if (tmpRes)
                {                    
                    dataRowConverted.Add(entry.Key, args[1]);
                }
                else
                {
                    result = tmpRes;
                    string msg = string.Format("Attribute '{0}' Parsing from string to {1} failed! '{2}' is not a {3} parsable string!", entry.Key, colStruct.Value.colType.ToString(), entry.Value, colStruct.Value.colType.ToString());                    
                    errReport.Add(msg);
                }
            }

            if (errReport.Count == 0)
                errReport = null;
            if (warnReport.Count == 0)
                warnReport = null;
            if (infoReport.Count == 0)
                infoReport = null;
            if (dataRowConverted.Count == 0)
                dataRowConverted = null;

            return result;
        } // Can Throw tons of Exception, because of fatal Errors!

        public bool ParsingValidation(List<Dictionary<string, string>> data, ref List<Dictionary<string, object>> dataConverted, ref List<string> errReport, ref List<string> warnReport, ref List<string> infoReport)
        {
            bool result = true;

            if(dataConverted == null)
                dataConverted = new List<Dictionary<string, object>>();
            if(errReport == null)
                errReport = new List<string>();
            if (warnReport == null)
                warnReport = new List<string>();
            if (infoReport == null)
                infoReport = new List<string>();

            int i = 0;
            foreach (Dictionary<string, string> datum in data)
            {
                Dictionary<string, object> datumConverted = new Dictionary<string, object>();
                dataConverted.Add(datumConverted);

                List<string> errReport_row = null;
                List<string> warnReport_row = null;
                List<string> infoReport_row = null;

                bool rowRes = ParsingValidationRow(datum, ref datumConverted, ref errReport_row, ref warnReport_row, ref infoReport_row);

                if (!rowRes)
                    result = false;

                string msg = string.Format("Line {0}: ", i + 1);
                if(errReport_row != null && errReport_row.Count > 0)
                {
                    msg += string.Join(" | ", errReport_row.ToArray());
                    errReport.Add(msg);
                }
                if (warnReport_row != null && warnReport_row.Count > 0)
                {
                    msg += string.Join(" | ", warnReport_row.ToArray());
                    warnReport.Add(msg);
                }
                if (infoReport_row != null && infoReport_row.Count > 0)
                {
                    msg += string.Join(" | ", infoReport_row.ToArray());
                    infoReport.Add(msg);
                }

                i++;
            }
            if (errReport.Count == 0)
                errReport = null;
            if (warnReport.Count == 0)
                warnReport = null;
            if (infoReport.Count == 0)
                infoReport = null;
            if (dataConverted.Count == 0)
                dataConverted = null;

            return result;
        }

        public bool CheckAndConstranitsValidation<T>(List<T> objectData, ref List<string> errReport, ref List<string> warnReport, ref List<string> infoReport)
        {
            bool valid = true;

            if (errReport == null)
                errReport = new List<string>();
            if (warnReport == null)
                warnReport = new List<string>();
            if (infoReport == null)
                infoReport = new List<string>();

            int i = 1;
            foreach (T data in objectData)
            {
                List<string> errReport_att = new List<string>();
                List<string> warnReport_att = new List<string>();
                List<string> infoReport_att = new List<string>();

                bool validRow = CheckAndConstranitsValidationRow<T>(data, ref errReport_att, ref warnReport_att, ref infoReport_att);
                if (!validRow)
                    valid = false;

                string msg = string.Format("Item {0}: ", i);
                if (errReport_att != null && errReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", errReport_att.ToArray());
                    errReport.Add(msg_);
                }
                if (warnReport_att != null && warnReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", warnReport_att.ToArray());
                    warnReport.Add(msg_);
                }
                if (infoReport_att != null && infoReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", infoReport_att.ToArray());
                    infoReport.Add(msg_);
                }

                errReport_att = null;
                warnReport_att = null;
                infoReport_att = null;

                i++;
            }

            if (errReport.Count == 0)
                errReport = null;
            if (warnReport.Count == 0)
                warnReport = null;
            if (infoReport.Count == 0)
                infoReport = null;

            return valid;
        }
        public bool CheckAndConstranitsValidationRow<T>(T objectData, ref List<string> errReport, ref List<string> warnReport, ref List<string> infoReport)
        {
            bool valid = true;

            if (errReport == null)
                errReport = new List<string>();
            if (warnReport == null)
                warnReport = new List<string>();
            if (infoReport == null)
                infoReport = new List<string>();


            foreach(PropertyInfo prop in objectData.GetType().GetProperties())
            {
                string propName = prop.Name;
                object propValue = prop.GetValue(objectData);

                List<string> errReport_att = new List<string>();
                List<string> warnReport_att = new List<string>();
                List<string> infoReport_att = new List<string>();

                ColumnStructure? colStruct = this.GetStructureByColName(propName);
                if (colStruct != null)
                {
                    bool validAtt = CheckAndConstranitsValidationItem<T>(colStruct.Value, propValue, objectData, ref errReport_att, ref warnReport_att, ref infoReport_att);
                    if (!validAtt)
                        valid = false;
                }

                string msg = string.Format("Attribute '{0}': ", propName);
                if (errReport_att != null && errReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", errReport_att.ToArray());
                    errReport.Add(msg_);
                }
                if (warnReport_att != null && warnReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", warnReport_att.ToArray());
                    warnReport.Add(msg_);
                }
                if (infoReport_att != null && infoReport_att.Count > 0)
                {
                    string msg_ = msg + string.Join(" - ", infoReport_att.ToArray());
                    infoReport.Add(msg_);
                }

                errReport_att = null;
                warnReport_att = null;
                infoReport_att = null;
            }


            if (errReport.Count == 0)
                errReport = null;
            if (warnReport.Count == 0)
                warnReport = null;
            if (infoReport.Count == 0)
                infoReport = null;

            return valid;
        }
        public bool CheckAndConstranitsValidationItem<T>(ColumnStructure colStruct, object data, T objectData, ref List<string> errReport, ref List<string> warnReport, ref List<string> infoReport)
        {
            bool valid = true;

            if (errReport == null)
                errReport = new List<string>();
            if (warnReport == null)
                warnReport = new List<string>();
            if (infoReport == null)
                infoReport = new List<string>();




            // 1. Not Null Conditions
            if (data == null)
                if (colStruct.colNotNull)
                {
                    valid = false;
                    string msg = "NULL is not a valid value for this attribute!";
                    if (errReport == null)
                        errReport = new List<string>();
                    errReport.Add(msg);
                }



            // 2. MaxLength
            if (colStruct.colMaxLength != null && colStruct.colType == typeof(string) && data != null)
            {
                if (data.ToString().Length > colStruct.colMaxLength)
                {
                    valid = false;
                    string msg = string.Format("Exceeding the maximum length available. Actual length is {0} of {1}!", data.ToString().Length, colStruct.colMaxLength);
                    if (errReport == null)
                        errReport = new List<string>();
                    errReport.Add(msg);
                }
            }



            // 3. Checks Validation
            if (colStruct.colChecks != null && data != null)
            {
                foreach (CheckValidation check in colStruct.colChecks)
                {
                    Type classType = check.libName;
                    string funtionName = check.functionName;

                    List<string> errReport_func = new List<string>();
                    List<string> warnReport_func = new List<string>();
                    List<string> infoReport_func = new List<string>();

                    object[] args = new object[check.args.Count + 4];
                    args[0] = data;
                    int i = 1;
                    foreach (string arg in check.args)
                    {
                        object propValue = null;
                        if (objectData.GetType().GetProperty(arg) != null)
                        {
                            propValue = objectData.GetType().GetProperty(arg).GetValue(objectData, null);
                        }
                        else
                        { 
                            string err = string.Format("Fatal Error! {0} is an undefined property of the class {1}. Check the TableStructure class, the 'colChecks' property for the attribute {0}!", arg, objectData.GetType().ToString(), colStruct.colName);
                            throw new NullReferenceException(err);
                        }
                        args[i] = propValue;
                        i++;
                    }
                    args[i] = errReport_func;
                    args[i+1] = warnReport_func;
                    args[i+2] = infoReport_func;

                    try
                    {
                        MethodInfo method = classType.GetMethod(funtionName);
                        object genericResult = method.Invoke(null, args);

                        bool tmpRes = (bool)genericResult;

                        if (!tmpRes)
                            valid = false;

                        //string msg = string.Format("{0}.{1}() function check -> ", classType.ToString(), funtionName);
                        string msg = "";
                        if (errReport_func != null && errReport_func.Count > 0)
                        {
                            string msg_ = msg + string.Join(" - ", errReport_func.ToArray());
                            errReport.Add(msg_);
                        }
                        if (warnReport_func != null && warnReport_func.Count > 0)
                        {
                            string msg_ = msg + string.Join(" - ", warnReport_func.ToArray());
                            warnReport.Add(msg_);
                        }
                        if (infoReport_func != null && infoReport_func.Count > 0)
                        {
                            string msg_ = msg + string.Join(" - ", infoReport_func.ToArray());
                            infoReport.Add(msg_);
                        }
                        errReport_func = null;
                        warnReport_func = null;
                        infoReport_func = null;
                    }
                    catch (Exception ex)
                    {
                        string msg = string.Format("Fatal Error during check validations. Look at the function {0}.{1}(). Check if it's defined as static, return a bool, has the right number of args and doesn't throw unhandled exceptions!", classType.ToString(), funtionName);
                        throw new Exception(msg, ex);
                    }
                }
            }


            
            
        



            if (errReport.Count == 0)
                errReport = null;
            if (warnReport.Count == 0)
                warnReport = null;
            if (infoReport.Count == 0)
                infoReport = null;

            return valid;
        }
    }

    public struct ColumnStructure
    {
        public string colName;
        public Type colType;
        public int? colMaxLength;
        public bool colNotNull;
        public List<CheckValidation> colChecks;
    }

    public struct CheckValidation
    {
        public Type libName;
        public string functionName; // Function must be static!!
        public List<string> args; // Other attribute Names of the same row u want to check!! Not add the self attribute
    }
}
