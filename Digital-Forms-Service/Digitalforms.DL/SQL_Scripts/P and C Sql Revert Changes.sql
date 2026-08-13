
GO
ALTER TABLE [dbo].[Forms] DROP COLUMN [ParentFId]  
GO
DROP TABLE DependentForms
GO
DROP TABLE ChildConfigs
GO
DROP TABLE ParentChildConfig 
GO
DROP TABLE ParentChild 
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

commit tran t1

select 'successfully deleted'

end try

begin catch
rollback tran t1
select @@ERROR 
end catch