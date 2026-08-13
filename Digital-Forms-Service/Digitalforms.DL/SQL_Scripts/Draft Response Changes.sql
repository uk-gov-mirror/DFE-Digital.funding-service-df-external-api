

GO
PRINT N'Creating Table [dbo].[DraftResponse]...';


GO
CREATE TABLE [dbo].[DraftResponse] (
    [DRID]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [Id]                   NVARCHAR (250) NULL,
    [Formid]               NVARCHAR (250) NULL,
    [UserOrgID]            BIGINT         NULL,
    [Progress]             TEXT           NULL,
    [Key]                  NVARCHAR (250) NULL,
    [Answer]               TEXT           NULL,
    [PreviousPage]         NVARCHAR (MAX) NULL,
    [OUID]                 BIGINT         NULL,
    [RSPID]                BIGINT         NULL,
    [FormDataId]           NVARCHAR (MAX) NULL,
    [UserCompletedSummary] NVARCHAR (MAX) NULL,
    [Reference]            NVARCHAR (MAX) NULL,
    [ReferenceIsStored]    NVARCHAR (MAX) NULL,
    [UpdatedOn]            DATETIME       NULL,
    [Type]                 NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([DRID] ASC)
);


GO
PRINT N'Creating Table [dbo].[SubmissionFormLog]...';


GO
CREATE TABLE [dbo].[SubmissionFormLog] (
    [SFid]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [id]                 VARCHAR (500) NULL,
    [SubmissionFormName] VARCHAR (500) NULL,
    [EmailSent]          BIT           NULL,
    [EmailHadAttachment] BIT           NULL,
    [EmailAddress]       VARCHAR (500) NULL,
    [TemplateId]         VARCHAR (500) NULL,
    [EmailSentOn]        VARCHAR (500) NULL,
    [Createdon]          DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([SFid] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_DraftResponse_UserOrgID]...';


GO
ALTER TABLE [dbo].[DraftResponse] WITH NOCHECK
    ADD CONSTRAINT [FK_DraftResponse_UserOrgID] FOREIGN KEY ([UserOrgID]) REFERENCES [dbo].[UserOrganisationDetails] ([UserOrgID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_DraftResponse_OUID]...';


GO
ALTER TABLE [dbo].[DraftResponse] WITH NOCHECK
    ADD CONSTRAINT [FK_DraftResponse_OUID] FOREIGN KEY ([OUID]) REFERENCES [dbo].[Outputs] ([OUID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_DraftResponse_RSPID]...';


GO
ALTER TABLE [dbo].[DraftResponse] WITH NOCHECK
    ADD CONSTRAINT [FK_DraftResponse_RSPID] FOREIGN KEY ([RSPID]) REFERENCES [dbo].[Responses] ([RSPID]);


create nonclustered index index_DR_id on DraftResponse (Id) include(DRID)
create nonclustered index index_UOD_UID on UserOrganisationDetails ([UID]) include([UserOrgID])
create nonclustered index index_UOD_URGID on UserOrganisationDetails ([ORGID]) include([UserOrgID])
 
CREATE NONCLUSTERED INDEX [nci_msft_1_ConditionDetails_2A9C463622E347293F5C6CAAB8A0AB3B] ON [dbo].[ConditionDetails] ([CNID]) INCLUDE ([PropName], [PropType], [PropValue], [SubsetNo]) WITH (ONLINE = ON)
 
CREATE NONCLUSTERED INDEX [nci_msft_1_ListItems_07CE36D973F1E122666F6C2ECD542C8E] ON [dbo].[ListItems] ([LSTID]) INCLUDE ([LSTItemCondition], [LSTItemDesc], [LSTItemLinks], [LSTItemsText], [LSTItemValue], [LSTOrder]) WITH (ONLINE = ON)