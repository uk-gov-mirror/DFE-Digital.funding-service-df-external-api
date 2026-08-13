
GO
CREATE TABLE RepeatableSectionsData(
 RFSID bigint primary key identity(1,1)
,FID nvarchar(200)
,SectionName varchar(200)
,NoofRepeats INT
,Status bit
,SectionData [varbinary](max) NULL
)

GO
CREATE TABLE RepeatableFormsMapper
(
  RFMID bigint primary key identity(1,1)
 ,RFID bigint  
 ,RFSID bigint  
)


GO 
ALTER TABLE [dbo].[RepeatableFormsMapper]  WITH NOCHECK ADD  CONSTRAINT [FK_RepeatableFormsMapper_RFID] FOREIGN KEY([RFID])
REFERENCES [dbo].[RepeatableFormsData] ([RFID])

GO
ALTER TABLE [dbo].[RepeatableFormsMapper]  WITH NOCHECK ADD  CONSTRAINT [FK_RepeatableFormsMapper_RFSID] FOREIGN KEY([RFSID])
REFERENCES [dbo].[RepeatableSectionsData] ([RFSID])

GO
ALTER TABLE DesignDataSetDetails
ADD [Format] VARCHAR(100) 

GO
ALTER TABLE [dbo].[Calculations]
ADD [Repeatable] BIT NULL;

