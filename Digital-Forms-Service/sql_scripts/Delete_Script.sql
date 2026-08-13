declare @id  bigint
set @id=1

--delete data 

delete FROM ComponentDatasetSchemaPropertyDetails where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )))
delete FROM DatasetDataDetails  where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )))
delete FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id ))



delete FROM CalculationComponentDetails  where CALCID in (SELECT CALCID FROM Calculations  where fid=@id )
delete FROM Calculations  where fid=@id 

delete FROM Sections  where fid=@id 

delete FROM TabChildDetails  where tabid in (SELECT tabid FROM TabDetails  where fid=@id ) 
delete FROM TabDetails  where fid=@id 

delete FROM OutputDetails  where OUID in (SELECT OUID FROM Outputs  where fid=@id )
delete FROM Outputs  where fid=@id 

delete FROM Fees  where fid=@id 
delete FROM MetaData  where fid=@id 

delete FROM ConditionDetails  where CNID in (SELECT cnid FROM Conditions  where fid=@id )
delete FROM Conditions  where fid=@id 

 

delete FROM ComponentAdditionalSettings where CMPID in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id ))
delete FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )

delete FROM DesignDataSetDetails  where DDSID in (SELECT DDSID FROM DesignDataSet  where fid=@id )
delete FROM DesignDataSet  where fid=@id 
delete FROM documents  where fid=@id 
delete FROM ListItems  where LSTID in (SELECT LSTID FROM list  where fid=@id )
delete FROM list  where fid=@id

delete FROM PageChildSettings where PGID in (SELECT PGID FROM pages where fid=@id)
delete FROM pages where fid=@id
delete FROM Forms where fid=@id 



--select data

SELECT * FROM Forms where fid=@id 
SELECT * FROM pages where fid=@id
SELECT * FROM PageChildSettings where PGID in (SELECT PGID FROM pages where fid=@id)
SELECT * FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )
SELECT * FROM ComponentAdditionalSettings where CMPID in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id)) 
SELECT * FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id ))
SELECT * FROM ComponentDatasetSchemaPropertyDetails where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )))
SELECT * FROM DatasetDataDetails  where cdsdid in (SELECT cdsdid FROM ComponentDatasetSchemaDetails where CMPID  in (SELECT CMPID FROM components where pgid in (SELECT pgid  FROM pages where fid=@id )))
SELECT * FROM Calculations  where fid=@id 
SELECT * FROM CalculationComponentDetails  where CALCID in (SELECT CALCID FROM Calculations  where fid=@id )
SELECT * FROM Conditions  where fid=@id 
SELECT * FROM ConditionDetails  where CNID in (SELECT cnid FROM TabDetails  where fid=@id )
SELECT * FROM DesignDataSet  where fid=@id 
SELECT * FROM DesignDataSetDetails  where DDSID in (SELECT DDSID FROM DesignDataSet  where fid=@id )
SELECT * FROM documents  where fid=@id 
SELECT * FROM Fees  where fid=@id 
SELECT * FROM MetaData  where fid=@id
SELECT * FROM list  where fid=@id 
SELECT * FROM ListItems  where LSTID in (SELECT LSTID FROM list  where fid=@id )
SELECT * FROM Outputs  where fid=@id 
SELECT * FROM OutputDetails  where OUID in (SELECT OUID FROM TabDetails  where fid=@id )
SELECT * FROM Sections  where fid=@id 
SELECT * FROM TabDetails  where fid=@id 
SELECT * FROM TabChildDetails  where tabid in (SELECT tabid FROM TabDetails  where fid=@id )

--select data
--SELECT top 5 * FROM FormStatus order by 1 desc
--SELECT top 5 * FROM userdetails order by 1 desc
--SELECT top 5 * FROM Forms order by 1 desc
--SELECT top 5 * FROM pages order by 1 desc
--SELECT top 5 * FROM PageChildSettings  order by 1 desc
--SELECT top 5 * FROM components  order by 1 desc
--SELECT top 5 * FROM ComponentAdditionalSettings order by 1 desc
--SELECT top 5 * FROM ComponentDatasetSchemaDetails  order by 1 desc
--SELECT top 5 * FROM ComponentDatasetSchemaPropertyDetails order by 1 desc
--SELECT top 5 * FROM DatasetDataDetails   order by 1 desc
--SELECT top 5 * FROM documents   order by 1 desc
--SELECT top 5 * FROM Calculations   order by 1 desc
--SELECT top 5 * FROM CalculationComponentDetails   order by 1 desc
--SELECT top 5 * FROM Sections  order by 1 desc
--SELECT top 5 * FROM TabDetails   order by 1 desc
--SELECT top 5 * FROM TabChildDetails   order by 1 desc
--SELECT top 5 * FROM Outputs   order by 1 desc
--SELECT top 5 * FROM OutputDetails  order by 1 desc
--SELECT top 5 * FROM Fees   order by 1 desc
--SELECT top 5 * FROM MetaData   order by 1 desc
--SELECT top 5 * FROM Conditions  order by 1 desc
--SELECT top 5 * FROM ConditionDetails order by 1 desc
--SELECT top 5 * FROM DesignDataSet order by 1 desc
--SELECT top 5 * FROM DesignDataSetDetails   order by 1 desc
--SELECT top 5 * FROM list  order by 1 desc
--SELECT top 5 * FROM ListItems   order by 1 desc
----next set for responses
--SELECT top 5 * FROM formsjson  order by 1 desc
--SELECT top 5 * FROM OrganisationDetails   order by 1 desc
--SELECT top 5 * FROM ProviderMapping  order by 1 desc
--SELECT top 5 * FROM providersmappingjson   order by 1 desc
--SELECT top 5 * FROM responsesjson  order by 1 desc
--SELECT top 5 * FROM Responses   order by 1 desc
--SELECT top 5 * FROM ResponseSubmissionData  order by 1 desc
--SELECT top 5 * FROM Tbl_Ids   order by 1 desc


