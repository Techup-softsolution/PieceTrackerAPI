using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Web;
using System.Globalization;
using System.Resources;
using System.ComponentModel.DataAnnotations;

namespace PieceTracker.Common
{
    public class Utility
    {
        public static SqlParameter GetSQLParam(string paramName, SqlDbType type, dynamic value)
        {
            SqlParameter sqlParam = new SqlParameter(paramName, type);
            sqlParam.Value = value;
            return sqlParam;
        }

        public static SqlParameter GetSQLParam(string paramName, SqlDbType type, dynamic value, [Optional] string typeName)
        {
            SqlParameter sqlParam = new SqlParameter(paramName, type);
            sqlParam.Value = value;
            if (typeName != null)
            {
                sqlParam.TypeName = typeName;
            }
            return sqlParam;
        }

        public static string GetSortOrder(string SortBy, string SortDirection)
        {
            return SortBy + " " + (SortDirection.ToLower() == "descending" ? "DESC" : "");
        }

        public static DataTable ReadCsvFile(string FilePath)
        {
            string a = "";
            DataTable dtCsv = new DataTable();
            string text;
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    text = sr.ReadToEnd().ToString();
                    string[] rows = text.Split('\r');
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(new string[] { "\",\"" }, StringSplitOptions.None);
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j].Replace("\"", ""));
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString().Replace("\"", "");
                                }
                                dtCsv.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }

        public static void WriteCsvFile(string FilePath, DataTable dtTable)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn dc in dtTable.Columns)
                sb.Append(FormatCSV(dc.ColumnName.ToString()) + ",");
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine();

            foreach (DataRow dr in dtTable.Rows)
            {
                foreach (DataColumn dc in dtTable.Columns)
                    sb.Append(FormatCSV(dr[dc.ColumnName].ToString()) + ",");
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            File.WriteAllText(FilePath, sb.ToString());
        }

        public static string FormatCSV(string input)
        {
            try
            {
                if (input == null)
                    return string.Empty;

                bool containsQuote = false;
                bool containsComma = false;
                int len = input.Length;
                for (int i = 0; i < len && (containsComma == false || containsQuote == false); i++)
                {
                    char ch = input[i];
                    if (ch == '"')
                        containsQuote = true;
                    else if (ch == ',')
                        containsComma = true;
                }

                if (containsQuote && containsComma)
                    input = input.Replace("\"", "\"\"");

                if (containsComma)
                    return "\"" + input + "\"";
                else
                    return input;
            }
            catch
            {
                throw;
            }
        }

        public static void WriteLogFile(string msg)
        {
            const string path = @"C:\Users\sit87\Desktop\Log.txt";
            File.AppendAllText(msg, path);
        }

        public static string EncryptNullorEmptystring(string _string)
        {
            string returnstring = string.Empty;
            try
            {
                returnstring = (!string.IsNullOrEmpty(_string) ? Security.Encrypt(_string) : _string);
            }
            catch (Exception ex)
            {
                returnstring = _string;
            }
            return returnstring;
        }
        public static string DecryptNullorEmptystring(string _string)
        {
            string returnstring = string.Empty;
            try
            {
                returnstring = (!string.IsNullOrEmpty(_string) ? Security.Decrypt(_string) : _string);
            }
            catch (Exception ex)
            {
                returnstring = _string;
            }
            return returnstring;
        }
        public static string GetPhoneNo(string PhoneNo)
        {
            return PhoneNo.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string ConvertDatetimetoDate(DateTime date)
        {
            DateTime dt = DateTime.ParseExact(date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string s = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            return s;
        }


        public  static bool ContainsSpecialChars(string StringToCheck)
        {
            bool lReturn = false;
            if (StringToCheck.Contains("'")) return true;
            if (StringToCheck.Contains("`")) return true;
            if (StringToCheck.Contains("^")) return true;

            return lReturn;
        }
        public static string GetNextLevelCode(string current)
        {
            string nextCode = string.Empty;
            int temp = Convert.ToInt32(current.Split('L')[1].ToString()) + 1;
            nextCode = temp.ToString();
            return nextCode;
        }

        public static string GenerateReferCode()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 8;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }
        public static string GenerateRandomOTPCode()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }
        public static string GetResponseMessage(bool status, int recordId, int action)
        {
            string response = string.Empty;
            switch (action)
            {
                case (int)Enums.ActionName.AddUpdate:
                    response = status ? (recordId > 0 ? (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.EditMessage)) : (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.CreateMessage))) : (recordId > 0 ? (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.EditErrorMessage)) : (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.CreateErrorMessage)));
                    break;
                case (int)Enums.ActionName.Delete:
                    response = status ? (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.DeleteMessage)) : ((EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.DeleteErrorMessage)));
                    break;
                case (int)Enums.ActionName.Verify:
                    response = status ? (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.VerifySuccessMessage)) : ((EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.VerifyErrorMessage)));
                    break;
                case (int)Enums.ActionName.ResetPasswordLink:
                    response = status ? (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.ResetPasswordLinkSuccess)) : ((EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.ResetPasswordLinkError)));
                    break;
            }            
            return response;
        }
        public static string GenerateRandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*().";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 8;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }
        public static string GenerateRandomResetToken()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*().";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 20;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }
    }

    public class ConvertToXml<T> where T : class, new()
    {
        static ConvertToXml()
        {


        }

        public static string GetXMLString(List<T> sourceList, string csvSelectedProperties = "")
        {

            //All numeric values in created xml was with dot symbol instead of comma
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            if (sourceList != null)
            {


                StringBuilder xmlString = new StringBuilder();
                xmlString.Append(@"<table>");

                Type sourceType;
                foreach (T item in sourceList)
                {
                    xmlString.Append("<row>");
                    sourceType = item.GetType();

                    foreach (PropertyInfo p in sourceType.GetProperties().Where(p => string.IsNullOrEmpty(csvSelectedProperties) || csvSelectedProperties.Split(',').Contains(p.Name)))
                    {
                        xmlString.Append("<" + p.Name + ">");
                        xmlString.Append(EncodeSpecialCharacter(p.GetValue(item, null)));
                        xmlString.Append("</" + p.Name + ">");
                    }
                    xmlString.Append("</row>");
                }
                xmlString.Append("</table>");

                return xmlString.ToString();
            }
            return string.Empty;
        }
        
        /// <summary>
        /// Replace Special Character as following 
        /// 1) &  =   &amp;
        /// 2) <  =   &lt;
        /// 3) >  =   &gt;
        /// 4) "  =   &quot;
        /// 5) '  =   &#39;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object EncodeSpecialCharacter(object value)
        {
            if (value != null)
            {
                string strValue = value as string;

                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(@"""", "&quot;").Replace("'", "&#39;");
                    return strValue;

                }
            }
            return value;
        }

        


    }



}
