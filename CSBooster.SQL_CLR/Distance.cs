using System.Collections;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace _4screen.CSB.SQL_CLR
{
	public class Distance
	{
		[SqlFunction(DataAccess = DataAccessKind.Read)]
      public static double CalcDistance(SqlDouble breite1, SqlDouble laenge1, double breite2, double laenge2)
		{
         if (!breite1.IsNull && !laenge1.IsNull)
         {
            double calc = 0.0;
            calc = Math.Sin(Radians(breite1.Value)) * Math.Sin(Radians(breite2)) + Math.Cos(Radians(breite1.Value)) * Math.Cos(Radians(breite2)) * Math.Cos(Radians(laenge2 - laenge1.Value));
            if (calc > 1.0)
               calc = 1.0;
            else if (calc < -1.0)
               calc = -1.0;
            return Math.Acos(calc) * 6378.338;
         }
         else
         {
            return double.MaxValue;
         }
		}

		public static double Radians(double degrees)
		{
			double radians = (Math.PI / 180) * degrees;
			return (radians);
		}
	}
}
