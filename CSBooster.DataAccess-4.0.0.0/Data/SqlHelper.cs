// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    public static class SqlHelper
    {
        public static string GetNullString(string content)
        {
            if (string.IsNullOrEmpty(content) || content.Trim().Length == 0)
                return "NULL";
            else
                return string.Format("'{0}'", content.Replace("'", "''"));
        }

        public static string GetString(string content)
        {
            if (string.IsNullOrEmpty(content) || content.Trim().Length == 0)
                return "''";
            else
                return string.Format("'{0}'", content.Replace("'", "''"));
        }

        public static string GetNullString(int? content)
        {
            if (content == null)
                return "NULL";
            else
                return content.Value.ToString();
        }

        public static string GetNullString(bool? content)
        {
            if (content == null)
                return "NULL";
            else
                return content.Value ? "1" : "0";
        }

        public static string GetNullString(DateTime? content)
        {
            if (content == null)
                return "NULL";
            else
                return string.Format("CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)", content.Value.Year, content.Value.Month.ToString("00"), content.Value.Day.ToString("00"));
        }

        public static SqlParameter AddParameter(string name, SqlDbType fieldType, int size, object Value)
        {
            SqlParameter para = new SqlParameter(name, fieldType, size);
            para.Value = Value ?? DBNull.Value;
            return para;
        }

        public static SqlParameter AddParameter(string name, SqlDbType fieldType, object Value)
        {
            SqlParameter para = new SqlParameter(name, fieldType);
            para.Value = Value;
            return para;
        }

        public static SqlParameter AddParameter(string name, SqlDbType fieldType)
        {
            SqlParameter para = new SqlParameter(name, fieldType);
            para.Value = DBNull.Value;
            return para;
        }

        public static string PrepareLike(string Value, bool AtBegin, bool AtEnd)
        {
            if (Value.Trim().Length > 0)
            {
                if (!Value.EndsWith("%") && AtEnd)
                {
                    if (Value.EndsWith("*"))
                        Value = string.Concat(Value.Substring(0, Value.Length - 1), "%");
                    else
                        Value = string.Concat(Value, "%");
                }
                if (!Value.StartsWith("%") && AtBegin)
                {
                    if (Value.StartsWith("*"))
                        Value = string.Concat("%", Value.Substring(1, Value.Length));
                    else
                        Value = string.Concat("%", Value);
                }
            }
            return Value.Trim().Replace("'", "''");
        }
    }
}