SELECT * FROM [dbo].[formsjson]

SELECT * FROM [dbo].[Forms]


BEGIN TRAN Migration
	BEGIN TRY
		DECLARE @formjsondata nvarchar(max)
		
		DECLARE formsjson CURSOR FOR SELECT DATA FROM formsjson --WHERE id=14
		OPEN formsjson
		FETCH NEXT FROM formsjson INTO @formjsondata

		WHILE @@FETCH_STATUS = 0
			BEGIN
				--declare all the general usage variables here
				--these can be utilised across the entire script
				DECLARE @FormId nvarchar(250), @FormStatus nvarchar(250), @Createdbyid nvarchar(250),
					@Createdbyname nvarchar(250), @Modifiedbyid nvarchar(250), @Modifiedbyname nvarchar(250),
					@csv nvarchar(250), @selectedDocument nvarchar(250), @tabchildetail nvarchar(250), @tabName nvarchar(250), @type nvarchar(250), @tabsID nvarchar(250),
					@compTest nvarchar(250)
				DECLARE @status bit
				DECLARE @count int, @FID int, @PGID int, @CMPID int, @componentSettings int, @LSTID int, @CDSDID int, @CALCID int, 
					@TABID int, @CADSTID int, @OUID int, @CNID int, @SUBID int, @DDSID int, @RowID int
				DECLARE @pagedata nvarchar(max), @pagechildrendata nvarchar(max), @componentdata nvarchar(max), 
					@Document nvarchar(max), @designDS nvarchar(max), @list nvarchar(max), @componentkey nvarchar(max),
					@componentvalue nvarchar(max), @componenttype nvarchar(max), @listItemdata nvarchar(max), @columndata nvarchar(max),
					@columnschemaval nvarchar(max), @columnschemakey nvarchar(max), @calculationdata nvarchar(max), @sectiondata nvarchar(max),
					@tabsjson nvarchar(max), @tabData nvarchar(max), @outputdata nvarchar(max), @outputdetailskey nvarchar(max), @outputdetailsvalue nvarchar(max),
					@conditiondata nvarchar(max), @subconditiondata nvarchar(max), @subconditionkey nvarchar(max), @subconditionvalue nvarchar(max),
					@DDSDetails nvarchar(max), @DDSDetailsRow nvarchar(max)

				SET @FormId= JSON_VALUE(@formjsondata, '$.id')
				SET @FormStatus= JSON_VALUE(@formjsondata, '$.formStatus')
				SET @Createdbyid= JSON_VALUE(@formjsondata, '$.userId')
				SET @Modifiedbyid= JSON_VALUE(@formjsondata, '$.lastUpdatedById')
				SET @Createdbyname= JSON_VALUE(@formjsondata, '$.createdBy')
				SET @Modifiedbyname= JSON_VALUE(@formjsondata, '$.lastUpdatedByName')
				SET @status = 1

				--1. Forms Status table data

				--insert if any new form status comes in.

					select @count = count(1) from FormStatus where status=@FormStatus
					if @count = 0
						insert into FormStatus select (@FormStatus)

				--2. Users table data
				-- insert if the user data is not available in user table

					select @count = count(1) from UserDetails where userId=@Createdbyid
					if @count = 0
						insert into UserDetails select  @Createdbyid,@Createdbyname,'',@status

					select @count = count(1) from UserDetails where userId=@Modifiedbyid
					if @count = 0
						insert into UserDetails select @Modifiedbyid,@Modifiedbyname,'',@status


				--inserted form status and users table up front as they are not dependent on anything else.
				--upcoming tables are all dependent on forms, so they should follow after forms insertion.

				--3. Forms table data
				SET ANSI_WARNINGS OFF;
				-- Your insert TSQL here.
				
						insert into Forms
						select 
						@FormId as FormId
						,JSON_VALUE(@formjsondata, '$.displayName') displayName
						,JSON_VALUE(@formjsondata, '$.name') as name
						,(select FSID from FormStatus where status=@FormStatus)as FSID
						,JSON_VALUE(@formjsondata, '$.version') as version
						,JSON_VALUE(@formjsondata, '$.skipSummary') skipSummary
						,JSON_VALUE(@formjsondata, '$.signInRequired') as signInRequired
						,(select UID from UserDetails where userId=@Createdbyid) as CreatedByUserId
						,JSON_VALUE(@formjsondata, '$.lastModified') as CreatedOn
						,(select UID from UserDetails where userId=@Modifiedbyid) as lastUpdatedByUserId
						,JSON_VALUE(@formjsondata, '$.lastModified') as Lastupdatedon
						,@status as [status]
						,JSON_VALUE(@formjsondata, '$.file') as [File]
						,JSON_VALUE(@formjsondata, '$.startPage') StartPage
						,JSON_VALUE(@formjsondata, '$.confirmationMsg') as confirmationMsg
						,JSON_VALUE(@formjsondata, '$.declaration') declaration
						, case when charindex(',',JSON_VALUE(@formjsondata, '$.lastDownloaded'),0) > 0 then
							try_convert(datetime,replace(JSON_VALUE(@formjsondata, '$.lastDownloaded'),',',''),103) 
							else 
							try_convert(datetime,replace(JSON_VALUE(@formjsondata, '$.lastDownloaded'),',',''),111) 
							end as lastDownloaded
						,JSON_VALUE(@formjsondata, '$.phaseBanner.phase') as PhaseBanner
						,JSON_VALUE(@formjsondata, '$.feedback.feedbackForm') FeedbackForm
						,JSON_VALUE(@formjsondata, '$.feedback.url') as [Url]
						,null as CustomSummaryMessage
						SET ANSI_WARNINGS ON;

						print 'Forms Table inserted'
					--print @@identity

				--- all other tables to follow below


					--get the pages data relevant for the form

					-- When the script is run as a whole use identity to set the form ID
					-- for test use value 1
					SET @FID = @@identity
					
					--SET @FID = 14

				-- DATASETS LOOP
					
					DECLARE lists CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.lists')) AS JSON
					OPEN lists
					FETCH NEXT FROM lists INTO @list
						WHILE @@FETCH_STATUS = 0
							BEGIN
					--4. List Table
								INSERT INTO [List]
								SELECT
									@FID AS FID
									, JSON_VALUE(@list, '$.title') AS LSTTitle
									, JSON_VALUE(@list, '$.name') AS LSTName
									, JSON_VALUE(@list, '$.type') AS LSTType
									,null as LASTDataset

								print '[List] Table inserted'

								SET @LSTID=@@identity
								--SET @LSTID=1

								DECLARE listItem CURSOR FOR SELECT [value] FROM OPENJSON(@list,'$.items') AS JSON
                OPEN listItem
                FETCH NEXT FROM listItem INTO @listItemdata
                  WHILE @@FETCH_STATUS = 0
										BEGIN
					--5. ListItems Table
											--SET IDENTITY_INSERT [ListItems] ON;
											INSERT INTO [ListItems]
											SELECT
												@LSTID as LSTID
												, JSON_VALUE(@listItemdata, '$.text') AS LSTItemsText
												, JSON_VALUE(@listItemdata, '$.value') AS LSTItemValue
												, JSON_VALUE(@listItemdata, '$.condition') AS LSTItemCondition
												, JSON_VALUE(@listItemdata, '$.description') AS LSTItemsDesc
												, null as LSTItemLinks
												, null as LSTOrder
                      --SET IDENTITY_INSERT [ListItems] OFF;
											
											print '[ListItems] Table inserted'

											SET @listItemdata = ''
											FETCH  NEXT FROM listItem INTO @listItemdata
										END
									CLOSE listItem
									DEALLOCATE listItem
								SET @list=''
								FETCH NEXT FROM lists INTO @list
							END
						CLOSE lists
						DEALLOCATE lists

					DECLARE documents CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.documents')) AS JSON
					OPEN documents
					FETCH NEXT FROM documents INTO @Document
						WHILE @@FETCH_STATUS = 0
							BEGIN
					--6. Documents table
								INSERT INTO [Documents]
								SELECT
									@FID AS FID
									, JSON_VALUE(@Document, '$.title') AS FileTitle
									, JSON_VALUE(@Document, '$.fileName') AS [FileName]
									, JSON_VALUE(@Document, '$.path') AS FilePath
									, JSON_VALUE(@Document, '$.type') AS FileType
									, JSON_VALUE(@Document, '$.id') AS FileId
									, JSON_VALUE(@Document, '$.uploadedDate') AS UploadedOn

								SET @Document=''
								FETCH NEXT FROM documents INTO @Document
							END
						CLOSE documents
						DEALLOCATE documents

					DECLARE documents CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.importedDataSets')) AS JSON
					OPEN documents
					FETCH NEXT FROM documents INTO @Document
						WHILE @@FETCH_STATUS = 0
							BEGIN
					--6. Documents Table
								INSERT INTO [Documents]
								SELECT
									@FID AS FID
									, JSON_VALUE(@Document, '$.fileTitle') AS FileTitle
									, JSON_VALUE(@Document, '$.fileName') AS [FileName]
									, JSON_VALUE(@Document, '$.path') AS FilePath
									, JSON_VALUE(@Document, '$.type') AS FileType
									, JSON_VALUE(@Document, '$.fileId') AS FileId
									, JSON_VALUE(@Document, '$.uploadedDate') AS UploadedOn
								
								SET @Document=''
								FETCH NEXT FROM documents INTO @Document
							END
						CLOSE documents
						DEALLOCATE documents

						print '[Documents]2 Table inserted'
					-- loop through the designdatasets inside the current form
					DECLARE designDS CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.designedDataSets')) AS JSON
					OPEN designDS
					FETCH NEXT FROM designDS INTO @designDS
						WHILE @@FETCH_STATUS = 0
							BEGIN
								print @designDS
					--7. DesignDataSet Table
								INSERT INTO [DesignDataSet]
								SELECT
									@FID AS FID
									, (select ISNULL(DOCID, '') from Documents where FileId=(JSON_VALUE(@designDS, '$.csvUsed')) AND FID=@FID) AS DOCID
									, JSON_VALUE(@designDS, '$.id') AS DDSetID
									, JSON_VALUE(@designDS, '$.title') AS Title
									, STRING_ESCAPE(JSON_VALUE(@designDS, '$.keyIdentifier'), 'json') AS KeyIdentifier
									, JSON_VALUE(@designDS, '$.uploadedDate') AS UploadedOn
									, JSON_VALUE(@designDS, '$.csvUsed') AS csvUsed
								
								print '[DesignDataSet] Table inserted'
