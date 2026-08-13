alter table designdatasetdetails add MarkForCalculation bit default 0
alter table designdatasetdetails add Checked bit default 0

update designdatasetdetails set MarkForCalculation=0 
update designdatasetdetails set Checked=0 


ALTER TABLE ComponentDatasetSchemaDetails
ADD ColumnOrder INT
 
ALTER TABLE ListItems
ADD LSTOrder INT



alter table forms alter column ConfirmationMsg nvarchar(max)


alter table tabchilddetails alter column [Value] nvarchar(max)
--
// do not execute

CREATE TABLE [UserOrganisationDetails]
(
UserOrgID BIGINT IDENTITY(1,1) PRIMARY KEY,
UID BIGINT NULL,
ORGID BIGINT NULL,
)   

ALTER TABLE [dbo].[UserOrganisationDetails] WITH CHECK ADD CONSTRAINT [FK_UserOrganisationDetails_Userid] FOREIGN KEY([UID])
REFERENCES [dbo].[UserDetails] ([UID])

ALTER TABLE [dbo].[UserOrganisationDetails] WITH CHECK ADD CONSTRAINT [FK_UserOrganisationDetails_ORGID] FOREIGN KEY([ORGID])
REFERENCES [dbo].[OrganisationDetails] ([ORGID])
 
ALTER TABLE [dbo].[Responses] ADD UserOrgID BIGINT NULL

ALTER TABLE [dbo].[Responses] WITH CHECK ADD CONSTRAINT [FK_Responses_UserOrgID] FOREIGN KEY([UserOrgID])
REFERENCES [dbo].[UserOrganisationDetails] ([UserOrgID])
// end
 until this point this is executed inlive as of dark release in feb.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

alter table Components alter column Hint nvarchar(4000)


alter table forms alter column Declaration nvarchar(max)

alter table components add  CmpOrder int




create nonclustered index index_UkprnPrvdta_pmid on [UKPRNProvidersData] (pmid) include (ukprn)
create nonclustered index index_UrnPrvdta_pmid on [URNProvidersData] (pmid) include (urn)
create nonclustered index index_AdmncdPrvdta_pmid on [AdminCodeProvidersData] (pmid) include (admincode)



vimal changes for uid/org details/calculation addtional settings



/****** Object:  Table [dbo].[UserOrganisationDetails]    Script Date: 22/03/2024 11:21:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserOrganisationDetails](
	[UserOrgID] [bigint] IDENTITY(1,1) NOT NULL,
	[UID] [bigint] NULL,
	[ORGID] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserOrgID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserOrganisationDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisationDetails_ORGID] FOREIGN KEY([ORGID])
REFERENCES [dbo].[OrganisationDetails] ([ORGID])
GO

ALTER TABLE [dbo].[UserOrganisationDetails] CHECK CONSTRAINT [FK_UserOrganisationDetails_ORGID]
GO

ALTER TABLE [dbo].[UserOrganisationDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisationDetails_Userid] FOREIGN KEY([UID])
REFERENCES [dbo].[UserDetails] ([UID])
GO

ALTER TABLE [dbo].[UserOrganisationDetails] CHECK CONSTRAINT [FK_UserOrganisationDetails_Userid]
GO

DROP INDEX index_orgzn_uid ON [OrganisationDetails]
GO

ALTER TABLE [OrganisationDetails]
DROP CONSTRAINT [FK_OrganisationDetails.UID]
GO

ALTER TABLE OrganisationDetails
DROP COLUMN [UID]
GO

ALTER TABLE Responses
ADD UserOrgID BIGINT
GO

ALTER TABLE [dbo].[Responses]  WITH CHECK ADD  CONSTRAINT [FK_Responses_UserOrgID] FOREIGN KEY([UserOrgID])
REFERENCES [dbo].[UserOrganisationDetails] ([UserOrgID])


GO
ALTER TABLE [dbo].[OrganisationDetails] ALTER COLUMN [UKPRN] NVARCHAR (50) NULL;


GO
CREATE TABLE [dbo].[CalculationAdditionalSettings] (
    [CALASID]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [CALCID]    BIGINT          NOT NULL,
    [PropName]  NVARCHAR (250)  NULL,
    [PropValue] NVARCHAR (1000) NULL,
    [PropType]  NVARCHAR (250)  NULL,
    [RowNumber] INT             NULL,
    PRIMARY KEY CLUSTERED ([CALASID] ASC)
);


GO
ALTER TABLE [dbo].[CalculationAdditionalSettings] WITH NOCHECK
ADD CONSTRAINT [FK_CalculationAdditionalSettings.CALCID] FOREIGN KEY ([CALCID]) REFERENCES [dbo].[Calculations] ([CALCID]);

