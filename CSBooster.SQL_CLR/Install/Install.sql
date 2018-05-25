
EXEC sp_configure 'CLR enabled', 1
RECONFIGURE With Override 
GO
CREATE ASSEMBLY CLRFuncs FROM 'C:\_USERSPACE\Patrik Imboden\CSBooster.SQL_CLR.dll'
-- Note: Change the path according to the location of your DLL. 
GO
CREATE FUNCTION GuidListSplit(@list NVARCHAR(MAX), @delim nchar(1)=N'|')
Returns Table (returnValue uniqueidentifier)
AS
External Name [CLRFuncs].[_4screen.CSB.SQL_CLR.Spliter].[GuidListSplit]
GO

CREATE FUNCTION IntListSplit(@list NVARCHAR(MAX), @delim nchar(1)=N'|')
Returns Table (returnValue int)
AS
External Name [CLRFuncs].[_4screen.CSB.SQL_CLR.Spliter].[IntListSplit]
GO

CREATE FUNCTION StringListSplit(@list NVARCHAR(MAX) ,@delim nchar(1)=N'|')
Returns Table (returnValue NVARCHAR(MAX))
AS
External Name [CLRFuncs].[_4screen.CSB.SQL_CLR.Spliter].[StringListSplit]
GO

CREATE FUNCTION hifu_CalcDistance(@breite_1 float, @laenge_1 float, @breite_2 float, @laenge_2 float)
RETURNS float
AS
External Name [CLRFuncs].[_4screen.CSB.SQL_CLR.Distance].[CalcDistance]

GO