--select ISNULL(DOCID, '') from Documents where FileId='WyEcBl' and 
--select * from Documents where FileId='WyEcBl'and FID=5

								SET @DDSID=@@identity
								--SET @DDSID=1
								--SET the row number value for the DesignDataSetDetails
								SET @RowID = 1

								DECLARE DDSDetails CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@designDS, '$.data')) AS JSON
								OPEN DDSDetails
								FETCH NEXT FROM DDSDetails INTO @DDSDetails
									WHILE @@FETCH_STATUS = 0
										BEGIN
					--8. DesignDataSetDetails Table
											DECLARE DDSDetailsRow CURSOR FOR SELECT [value] FROM OPENJSON(@DDSDetails) AS JSON
											OPEN DDSDetailsRow
											FETCH NEXT FROM DDSDetailsRow INTO @DDSDetailsRow
												WHILE @@FETCH_STATUS = 0
													BEGIN
														INSERT INTO [DesignDataSetDetails]
														SELECT
															@DDSID as DDSID
															, JSON_VALUE(@DDSDetailsRow, '$.index') as [index]
															, JSON_VALUE(@DDSDetailsRow, '$.type') as [Type]
															, STRING_ESCAPE(JSON_VALUE(@DDSDetailsRow, '$.value'), 'json') as [Value]
															, CASE WHEN
																	JSON_VALUE(@DDSDetailsRow, '$.bold')='true'
																THEN 1
																ELSE 0
																END as [Bold]
															, @RowID as Rowno
															, null as [MarkForCalculation]
															, null as [Checked]
														
														print '[DesignDataSetDetails] Table inserted'

														SET @DDSDetailsRow=''
														FETCH NEXT FROM DDSDetailsRow INTO @DDSDetailsRow
													END
												CLOSE DDSDetailsRow
												DEALLOCATE DDSDetailsRow
											SET @RowID=@RowID+1
											SET @DDSDetails=''
											FETCH NEXT FROM DDSDetails INTO @DDSDetails
										END
									CLOSE DDSDetails
									DEALLOCATE DDSDetails
								--INSERT DesignDataSetDetails
								
								SET @designDS=''
								FETCH NEXT FROM designDS INTO @designDS
							END
						CLOSE designDS
						DEALLOCATE designDS

				-- PAGES LOOP

					-- Open a cursor on the pages object table for the current form
					DECLARE pagejson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.pages')) AS JSON
					OPEN pagejson
					FETCH NEXT FROM pagejson INTO @pagedata
						print @pagedata
						WHILE @@FETCH_STATUS = 0
							BEGIN
					--9. Pages Table
								INSERT INTO [Pages]
								
								SELECT 
									@FID as FID
									,JSON_VALUE(@pagedata, '$.title') as Title
									,JSON_VALUE(@pagedata, '$.path') as [Path]
									,JSON_VALUE(@pagedata, '$.controller') as Controller
									,CASE WHEN
										(SELECT COUNT(*) FROM OPENJSON(JSON_QUERY(@pagedata, '$.next')))>0
										THEN 1
										ELSE 0
									 END as [Next] -- Change to 1 or 0 depending on whether it has values
									,JSON_VALUE(@pagedata, '$.section') as Section
								print '[Pages] Table inserted'
								
									-- When the script is run as a whole use identity to set the page ID
									-- for test use value 1
									SET @PGID = @@identity
									--SET @PGID = 1

					--10. PageChildSettings table
								-- Open a cursor on the next property in the current page to insert in PageChildSettings table
									DECLARE pagechildjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@pagedata, '$.next')) AS JSON
									OPEN pagechildjson
									FETCH NEXT FROM pagechildjson INTO @pagechildrendata
										WHILE @@FETCH_STATUS = 0
											BEGIN
												INSERT INTO [PageChildSettings]
												SELECT
													@PGID as PGID
													, JSON_VALUE(@pagechildrendata, '$.path') as [Path]
													, JSON_VALUE(@pagechildrendata, '$.condition') as Condition
												print '[PageChildSettings] Table inserted'

												SET @pagechildrendata=''
												FETCH NEXT FROM pagechildjson INTO @pagechildrendata
											END
									CLOSE pagechildjson
									DEALLOCATE pagechildjson

					-- COMPONENTS LOOP
								-- Open a cursor on the components property of the current page
									DECLARE componentsjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@pagedata, '$.components')) AS JSON
									OPEN componentsjson
									FETCH NEXT FROM componentsjson INTO @componentdata
										WHILE @@FETCH_STATUS = 0
											BEGIN
												SET @type = JSON_VALUE(@componentdata, '$.type')
					--11. Components Table@CMPID
												INSERT INTO [Components]
												SELECT
													@PGID as PGID
												, JSON_VALUE(@componentdata, '$.type') as [Type]
												, CASE WHEN JSON_VALUE(@componentdata, '$.isEditingTabs')='false' 
													THEN 0 ELSE JSON_VALUE(@componentdata, '$.isEditingTabs') END AS IsEditingTabs
												, JSON_VALUE(@componentdata, '$.title') as Title
												, JSON_VALUE(@componentdata, '$.hint') as Hint
												, JSON_VALUE(@componentdata, '$.name') as [Name]
												, 1 as [Status]
												, JSON_VALUE(@componentdata, '$.nameHasError') as NameHasError
												, JSON_VALUE(@componentdata, '$.componentEdited') as ComponentEdited
												, CASE WHEN 
															(SELECT COUNT(*) FROM OPENJSON(JSON_QUERY(@componentdata, '$.options')))!=0 OR
															(SELECT COUNT(*) FROM OPENJSON(JSON_QUERY(@componentdata, '$.schema')))!=0 OR
															@type='Tabs' OR
															(SELECT COUNT([key]) 
															FROM OPENJSON(@componentdata) AS json 
															WHERE [key] NOT IN ('type', 'isEditingTabs', 'title', 'hint', 'name', 
															'nameHasError', 'componentEdited', 'content', 'checked'))>2
													THEN 1
													ELSE 0
													END AS AdditionalSettings
												, JSON_VALUE(@componentdata, '$.content') AS Content
												, JSON_VALUE(@componentdata, '$.checked') as Checked
												, (SELECT DDSID FROM DesignDataSet WHERE csvUsed=JSON_VALUE(@componentdata, '$.fileId')) AS DDSID
												, (SELECT LSTID FROM List 
													WHERE LSTName=JSON_VALUE(@componentdata, '$.list') AND FID=@FID) AS LSTID
												, null as DOCID

												print '[PageChildSettings] Table inserted'
 
												-- use identity to set the Component ID. For test use value 1
												SET @CMPID = @@identity
												--SET @CMPID = 1


												--SELECT @componentSettings = COUNT([key]) FROM OPENJSON(@componentdata) AS json 
												--															WHERE [key] NOT IN ('type', 'isEditingTabs', 'title', 'hint', 'name', 
												--														'nameHasError', 'componentEdited', 'content', 'checked', 'options', 'schema')
												----if @componentSettings!=0

												IF(@type='Tabs')
													BEGIN
														SET @tabName = JSON_VALUE(@componentdata, '$.name')
														
														DECLARE compadditionalsettingsjson CURSOR FOR SELECT [key], [value], [type]
																																			FROM OPENJSON(@componentdata) AS json
																																			WHERE [key] NOT IN ('type', 'isEditingTabs', 'hint', 'name', 'options',
																																				'nameHasError', 'componentEdited', 'content', 'checked', 'columns', 'columnNames')
														OPEN compadditionalsettingsjson
														FETCH NEXT FROM compadditionalsettingsjson INTO @componentkey, @componentvalue, @componenttype
															WHILE @@FETCH_STATUS = 0
																BEGIN
																	INSERT INTO [ComponentAdditionalSettings]
																	SELECT
																		@CMPID as CMPID
																		, @componentkey as PropName
																		, @componentvalue as PropValue
																		, NULL as PropType
																	print '[ComponentAdditionalSettings] Table inserted'
																	-- Get the Id for the ComponentAdditionalSettings item created above
																		SET @CADSTID = @@identity
																		--SET @CADSTID = 1

																	SET @componentkey=''
																	SET @componentvalue=''
																	SET @componenttype=''
																	FETCH NEXT FROM compadditionalsettingsjson INTO @componentkey, @componentvalue, @componenttype
																END
														CLOSE compadditionalsettingsjson
														DEALLOCATE compadditionalsettingsjson

			--13. TabDetails table
														INSERT INTO [TabDetails]
														SELECT
															@FID AS FID
															, JSON_VALUE(@componentdata, '$.title') as TabName

														print '[TabDetails] Table inserted'

													-- Get the Id for the TabDetails just created above
														SET @TABID = @@identity
														--SET @TABID = 1

														DECLARE tabsjson CURSOR FOR SELECT [value]
																												FROM OPENJSON(JSON_QUERY(@formjsondata, '$.tabs')) AS JSON
														OPEN tabsjson
														FETCH NEXT FROM tabsjson INTO @tabsjson
															WHILE @@FETCH_STATUS = 0
																BEGIN
				--14. TabChildDetails Table
																	print @tabsjson
																	SET @tabsID=JSON_VALUE(@tabsjson, '$.id')
																	--IF @tabsID=@tabName
																		DECLARE tabData CURSOR FOR SELECT [value]
																											FROM OPENJSON(JSON_QUERY(@tabsjson, '$.tabData')) AS JSON
																		OPEN tabData
																		FETCH NEXT FROM tabData INTO @tabData
																			print @tabData
																			WHILE @@FETCH_STATUS = 0
																				BEGIN
																					INSERT INTO [TabChildDetails]
																					SELECT
																					--- TODO CHANGE FOR CADSTID
																						@CADSTID as CADSTID
																						, @TABID as TABID
																						, JSON_VALUE(@tabData, '$.tabLabel') as tabLabel
																						, JSON_VALUE(@tabData, '$.tabHeader') as tabHeader
																						, JSON_VALUE(@tabData, '$.type') as [type]
																						, JSON_VALUE(@tabData, '$.value') as [Value]
																					
																					print '[TabChildDetails] Table inserted'
																					
																					SET @tabData=''
																					FETCH NEXT FROM tabData INTO @tabData
																				END
																		CLOSE tabData
																		DEALLOCATE tabData
																	SET @tabsjson=''
																	FETCH NEXT FROM tabsjson INTO @tabsjson
																END
															CLOSE tabsjson
															DEALLOCATE tabsjson
													END

												DECLARE compadditionalsettingsjson CURSOR FOR SELECT [key], [value], [type]
																																			FROM OPENJSON(@componentdata) AS json 
																																			WHERE [key] NOT IN ('type', 'isEditingTabs', 'title', 'hint', 'name', 
																																			'nameHasError', 'componentEdited', 'content', 'checked', 'columns', 'columnNames')
												OPEN compadditionalsettingsjson
												FETCH NEXT FROM compadditionalsettingsjson INTO @componentkey, @componentvalue, @componenttype
													WHILE @@FETCH_STATUS = 0
														BEGIN
															IF @componenttype=5
																BEGIN
				--12. ComponentAdditionalSettings Table
																	INSERT INTO [ComponentAdditionalSettings]
																	SELECT 
																		@CMPID as CMPID 
																		, JSON.[key] as PropName
																		, JSON.[value] as PropValue 
																		,	@componentkey as PropType
																	FROM OPENJSON(@componentvalue) as JSON

																	print '[ComponentAdditionalSettings] Table inserted'
																	
																	-- Get the Id for the ComponentAdditionalSettings item created above
																		SET @CADSTID = @@identity
																		--SET @CADSTID = 1
																END
															ELSE IF @componenttype=4
																BEGIN
				--12. ComponentAdditionalSettings Table
																	INSERT INTO [ComponentAdditionalSettings]
																	SELECT 
																		@CMPID as CMPID 
																		,	@componentkey as PropName
																		, JSON.[value] as PropValue 
																		, null as PropType
																	FROM OPENJSON(@componentvalue) as JSON

																	print '[ComponentAdditionalSettings] Table inserted'

																		
																	-- Get the Id for the ComponentAdditionalSettings item created above
																		SET @CADSTID = @@identity
																		--SET @CADSTID = 1
																END
															ELSE IF @componenttype=1
																BEGIN
				--15. ComponentAdditionalSettings Table
																	INSERT INTO [ComponentAdditionalSettings]
																	SELECT
																		@CMPID as CMPID
																		, @componentkey as PropName
																		, @componentvalue as PropValue
																		, NULL as PropType
																	-- Get the Id for the ComponentAdditionalSettings item created above
																		SET @CADSTID = @@identity
																		--SET @CADSTID = 1
																	print '[ComponentAdditionalSettings] Table inserted'
																END
															SET @componentkey=''
															SET @componentvalue=''
															SET @componenttype=''
															FETCH NEXT FROM compadditionalsettingsjson INTO @componentkey, @componentvalue, @componenttype
														END
													CLOSE compadditionalsettingsjson
													DEALLOCATE compadditionalsettingsjson

												SET @selectedDocument = JSON_VALUE(@componentdata, '$.selectedDocument')

												DECLARE columnsjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@componentdata, '$.columns')) AS JSON
												OPEN columnsjson
												FETCH NEXT FROM columnsjson INTO @columndata
													WHILE @@FETCH_STATUS = 0
														BEGIN
					--16. ComponentDatasetSchemaDetails Table
															INSERT INTO [ComponentDatasetSchemaDetails]
															SELECT
																@CMPID AS CMPID
																, JSON_VALUE(@columndata, '$.columnId') as ColumnId
																, JSON_VALUE(@columndata, '$.columnType') as ColumnType
																, JSON_VALUE(@columndata, '$.selectedColumnHeaderType') as SelectedColumnHeaderType
																, JSON_VALUE(@columndata, '$.selectedColumnHeaderValue') as SelectedColumnHeaderValue
																, CASE WHEN JSON_VALUE(@columndata, '$.isEdited')='false' 
																	THEN 0 ELSE JSON_VALUE(@columndata, '$.isEditingTabs') END AS IsEdited
																, CASE WHEN (SELECT COUNT(*) FROM OPENJSON(JSON_QUERY(@columndata, '$.columnSchema')))=0
																	THEN 0 ELSE 1 END AS columnSchema
																, null as ColumnOrder

																-- Get the Id for the ComponentDataSchemaDetails just created above
																SET @CDSDID = @@identity
																--SET @CDSDID = 1
															
															print '[ComponentDatasetSchemaDetails] Table inserted'

														
					--17. ComponentDatasetSchemaPropertyDetails table
																DECLARE columnschemajson CURSOR FOR SELECT [key], [value]
																	FROM OPENJSON(JSON_QUERY(@columndata, '$.columnSchema')) AS JSON
																OPEN columnschemajson
																FETCH NEXT FROM columnschemajson INTO @columnschemakey, @columnschemaval
																	WHILE @@FETCH_STATUS = 0
																		BEGIN
																			INSERT INTO [ComponentDatasetSchemaPropertyDetails]
																			SELECT
																				@CDSDID as CDSDID
																				, @columnschemakey as PropName
																				, @columnschemaval as PropValue
																				, 'columnSchema' as PropType

																			SET @columnschemakey=''
																			SET @columnschemaval=''
																			FETCH NEXT FROM columnschemajson INTO @columnschemakey, @columnschemaval

																			print '[ComponentDatasetSchemaPropertyDetails] Table inserted'

																		END
																	CLOSE columnschemajson
																	DEALLOCATE columnschemajson
															SET @columndata=''
															FETCH NEXT FROM columnsjson INTO @columndata
														END
													CLOSE columnsjson
													DEALLOCATE columnsjson
												SET @componentdata=''
												FETCH NEXT FROM componentsjson INTO @componentdata
											END
									CLOSE componentsjson
									DEALLOCATE componentsjson

								SET @pagedata=''
								FETCH NEXT FROM pagejson INTO @pagedata
							END
					CLOSE pagejson
					DEALLOCATE pagejson

					-- CALCULATIONS LOOP

					-- Open a cursor on the calculations object for the current form
					DECLARE calculationjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.calculations')) AS JSON
					OPEN calculationjson
					FETCH NEXT FROM calculationjson INTO @calculationdata
						WHILE @@FETCH_STATUS = 0
							BEGIN
								print @calculationdata
					--18. Calculations table
									INSERT INTO [Calculations]
									SELECT
										@FID AS FID
										, JSON_VALUE(@calculationdata, '$.displayName') as DisplayName
										, JSON_VALUE(@calculationdata, '$.hint') as Hint
										, JSON_VALUE(@calculationdata, '$.pageLocation') as PageLocation
										, JSON_VALUE(@calculationdata, '$.type') as [Type]
										, JSON_VALUE(@calculationdata, '$.name') as [Name]
										, CASE WHEN JSON_VALUE(@calculationdata, '$.hideResult')='false' 
												THEN 0 ELSE 1 END AS HideResult
										, JSON_VALUE(@calculationdata, '$.title') as Title
										, JSON_VALUE(@calculationdata, '$.expression') as Expressions

										-- Get the Id for the ComponentDataSchemaDetails just created above
										SET @CALCID = @@identity
										--SET @CALCID = 1
									print '[Calculations] Table inserted'

									set @compTest = (SELECT COUNT(*)
																		FROM [Components] 
																		WHERE [Name]=JSON_VALUE(@calculationdata, '$.name')
																		AND [PGID]=(
																				SELECT ISNULL(PGID, 0)
																				FROM Pages 
																				WHERE Title=JSON_VALUE(@calculationdata, '$.pageLocation')
																				AND FID=@FID)
																		)
									print @compTest
									--SELECT * FROM Pages WHERE Title='Mandatory Fields'
									--print @compTest
					--19. CalculationComponentDetails table
											if @compTest=1
												INSERT INTO [CalculationComponentDetails]
												SELECT
													@CALCID as CALCID
													, (SELECT ISNULL(CMPID,0) FROM [Components] WHERE [Name]=JSON_VALUE(@calculationdata, '$.name')
													AND PGID=(SELECT ISNULL(PGID, 0) FROM Pages WHERE Title=JSON_VALUE(@calculationdata, '$.pageLocation') AND FID=@FID)) as CMPID

												--print '[CalculationComponentDetails] Table inserted'
											else
												print ''

