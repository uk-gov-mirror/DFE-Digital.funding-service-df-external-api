
ALTER TABLE [dbo].[RepeatableFormsMapper] DROP CONSTRAINT [FK_RepeatableFormsMapper_RFID];
ALTER TABLE [dbo].[RepeatableFormsMapper] DROP CONSTRAINT [FK_RepeatableFormsMapper_RFSID];

DROP TABLE [dbo].[RepeatableFormsMapper];
DROP TABLE [dbo].[RepeatableSectionsData];

ALTER TABLE [dbo].[DesignDataSetDetails] DROP COLUMN [Format];
ALTER TABLE [dbo].[Calculations] DROP COLUMN [Repeatable];
