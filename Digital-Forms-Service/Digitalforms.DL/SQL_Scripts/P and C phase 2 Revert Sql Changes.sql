
GO
ALTER TABLE childconfigs 
DROP COLUMN Condition  

GO
ALTER TABLE childconfigs 
DROP COLUMN ConditionName 

GO
ALTER TABLE childconfigs 
DROP COLUMN IsMainChild	

GO
DROP INDEX [index_CD_URGID]
ON  [dbo].[ConditionDetails]

GO
ALTER TABLE ParentChildConfig
ALTER COLUMN [Description] VARCHAR(255)

GO
ALTER TABLE ParentChildConfig
ALTER COLUMN [ChildHeading] VARCHAR(255)