--alter table forms add constraint uniqueness unique(FormId)

---alter table forms drop constraint uniqueness
---create unique nonclustered index index_forms_formid on forms (FormId)

--select t.name, * from sys.indexes i join sys.tables t
--on t.object_id=i.object_id
--where t.type='u' order by t.name

--create nonclustered index listitem_index_lstiitemd on ListItems (lstiitemd) 



--delete from ComponentDatasetSchemaPropertyDetails dbcc checkident('ComponentDatasetSchemaPropertyDetails',reseed,0)
--delete from DatasetDataDetails dbcc checkident('DatasetDataDetails',reseed,0)
--delete from ComponentDatasetSchemaDetails dbcc checkident('ComponentDatasetSchemaDetails',reseed,0)
--delete from CalculationComponentDetails dbcc checkident('CalculationComponentDetails',reseed,0)  
--delete from Calculations dbcc checkident('Calculations',reseed,0)
--delete from Sections dbcc checkident('Sections',reseed,0)
--delete from TabChildDetails dbcc checkident('TabChildDetails',reseed,0)  
--delete from TabDetails dbcc checkident('TabDetails',reseed,0)  
--delete from OutputDetails dbcc checkident('OutputDetails',reseed,0)  
--delete from Outputs dbcc checkident('Outputs',reseed,0) 
--delete from Fees dbcc checkident('Fees',reseed,0)  
--delete from MetaData dbcc checkident('MetaData',reseed,0)  
--delete from ConditionDetails dbcc checkident('ConditionDetails',reseed,0) 
--delete from Conditions dbcc checkident('Conditions',reseed,0) 
--delete from ComponentAdditionalSettings dbcc checkident('ComponentAdditionalSettings',reseed,0) 
--delete from dbo.ResponseSubmissionData dbcc checkident('ResponseSubmissionData',reseed,0) 
--delete from components dbcc checkident('components',reseed,0) 
--delete from DesignDataSetDetails dbcc checkident('DesignDataSetDetails',reseed,0)  
--delete from DesignDataSet dbcc checkident('DesignDataSet',reseed,0)  
--delete from documents dbcc checkident('documents',reseed,0)  
--delete from ListItems dbcc checkident('ListItems',reseed,0)  
--delete from list dbcc checkident('list',reseed,0)
--delete from PageChildSettings dbcc checkident('PageChildSettings',reseed,0) 
--delete from pages dbcc checkident('pages',reseed,0)
--delete from Responses dbcc checkident('Responses',reseed,0)
--delete from Forms dbcc checkident('Forms',reseed,0)



--DROP INDEX calccompdt_index_calcid ON CalculationComponentDetails
--DROP INDEX calc_index_fid ON Calculations
--DROP INDEX cmpads_index_cmpid ON ComponentAdditionalSettings
--DROP INDEX cdsd_index_fid ON ComponentDatasetSchemaDetails
--DROP INDEX cdspd_index_cdsdid ON ComponentDatasetSchemaPropertyDetails
--DROP INDEX cmp_index_fid ON Components
--DROP INDEX cndtl_index_cdid ON ConditionDetails
--DROP INDEX cn_index_fid ON Conditions
--DROP INDEX dsddtl_index_cdsdid ON DatasetDataDetails
--DROP INDEX dds_index_fid ON DesignDataSet
--DROP INDEX dds_index_docid ON DesignDataSet
--DROP INDEX ddsdtl_index_ddsid ON DesignDataSetDetails
--DROP INDEX doc_index_fid ON Documents
--DROP INDEX fee_index_fid ON Fees
--DROP INDEX form_index_formid ON Forms
--DROP INDEX lst_index_fid ON List
--DROP INDEX lsti_index_lstid ON ListItems
--DROP INDEX mtd_index_fid ON MetaData
--DROP INDEX org_index_uid ON OrganisationDetails
--DROP INDEX opd_index_ouid ON OutputDetails
--DROP INDEX op_index_fid ON Outputs
--DROP INDEX pgchs_index_pgid ON PageChildSettings
--DROP INDEX pg_index_fid ON Pages
--DROP INDEX sec_index_fid ON Sections
--DROP INDEX tabc_index_tabid ON TabChildDetails
--DROP INDEX tab_index_fid ON TabDetails


--DROP table CalculationComponentDetails
--DROP table Calculations
--DROP table ComponentAdditionalSettings
--DROP table ComponentDatasetSchemaDetails
--DROP table ComponentDatasetSchemaPropertyDetails
--DROP table Components
--DROP table ConditionDetails
--DROP table Conditions
--DROP table DatasetDataDetails
--DROP table DesignDataSet
--DROP table DesignDataSetDetails
--DROP table Documents
--DROP table Fees
--DROP table Forms
--DROP table List
--DROP table ListItems
--DROP table MetaData
--DROP table OrganisationDetails
--DROP table OutputDetails
--DROP table Outputs
--DROP table PageChildSettings
--DROP table Pages
--DROP table Sections
--DROP table TabChildDetails
--DROP table TabDetails
--DROP table FormStatus
--DROP table UserDetails
--drop table Tbl_Ids
--drop table ResponseSubmissionData
--drop table Responses
--drop table ProviderMapping
--drop table Responsesjson
--drop table ProvidersMappingjson
--drop table formsjson