--SELECT * FROM [Components] WHERE name='cmSPUB'
--SELECT * FROM [CalculationComponentDetails]
--SELECT * FROM Pages WHERE Title='Total amount of funding retained'
								SET @calculationdata=''
								FETCH NEXT FROM calculationjson INTO @calculationdata
							END
						CLOSE calculationjson
						DEALLOCATE calculationjson

					-- Open a cursor on the sections object for the current form
					DECLARE sectionsjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.sections')) AS JSON
					OPEN sectionsjson
					FETCH NEXT FROM sectionsjson INTO @sectiondata
						WHILE @@FETCH_STATUS = 0
							BEGIN
					--20. Sections table
									INSERT INTO [Sections]
									SELECT
										@FID AS FID
										, JSON_VALUE(@sectiondata, '$.name') as SCName
										, JSON_VALUE(@sectiondata, '$.title') as SCTitle
									print '[Sections] Table inserted'

								SET @sectiondata=''
								FETCH NEXT FROM sectionsjson INTO @sectiondata
							END
						CLOSE sectionsjson
						DEALLOCATE sectionsjson

						
						-- Open a cursor on the outputs object for the current form
						DECLARE outputsjson CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.outputs')) AS JSON
						OPEN outputsjson
						FETCH NEXT FROM outputsjson INTO @outputdata
							WHILE @@FETCH_STATUS = 0
								BEGIN
					--21. Outputs table
									INSERT INTO [Outputs]
									SELECT
										@FID AS FID
										, JSON_VALUE(@outputdata, '$.name') as [Name]
										, JSON_VALUE(@outputdata, '$.title') as [Title]
										, JSON_VALUE(@outputdata, '$.type') as [Type]

									SET @OUID = @@identity
									--SET @OUID = 1
									print '[Outputs] Table inserted'

										
									DECLARE outputdetailsdata CURSOR FOR SELECT [key], [value] FROM OPENJSON(JSON_QUERY(@outputdata, '$.outputConfiguration')) AS JSON
									OPEN outputdetailsdata
									FETCH NEXT FROM outputdetailsdata INTO @outputdetailskey, @outputdetailsvalue
										WHILE @@FETCH_STATUS = 0
											BEGIN
					--22. OutputDetails table
												IF @outputdetailskey='personalisation'
													BEGIN
														INSERT INTO [OutputDetails]
														SELECT
															@OUID AS OUID
															, @outputdetailskey as PropName
															, JSON.[value] as PropValue
															, Null as PropType
														FROM OPENJSON(@outputdetailsvalue) as JSON
														print '[OutputDetails]1 Table inserted'

													END
												ELSE
													BEGIN
														INSERT INTO [OutputDetails]
														SELECT
															@OUID AS OUID
															, @outputdetailskey as PropName
															, CASE WHEN
																	LEN(ISNULL(@outputdetailsvalue,''))<=2
																	THEN '' 
																	ELSE @outputdetailsvalue 
																	END as PropValue
															, Null as PropType
														print '[OutputDetails]2 Table inserted'
													END
												
												SET @outputdetailskey=''
												SET @outputdetailsvalue=''

												FETCH NEXT FROM outputdetailsdata INTO @outputdetailskey, @outputdetailsvalue
											END
									CLOSE outputdetailsdata
									DEALLOCATE outputdetailsdata

									SET @outputdata=''
									FETCH NEXT FROM outputsjson INTO @outputdata
								END
							CLOSE outputsjson
							DEALLOCATE outputsjson

							DECLARE conditionsdata CURSOR FOR SELECT [value] FROM OPENJSON(JSON_QUERY(@formjsondata, '$.conditions')) AS JSON
							OPEN conditionsdata
							FETCH NEXT FROM conditionsdata INTO @conditiondata
								WHILE @@FETCH_STATUS = 0
									BEGIN
					--23. Conditions table
										INSERT INTO [Conditions]
										SELECT
											@FID AS FID
											, JSON_VALUE(@conditiondata, '$.displayName') as DisplayName	
											, JSON_VALUE(@conditiondata, '$.name') as [Name]

										SET @CNID = @@identity
										print '[Conditions] Table inserted'

										--SET @CNID = 1

										-- SubsetNo ID to be reset on each condition loop
										SET @SUBID = 1

										DECLARE subconditionsdata CURSOR FOR SELECT [value] 
																													FROM OPENJSON(JSON_QUERY(@conditiondata, '$.value.conditions')) AS JSON
										OPEN subconditionsdata
										FETCH NEXT FROM subconditionsdata INTO @subconditiondata
											WHILE @@FETCH_STATUS = 0
												BEGIN
					--24. ConditionDetails table					
														DECLARE subvalues CURSOR FOR SELECT [key], [value] FROM OPENJSON(@subconditiondata) AS JSON
														OPEN subvalues
														FETCH NEXT FROM subvalues INTO @subconditionkey, @subconditionvalue
															WHILE @@FETCH_STATUS = 0
															BEGIN
																IF @subconditionkey='field' or  @subconditionkey='value'
																	INSERT INTO [ConditionDetails]
																	SELECT
																		@CNID as CNID
																		, @SUBID as SubsetNo
																		, JSON.[key] as PropName
																		, JSON.[value] as PropValue
																		, @subconditionkey as PropType
																	FROM OPENJSON(@subconditionvalue) as JSON
																ELSE
																	INSERT INTO [ConditionDetails]
																	SELECT
																		@CNID as CNID
																		, @SUBID as SubsetNo
																		, @subconditionkey as PropName
																		, @subconditionvalue as PropValue
																		, @subconditionkey as PropType
																print '[Conditions] Table inserted'
																
																SET @subconditionkey=''
																SET @subconditionvalue=''
																FETCH NEXT FROM subvalues INTO @subconditionkey, @subconditionvalue
															END
														CLOSE subvalues
														DEALLOCATE subvalues
														SET @SUBID = @SUBID+1;
													SET @subconditiondata=''
													FETCH NEXT FROM subconditionsdata INTO @subconditiondata
												END
											CLOSE subconditionsdata
											DEALLOCATE subconditionsdata

										SET @conditiondata=''
										FETCH NEXT FROM conditionsdata INTO @conditiondata
									END
								CLOSE conditionsdata
								DEALLOCATE conditionsdata

				SET @formjsondata=''
				FETCH NEXT FROM formsjson INTO @formjsondata
			END
		CLOSE formsjson
		DEALLOCATE formsjson
		SET ANSI_WARNINGS ON;
	END TRY

	BEGIN CATCH
		SELECT 'Error_Message: ' + ERROR_MESSAGE() + ' Error_Line: ' + ERROR_LINE() AS ErrorMessage
		ROLLBACK TRAN Migration
	END CATCH

COMMIT TRAN Migration


GO