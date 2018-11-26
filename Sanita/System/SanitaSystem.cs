using System;
using System.Data;
using System.Reflection;

namespace System
{
    public static class SanitaSystem
    {
        public static void SetProperty(this object obj, DataRow row)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                if (((column.ColumnName != "sync_flag") && (column.ColumnName != "update_flag")) && (column.ColumnName != "version"))
                {
                    object obj2 = row[column.ColumnName];
                    if (obj2 != DBNull.Value)
                    {
                        PropertyInfo property = obj.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if ((property != null) && property.CanWrite)
                        {
                            property.SetValue(obj, obj2, null);
                        }
                    }
                }
            }
        }
    }
}
