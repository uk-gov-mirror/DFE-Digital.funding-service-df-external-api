GO
PRINT N'Creating Table [dbo].[DCData]...';
CREATE TABLE [dbo].[DCData](
	[DCDID] [bigint] IDENTITY(1,1) NOT NULL,
	[fileName] [varchar](1000) NULL,
	[filePath] [varchar](1000) NULL,
	[fileStatus] [varchar](100) NULL,
	[sourceSystem] [varchar](100) NULL,
	[scanStatus] [varchar](100) NULL,
	[fileId] [varchar](100) NULL,
	[DateCreated] DATETIME2(3) NOT NULL
    CONSTRAINT DF_DCData_DateCreated DEFAULT (SYSDATETIME())
PRIMARY KEY CLUSTERED 
(
	[DCDID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
PRINT N'Creating Table [dbo].[DCDatajson]..';
create table DCDatajson(
id bigint primary key identity(1,1),
[data] varchar(max)
)

BEGIN TRAN migration

BEGIN try 
	DECLARE @dcDatajsondata NVARCHAR(max)
    DECLARE dcDatajson CURSOR FOR SELECT data FROM dcDatajson 
    OPEN dcDatajson
	FETCH next FROM dcDatajson
    INTO @dcDatajsondata
    WHILE @@FETCH_STATUS = 0
    BEGIN  
        insert into DCData ( [fileName], filePath, fileStatus, sourceSystem, scanStatus, fileId)
        SELECT  
		    JSON_VALUE(@dcDatajsondata, '$.file.fileName')     AS [fileName],
            JSON_VALUE(@dcDatajsondata, '$.file.filePath')     AS [filePath],
            JSON_VALUE(@dcDatajsondata, '$.file.fileStatus')   AS [fileStatus],
            JSON_VALUE(@dcDatajsondata, '$.file.sourceSystem') AS [sourceSystem],
            JSON_VALUE(@dcDatajsondata, '$.file.scanStatus')   AS [scanStatus],
            JSON_VALUE(@dcDatajsondata, '$.id')           AS [fileId];
 
        SET @dcDatajsondata = ''
        FETCH next FROM dcDatajson
        INTO @dcDatajsondata
    END
    CLOSE dcDatajson
    DEALLOCATE dcDatajson
    SET ansi_warnings ON;
END try
BEGIN catch
    SELECT 'Error_Message: ' + Error_message() + ' Error_Line: ' + Error_line() AS ErrorMessage
    ROLLBACK TRAN migration
END catch
COMMIT TRAN migration
