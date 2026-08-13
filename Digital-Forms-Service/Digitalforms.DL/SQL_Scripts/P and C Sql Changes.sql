
GO
PRINT N'Altering Table [dbo].[Forms]...';


GO
ALTER TABLE [dbo].[Forms]
    ADD [ParentFId] BIGINT NULL;


GO
PRINT N'Altering Table [dbo].[OrganisationDetails]...';


GO
ALTER TABLE [dbo].[OrganisationDetails] ALTER COLUMN [UKPRN] NVARCHAR (100) NULL;


GO
PRINT N'Creating Table [dbo].[ChildConfigs]...';


GO
CREATE TABLE [dbo].[ChildConfigs] (
    [CCID]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [PCCID]            BIGINT         NOT NULL,
    [ChildId]          VARCHAR (255)  NULL,
    [ChildFormName]    VARCHAR (255)  NULL,
    [ChildFormTitle]    VARCHAR (255)  NULL,
    [CardOrder]        INT            NULL,
    [IsDependentForms] BIT            NULL,
    [DateComponent]    NVARCHAR (255) NULL,
    [HelpText]         VARCHAR (500)  NULL,
    [ParentId]         NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([CCID] ASC)
);


GO
PRINT N'Creating Table [dbo].[DependentForms]...';


GO
CREATE TABLE [dbo].[DependentForms] (
    [DFID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [CCID]            BIGINT         NOT NULL,
    [FormId]          VARCHAR (255)  NULL,
    [DependentStatus] VARCHAR (100)  NULL,
    [MainParentId]    NVARCHAR (100) NULL,
    [FormName]        NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([DFID] ASC)
);


GO
PRINT N'Creating Table [dbo].[ParentChild]...';


GO
CREATE TABLE [dbo].[ParentChild] (
    [PCID]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [FID]                 BIGINT         NOT NULL,
    [IsMainParent]        BIT            NULL,
    [IsParentChildConfig] BIT            NULL,
    [Name]                NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([PCID] ASC)
);


GO
PRINT N'Creating Table [dbo].[ParentChildConfig]...';


GO
CREATE TABLE [dbo].[ParentChildConfig] (
    [PCCID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PCID]           BIGINT        NOT NULL,
    [Description]    VARCHAR (255) NULL,
    [ChildHeading]   VARCHAR (255) NULL,
    [IsChildConfigS] BIT           NULL,
    PRIMARY KEY CLUSTERED ([PCCID] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ChildConfigs__PCCID]...';


GO
ALTER TABLE [dbo].[ChildConfigs] WITH NOCHECK
    ADD CONSTRAINT [FK_ChildConfigs__PCCID] FOREIGN KEY ([PCCID]) REFERENCES [dbo].[ParentChildConfig] ([PCCID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_DependentForms__CCID]...';


GO
ALTER TABLE [dbo].[DependentForms] WITH NOCHECK
    ADD CONSTRAINT [FK_DependentForms__CCID] FOREIGN KEY ([CCID]) REFERENCES [dbo].[ChildConfigs] ([CCID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ParentChild__FID]...';


GO
ALTER TABLE [dbo].[ParentChild] WITH NOCHECK
    ADD CONSTRAINT [FK_ParentChild__FID] FOREIGN KEY ([FID]) REFERENCES [dbo].[Forms] ([FID]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ParentChildConfig__PCID]...';


GO
ALTER TABLE [dbo].[ParentChildConfig] WITH NOCHECK
    ADD CONSTRAINT [FK_ParentChildConfig__PCID] FOREIGN KEY ([PCID]) REFERENCES [dbo].[ParentChild] ([PCID]);


GO
PRINT N'Altering Procedure [dbo].[DeleteFormData]...';


GO
ALTER proc [dbo].[DeleteFormData]
@id nvarchar(250)
as

declare @formid bigint

select @formid=fid from forms where FormId=@id



begin tran t1
--delete data 
begin try
--delete FROM ComponentDatasetSchemaPropertyDetails where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@formid )))
--delete FROM DatasetDataDetails  where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@formid )))
--delete FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@formid ))

--delete FROM CalculationComponentDetails  where CALCID in (SELECT CALCID FROM Calculations  where fid=@formid )
--delete FROM Calculations  where fid=@formid 

--delete FROM Sections  where fid=@formid 

--delete FROM TabChildDetails  where tabid in (SELECT tabid FROM TabDetails  where fid=@formid ) 
--delete FROM TabDetails  where fid=@formid 

--delete FROM OutputDetails  where OUID in (SELECT OUID FROM Outputs  where fid=@formid )
--delete FROM Outputs  where fid=@formid 

--delete FROM Fees  where fid=@formid 
--delete FROM MetaData  where fid=@formid 

--delete FROM ConditionDetails  where CNID in (SELECT cnid FROM Conditions  where fid=@formid )
--delete FROM Conditions  where fid=@formid 

--delete FROM ComponentAdditionalSettings where CMPID in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@formid ))
--delete FROM components where pgid in (SELECT pgid  FROM pages where fid=@formid )

--delete FROM DesignDataSetDetails  where DDSID in (SELECT DDSID FROM DesignDataSet  where fid=@formid )
--delete FROM DesignDataSet  where fid=@formid 
--delete FROM documents  where fid=@formid 
--delete FROM ListItems  where LSTID in (SELECT LSTID FROM list  where fid=@formid )
--delete FROM list  where fid=@formid

--delete FROM PageChildSettings where PGID in (SELECT PGID FROM pages where fid=@formid)
--delete FROM pages where fid=@formid
--delete FROM Forms where fid=@formid 

update Forms set Status= Cast(0 as bit) where FID=@formid

IF EXISTS (SELECT 1 FROM ParentChild WHERE FID = @formid)
BEGIN
    DELETE FROM DependentForms WHERE CCID IN (SELECT CCID FROM ChildConfigs WHERE PCCID IN (SELECT PCCID FROM ParentChildConfig WHERE PCID IN (SELECT PCID FROM ParentChild WHERE FID = @formid)));
    DELETE FROM ChildConfigs WHERE PCCID IN (SELECT PCCID FROM ParentChildConfig WHERE PCID IN (SELECT PCID FROM ParentChild WHERE FID = @formid));
    DELETE FROM ParentChildConfig WHERE PCID IN (SELECT PCID FROM ParentChild WHERE FID = @formid);
    DELETE FROM ParentChild WHERE FID = @formid;
END

IF EXISTS (SELECT 1 FROM ChildConfigs WHERE ChildId = @id)
BEGIN
    DELETE FROM DependentForms WHERE CCID IN (SELECT CCID FROM ChildConfigs WHERE ChildId = @id);
    DELETE FROM ChildConfigs WHERE ChildId = @id;
END

IF EXISTS (SELECT 1 FROM DependentForms WHERE FormId = @id)
BEGIN
    DELETE FROM DependentForms WHERE FormId = @id;
END

commit tran t1

select 'successfully deleted'

end try

begin catch
rollback tran t1
select @@ERROR 
end catch
GO
