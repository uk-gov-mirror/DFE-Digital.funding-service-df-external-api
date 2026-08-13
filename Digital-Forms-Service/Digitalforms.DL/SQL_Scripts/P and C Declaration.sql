
GO

ALTER TABLE ChildConfigs
DROP COLUMN ChildFormName 

GO

ALTER TABLE DependentForms
DROP COLUMN FormName 


 
GO
ALTER TABLE [dbo].[DraftResponse] DROP COLUMN [PreviousPage], COLUMN [Progress];
    
 
GO
CREATE NONCLUSTERED INDEX [index_DR_ID_All]
    ON [dbo].[DraftResponse]([Id] ASC)
    INCLUDE([Formid], [UserOrgID], [Key], [OUID], [RSPID], [FormDataId], [UserCompletedSummary], [Reference], [ReferenceIsStored], [UpdatedOn], [Type]);
  
 
GO
ALTER TABLE [dbo].[OrganisationDetails] ALTER COLUMN [UKPRN] NVARCHAR (50) NULL;
 
 
GO 
ALTER TABLE [dbo].[DraftResponse] WITH NOCHECK
    ADD CONSTRAINT [FK_DraftResponse_OUID] FOREIGN KEY ([OUID]) REFERENCES [dbo].[Outputs] ([OUID]);
 
 