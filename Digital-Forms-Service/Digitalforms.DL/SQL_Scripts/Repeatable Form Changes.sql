--ALTER TABLE [dbo].[DraftResponse] DROP CONSTRAINT [FK_DraftResponse_OUID];
 
 
--GO
--PRINT N'Altering Table [dbo].[DraftResponse]...';
 
 
--GO
--ALTER TABLE [dbo].[DraftResponse] DROP COLUMN [PreviousPage], COLUMN [Progress];
 
 
--GO
--PRINT N'Creating Index [dbo].[DraftResponse].[index_DR_ID_All]...';
 
 
--GO
--CREATE NONCLUSTERED INDEX [index_DR_ID_All]
--    ON [dbo].[DraftResponse]([Id] ASC)
--    INCLUDE([Formid], [UserOrgID], [Key], [OUID], [RSPID], [FormDataId], [UserCompletedSummary], [Reference], [ReferenceIsStored], [UpdatedOn], [Type]);
 
 
--GO
--PRINT N'Altering Table [dbo].[OrganisationDetails]...';
 
 
--GO
--ALTER TABLE [dbo].[OrganisationDetails] ALTER COLUMN [UKPRN] NVARCHAR (50) NULL;
 
 
--GO
PRINT N'Altering Table [dbo].[Sections]...';
 
 
GO
ALTER TABLE [dbo].[Sections]
    ADD [Scnumbercomp]      VARCHAR (250) NULL,
        [repeatablesection] BIT           DEFAULT ((0)) NULL,
        [Scconditioncomp]   VARCHAR (250) NULL;
 
 
GO
PRINT N'Creating Table [dbo].[RepeatableFormsData]...';
 
 
GO
CREATE TABLE [dbo].[RepeatableFormsData] (
    [RFID]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [ID]        VARCHAR (100)   NULL,
    [CreatedOn] DATETIME        NULL,
    [UpdatedOn] DATETIME        NULL,
    [Status]    BIT             NULL,
    [FormData]  VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([RFID] ASC)
);
 
 
GO
--PRINT N'Creating Foreign Key [dbo].[FK_DraftResponse_OUID]...';
 
 
--GO
--ALTER TABLE [dbo].[DraftResponse] WITH NOCHECK
--    ADD CONSTRAINT [FK_DraftResponse_OUID] FOREIGN KEY ([OUID]) REFERENCES [dbo].[Outputs] ([OUID]);
 
 
--GO