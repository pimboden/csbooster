using System.Collections;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace _4screen.CSB.SQL_CLR
{
	public class Spliter
	{
		[SqlFunction(FillRowMethodName = "GuidListFillRow")]
		public static IEnumerable GuidListSplit(SqlString str, SqlString delimiter)
		{
			return str.Value.Split(delimiter.Value.ToCharArray(0, 1));
		}

		public static void GuidListFillRow(object row, out SqlGuid id)
		{
			try
			{
				id = new SqlGuid(row.ToString().Trim());
			}
			catch
			{
				id = SqlGuid.Null;
			}
		}
		[SqlFunction(FillRowMethodName = "IntListFillRow")]
		public static IEnumerable IntListSplit(SqlString str, SqlString delimiter)
		{
			return str.Value.Split(delimiter.Value.ToCharArray(0, 1));
		}

		public static void IntListFillRow(object row, out SqlInt32 id)
		{
			try
			{
				id = new SqlInt32(Convert.ToInt32(row.ToString().Trim()));
			}
			catch
			{
				id = SqlInt32.Null;
			}
		}
		[SqlFunction(FillRowMethodName = "StringListFillRow")]
		public static IEnumerable StringListSplit(SqlString str, SqlString delimiter)
		{
			return str.Value.Split(delimiter.Value.ToCharArray(0, 1));
		}

		public static void StringListFillRow(object row, out SqlString id)
		{
			try
			{
				id = new SqlString(row.ToString().Trim());
			}
			catch
			{
				id = SqlString.Null;
			}
		}
	}
}