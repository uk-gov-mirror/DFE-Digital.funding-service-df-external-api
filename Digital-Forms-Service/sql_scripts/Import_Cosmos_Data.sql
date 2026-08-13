--> Optional - a MASTER KEY is not required if a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'YourStrongPassword1';
GO
--> Optional - a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
alter DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
 WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
 SECRET = 'sp=r&st=2023-08-24T15:13:17Z&se=2023-08-24T23:13:17Z&spr=https&sv=2022-11-02&sr=c&sig=r34tUSsdHPKZeoeCmp%2BH%2B8uyHB77XM5GLDSNOm%2Fe68c%3D';
 -- NOTE: Make sure that you don't have a leading ? in SAS token, and
 -- that you have at least read permission on the object that should be loaded srt=o&sp=r, and
 -- that expiration period is valid (all dates are in UTC time)

create EXTERNAL DATA SOURCE MyTempAzureBlobStorage2
WITH ( TYPE = BLOB_STORAGE,
          LOCATION = 'https://s255d01rgdcshareda39c.blob.core.windows.net/sql'
          , CREDENTIAL= MyAzureBlobStorageCredential --> CREDENTIAL is not required if a blob is configured for public (anonymous) access!
);

insert into formsjson(data)
select value FROM OPENROWSET(BULK 'forms-df-prod.json',DATA_SOURCE = 'MyTempAzureBlobStorage',SINGLE_CLOB) as j
--select value FROM OPENROWSET(BULK 'new_tabs_form.json',DATA_SOURCE = 'MyTempAzureBlobStorage',SINGLE_CLOB) as j
CROSS APPLY OPENJSON(BulkColumn)


select * from formsjson
--insert into formsjson(data)
--select value FROM OPENROWSET(BULK 'forms-df-prod.json',DATA_SOURCE = 'MyTempAzureBlobStorage',SINGLE_CLOB) as j
--CROSS APPLY OPENJSON(BulkColumn)

select * from providersmappingjson
--insert into providersmappingjson(data)
--select value FROM OPENROWSET(BULK 'providers-mapping-df-prod.json',DATA_SOURCE = 'MyTempAzureBlobStorage',SINGLE_CLOB) as j
--CROSS APPLY OPENJSON(BulkColumn)

select * from responsesjson
--insert into responsesjson(data)
--select value FROM OPENROWSET(BULK 'responses-df-prod.json',DATA_SOURCE = 'MyTempAzureBlobStorage',SINGLE_CLOB) as j
--CROSS APPLY OPENJSON(BulkColumn)

