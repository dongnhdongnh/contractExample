using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.utility
{
    public class DatabaseUtility
    {
        public static string Escape(bool s)
        {
            if (s)
            {
                return "'1'";
            }
            return "'0'";
        }

        //public static string Escape(byte[] s)
        //{
        //    if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
        //    {
        //        return SanitaUtility.ConvertBinary2HexString_POSTGRES(s);
        //    }
        //    return SanitaUtility.ConvertBinary2HexString_MYSQL(s);
        //}

        public static string Escape(DateTime s) =>
            $"'{s:yyyy-MM-dd HH:mm:ss}'";

        public static string Escape(double s) =>
            ("'" + s.ToString(CultureInfo.InvariantCulture) + "'");

        public static string Escape(int s) =>
            ("'" + s.ToString() + "'");

        public static string Escape(long s) =>
            ("'" + s.ToString() + "'");

        public static string Escape(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return ("'" + s.Replace("'", "''") + "'");
            }

            return "''";
        }

        public static string Escape(ulong s)
        {
            if (s == 0L)
            {
                return "NULL";
            }
            DateTime time = DateTime.FromBinary((long)s);
            return $"'{time:yyyy-MM-dd HH:mm:ss}'";
        }

        public static string Escape(string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "NULL";
            }
            s = s.Trim();
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength - 1);
            }
            return ("'" + s.Trim().Replace("'", "''") + "'");
        }
    }
}
