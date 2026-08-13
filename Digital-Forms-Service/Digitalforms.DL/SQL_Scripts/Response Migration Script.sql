BEGIN TRAN migration

BEGIN try
    DECLARE @responsejsondata NVARCHAR(max)
    DECLARE responsesjson CURSOR FOR SELECT data FROM responsesjson

    OPEN responsesjson

    FETCH next FROM responsesjson
    INTO @responsejsondata

    WHILE @@FETCH_STATUS = 0
    BEGIN

        DECLARE @FormId NVARCHAR(250),
                @UserID NVARCHAR(250),
                @UKPRN NVARCHAR(250),
                @UID NVARCHAR(250),
                @ORGID NVARCHAR(250),
                @MtdtId NVARCHAR(250),
                @UserOrgID NVARCHAR(250),
                @RSPID NVARCHAR(250),
                @RspQstId NVARCHAR(250)


        DECLARE @Question NVARCHAR(max),
                @Questionsdata NVARCHAR(max)

        SET @FormId = JSON_VALUE(@responsejsondata, '$.formId')
        SET @UserID = JSON_VALUE(@responsejsondata, '$.user.id')
        SET @UKPRN = JSON_VALUE(@responsejsondata, '$.user.organization.ukprn')

        IF NOT EXISTS (SELECT 1 FROM UserDetails where UserId = @UserID)
        BEGIN
            INSERT INTO UserDetails
            SELECT Json_value(@responsejsondata, '$.user.id') AS [UserId],
                   Json_value(@responsejsondata, '$.user.name') AS [Name],
                   Json_value(@responsejsondata, '$.user.email') AS [Email],
                   null AS [Status]

            SET @UID = @@identity
        END
        ELSE
        BEGIN
            SELECT @UID = [UID]
            FROM [UserDetails]
            where UserId = @UserID
        END

        IF NOT EXISTS (SELECT 1 FROM [OrganisationDetails] where UKPRN = @UKPRN)
        BEGIN
            INSERT INTO [OrganisationDetails]
            SELECT Json_value(@responsejsondata, '$.user.organization.ukprn') AS [UKPRN],
                   Json_value(@responsejsondata, '$.user.organization.urn') AS [URN],
                   Json_value(@responsejsondata, '$.user.organization.ukprn') AS [AdminCode],
                   Json_value(@responsejsondata, '$.user.organization.name') AS [Name]

            SET @ORGID = @@identity
        END
        ELSE
        BEGIN
            SELECT @ORGID = ORGID
            FROM [OrganisationDetails]
            where UKPRN = @UKPRN
        END

        INSERT INTO [UserOrganisationDetails]
        select @UID,
               @ORGID

        SET @UserOrgID = @@identity

        INSERT INTO [MetaData]
        select NULL AS [FID],
               'paymentSkipped' AS [PropName],
               Json_value(@responsejsondata, '$.metadata.paymentSkipped') AS [PropValue],
               'paymentSkipped' AS [PropType]

        SET @MtdtId = @@identity

        INSERT INTO [Responses]
        SELECT Json_value(@responsejsondata, '$.formId') AS [FID],
               GETDATE() AS [UpdatedOn],
               @UID AS [UpdatedBy],
               1 AS [ResponseStatus],
               Json_value(@responsejsondata, '$.id') AS [Id],
               Json_value(@responsejsondata, '$.name') AS [FormName],
               @UID AS [UserId],
               @MtdtId AS [MtdtId],
               0 AS isUAT,
               @UserOrgID AS UserOrgID

        SET @RSPID = @@identity

        DECLARE Questions CURSOR FOR
        SELECT [value]
        FROM OPENJSON(JSON_QUERY(@responsejsondata, '$.questions')) AS JSON
        OPEN Questions
        FETCH NEXT FROM Questions
        INTO @Question
        WHILE @@FETCH_STATUS = 0
        BEGIN

            INSERT INTO [ResponseQuestions]
            SELECT @RSPID AS [RspId],
                   Json_value(@Question, '$.question') AS [Question]

            SET @RspQstId = @@identity

            DECLARE QuestionsData CURSOR FOR
            SELECT [value]
            FROM OPENJSON(@Question, '$.fields') AS JSON
            OPEN QuestionsData
            FETCH NEXT FROM QuestionsData
            INTO @Questionsdata
            WHILE @@FETCH_STATUS = 0
            BEGIN

                INSERT INTO [ResponseQuestionData]
                SELECT @RspQstId AS [RspQstId],
                       Json_value(@Questionsdata, '$.key') AS [Key],
                       Json_value(@Questionsdata, '$.title') AS [Title],
                       Json_value(@Questionsdata, '$.type') AS [Type],
                       Json_value(@Questionsdata, '$.answer') AS [Answer]

                SET @Questionsdata = ''
                FETCH NEXT FROM QuestionsData
                INTO @Questionsdata
            END
            CLOSE QuestionsData
            DEALLOCATE QuestionsData
            SET @Question = ''
            FETCH NEXT FROM Questions
            INTO @Question
        END
        CLOSE Questions
        DEALLOCATE Questions

        SET @responsejsondata = ''

        FETCH next FROM responsesjson
        INTO @responsejsondata
    END

    CLOSE responsesjson

    DEALLOCATE responsesjson

    SET ansi_warnings ON;
END try
BEGIN catch
    SELECT 'Error_Message: ' + Error_message() + ' Error_Line: ' + Error_line() AS ErrorMessage

    ROLLBACK TRAN migration
END catch

COMMIT TRAN migration