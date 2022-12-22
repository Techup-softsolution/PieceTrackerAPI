using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PieceTracker.Common
{
    public static class ExtensionMethods
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }

        public static bool IsNull(this string value)
        {
            return value == null;
        }

        public static int? ToIntOrNull(this string value)
        {
            int i;
            if (int.TryParse(value, out i))
            {
                return i;
            }

            return null;
        }

        public static long? ToLongOrNull(this string value)
        {
            long i;
            if (long.TryParse(value, out i))
            {
                return i;
            }

            return null;
        }

        public static string SafeTrim(this string value)
        {
            return !string.IsNullOrEmpty(value) ? value.Trim() : value;
        }
        public static string NullIsBlank(this string value)
        {
            return !string.IsNullOrEmpty(value) ? value.Trim() : "";
        }

        public static bool IsNullOrBlank(this string text)
        {
            return text == null || text.Trim().Length == 0;
        }

        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> enumerable, string typeName, IEnumerable<string> tableColumnNames = null)
        {
            var dataTable = new DataTable();

            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(tableColumnNames == null ? "NONAME" : tableColumnNames.First(), typeof(T));
                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where(w => w.CanRead).ToArray();
                if (readableProperties.Length > 1 && tableColumnNames == null)
                    throw new ArgumentException("List of column names  must be provided when TVP contains more than one column");

                var columnNames = (tableColumnNames ?? readableProperties.Select(s => s.Name)).ToArray();
                foreach (string name in columnNames)
                {
                    dataTable.Columns.Add(name, readableProperties.Single(s => s.Name.Equals(name)).PropertyType);
                }
                if (!enumerable.IsNull())
                {
                    foreach (T obj in enumerable)
                    {
                        dataTable.Rows.Add(columnNames.Select(s => readableProperties.Single(s2 => s2.Name.Equals(s)).GetValue(obj)).ToArray());
                    }
                }
            }

            return dataTable.AsTableValuedParameter(typeName);
        }

        public static SqlMapper.ICustomQueryParameter AsTableValuedParameters<T>(this IEnumerable<T> enumerable, string typeName)
        {
            var dataTable = new DataTable();

            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add("NONAME", typeof(T));
                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where(w => w.CanRead).ToArray();

                var columnNames = (readableProperties.Select(s => s.Name)).ToArray();
                foreach (string name in columnNames)
                {
                    //dataTable.Columns.Add(name, readableProperties.Single(s => s.Name.Equals(name)).PropertyType);
                    var propertyType = readableProperties.Single(s => s.Name.Equals(name)).PropertyType;
                    dataTable.Columns.Add(name, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }
                if (!enumerable.IsNull())
                {
                    foreach (T obj in enumerable)
                    {
                        dataTable.Rows.Add(columnNames.Select(s => readableProperties.Single(s2 => s2.Name.Equals(s)).GetValue(obj)).ToArray());
                    }
                }
            }

            return dataTable.AsTableValuedParameter(typeName);
        }

        //triming object as well as list --Start
        public static List<T> Trim<T>(this List<T> obj)
        {
            if (obj != null && obj.Count != 0)
            {
                foreach (var item in obj)
                {
                    Trim(item);
                }
            }
            return obj;
        }
        public static T Trim<T>(this T item)
        {

            if (item != null)
            {
                var stringProperties = item.GetType().GetProperties()
                              .Where(p => p.PropertyType == typeof(string));

                foreach (var stringProperty in stringProperties)
                {
                    string currentValue = (string)stringProperty.GetValue(item, null);
                    if (currentValue != null)
                        stringProperty.SetValue(item, currentValue.Trim(), null);
                }
            }
            return item;
        }
        //triming object as well as list --End
        public static string ToXmlSerializer(this object obj)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (System.IO.StringWriter writer = new System.IO.StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
