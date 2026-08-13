BEGIN TRANSACTION;

BEGIN TRY

    -- 1. Delete child first (FK safe)
    DELETE FROM ComponentAdditionalSettings;

    -- 2. Delete parent
    DELETE FROM Components;

    -- 3. Restore Components
    SET IDENTITY_INSERT Components ON;

    INSERT INTO Components (
        CMPID,
        PGID,
        Type,
        IsEditingTabs,
        Title,
        Hint,
        Name,
        Status,
        NameHasError,
        ComponentEdited,
        AdditionalSettings,
        Content,
        Checked,
        DDSID,
        LSTID,
        DOCID,
        CmpOrder
    )
    SELECT 
        CMPID,
        PGID,
        Type,
        IsEditingTabs,
        Title,
        Hint,
        Name,
        Status,
        NameHasError,
        ComponentEdited,
        AdditionalSettings,
        Content,
        Checked,
        DDSID,
        LSTID,
        DOCID,
        CmpOrder
    FROM Components_BCKUP;

    SET IDENTITY_INSERT Components OFF;

    -- 4. Restore ComponentAdditionalSettings
    SET IDENTITY_INSERT ComponentAdditionalSettings ON;

    INSERT INTO ComponentAdditionalSettings (
        CADSTID,
        CMPID,
        PropName,
        PropValue,
        PropType
    )
    SELECT 
        CADSTID,
        CMPID,
        PropName,
        PropValue,
        PropType
    FROM ComponentAdditionalSettings_BCKUP;

    SET IDENTITY_INSERT ComponentAdditionalSettings OFF;

    COMMIT TRANSACTION;

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;

    PRINT 'Rollback failed!';
    PRINT ERROR_MESSAGE();
END CATCH;