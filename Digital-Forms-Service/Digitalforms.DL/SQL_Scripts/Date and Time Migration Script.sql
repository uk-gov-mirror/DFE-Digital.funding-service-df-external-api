

-- Drop backup tables if they already exist (optional safety)
IF OBJECT_ID('Components_BCKUP', 'U') IS NOT NULL
    DROP TABLE Components_BCKUP;

IF OBJECT_ID('ComponentAdditionalSettings_BCKUP', 'U') IS NOT NULL
    DROP TABLE ComponentAdditionalSettings_BCKUP;

-- Create backups
SELECT *
INTO Components_BCKUP
FROM Components;

SELECT *
INTO ComponentAdditionalSettings_BCKUP
FROM ComponentAdditionalSettings;


INSERT INTO ComponentAdditionalSettings (CMPID, PropName, PropValue, PropType)
SELECT c.CMPID,
       v.PropName,
       v.PropValue,
       v.PropType
FROM Components c
CROSS JOIN (
    VALUES 
        ('hideDay',   'False', 'date'),
        ('hideMonth', 'False', 'date'),
        ('hideYear',  'False', 'date')
) v (PropName, PropValue, PropType)
WHERE c.[Type] IN (    'DateField',    'DatePartsField',    'DateTimeField',    'DateTimePartsField',    'TimeField')



-- DateField
update ComponentAdditionalSettings set PropValue = 'true' where CMPID IN (select CMPID from Components where [Type]= 'DateField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT) > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN (select CMPID from Components where [Type]= 'DateField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT) < 1
update ComponentAdditionalSettings set PropValue = 'true' where CMPID IN (select CMPID from Components where [Type]= 'DateField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT) > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN (select CMPID from Components where [Type]= 'DateField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT) < 1
update Components set [Type] = 'DateAndTimeField' where [Type]= 'DateField'
 
-- DatePartsField
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DatePartsField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)    > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DatePartsField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)  < 1
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DatePartsField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DatePartsField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  < 1
update Components set [Type] = 'DateAndTimeField' where [Type]= 'DatePartsField'
 
-- DateTimeField
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DateTimeField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)    > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DateTimeField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)  < 1
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DateTimeField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DateTimeField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  < 1
update Components set [Type] = 'DateAndTimeField' where [Type]= 'DateTimeField'
 
-- DateTimePartsField
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DateTimePartsField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)    > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DateTimePartsField') and PropName = 'maxDaysInPast' and  TRY_CAST(PropValue AS INT)  < 1
update ComponentAdditionalSettings set PropValue = 'true' where CMPID  IN  (select CMPID from Components where [Type]= 'DateTimePartsField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  > 0
update ComponentAdditionalSettings set PropValue = 'false' where CMPID IN  (select CMPID from Components where [Type]= 'DateTimePartsField') and PropName = 'maxDaysInFuture' and  TRY_CAST(PropValue AS INT)  < 1
update Components set [Type] = 'DateAndTimeField' where [Type]= 'DateTimePartsField'
 
--TimeField
update Components set [Type] = 'DateAndTimeField' where [Type]= 'TimeField'




