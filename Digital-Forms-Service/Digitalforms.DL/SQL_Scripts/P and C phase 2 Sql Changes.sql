
GO
ALTER TABLE ChildConfigs 
ADD Condition VARCHAR(100) 

GO
ALTER TABLE ChildConfigs 
ADD ConditionName VARCHAR(500)

GO
ALTER TABLE ChildConfigs 
ADD IsMainChild	BIT		

GO
CREATE NONCLUSTERED INDEX [index_CD_URGID] ON [dbo].[ConditionDetails]([CNID] ASC)
INCLUDE([PropName], [PropType], [PropValue], [SubsetNo]);

GO
ALTER TABLE ParentChildConfig
ALTER COLUMN [Description] VARCHAR(MAX)

GO
ALTER TABLE ParentChildConfig
ALTER COLUMN [ChildHeading] VARCHAR(1000)