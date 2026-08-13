using AutoMapper;
using DigitalForms.BL.Models;
using DigitalForms.BL.Models.Generated;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DigitalForms.BL.Serialized.Models
{
    public class MapperConfig
    {
        private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;

                //form to forms
                cfg.CreateMap<BL.Models.Form, Forms>()
                //members
                .ForMember(dest => dest.startPage, act => act.MapFrom(src => src.StartPage))
                .ForMember(dest => dest.version, act => act.MapFrom(src => src.Version))
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.FormId))
                .ForMember(dest => dest.key, act => act.MapFrom(src => src.FormId))
                .ForMember(dest => dest.displayName, act => act.MapFrom(src => src.Displayname))
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.lastModified, act => act.MapFrom(src => src.LastUpdatedOn))
                .ForMember(dest => dest.skipSummary, act => act.MapFrom(src => src.SkipSummary))
                .ForMember(dest => dest.signInRequired, act => act.MapFrom(src => src.SignInRequired))
                .ForMember(dest => dest.confirmationMsg, act => act.MapFrom(src => src.ConfirmationMsg))
                .ForMember(dest => dest.declaration, act => act.MapFrom(src => src.Declaration))
                .ForMember(dest => dest.lastDownloaded, act => act.MapFrom(src => src.LastDownloaded))
                .ForMember(dest => dest.userId, act => act.MapFrom(src => src.CreatedByUser.UserId))
                .ForMember(dest => dest.createdBy, act => act.MapFrom(src => src.CreatedByUser.Name))
                .ForMember(dest => dest.formStatus, act => act.MapFrom(src => src.Fs.Status))
                .ForMember(dest => dest.lastUpdatedByName, act => act.MapFrom(src => src.LastUpdatedByUser.Name))
                .ForMember(dest => dest.lastUpdatedById, act => act.MapFrom(src => src.LastUpdatedByUser.UserId))
                .ForMember(dest => dest.customSummaryMessage, act => act.MapFrom(src => src.CustomSummaryMessage))
                ////class objs
                //.ForMember(dest => dest.metadata, act => act.MapFrom(src => src.MetaData))
                //.ForMember(dest => dest.fees, act => act.MapFrom(src => src.Fees))
                //.ForMember(dest => dest.feedback, act => act.MapFrom(src => src.Fs.Forms))
                //.ForMember(dest => dest.phaseBanner, act => act.MapFrom(src => src.Fs.Forms))
                //.ForMember(dest => dest.lists, act => act.MapFrom(src => src.Lists))
                .ForMember(dest => dest.file, act => act.MapFrom(src => (src.File.IsNullOrEmpty() ? "" : src.File)))
                //.ForMember(dest => dest.sections, act => act.MapFrom(src => src.Sections))

                ////NEED TO CHK AGAIN
                //.ForMember(dest => dest.conditions, act => act.MapFrom(src => src.Conditions))
                //.ForMember(dest => dest.outputs, act => act.MapFrom(src => src.Outputs))
                //.ForMember(dest => dest.documents, act => act.MapFrom(src => src.Documents))
                //.ForMember(dest => dest.importedDataSets, act => act.MapFrom(src => src.Documents))
                //.ForMember(dest => dest.designedDataSets, act => act.MapFrom(src => src.DesignDataSets))
                //.ForMember(dest => dest.calculations, act => act.MapFrom(src => src.Calculations))
                //.ForMember(dest => dest.tabs, act => act.MapFrom(src => src.TabDetails))                
                .ReverseMap();

                cfg.CreateMap<Form, FormConfiguration>()
                 .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.CreatedByUser.UserId))
                 .ForMember(dest => dest.CreatedBy, act => act.MapFrom(src => src.CreatedByUser.Name))
                 .ForMember(dest => dest.FormStatus, act => act.MapFrom(src => src.Fs.Status))
                 .ForMember(dest => dest.lastModifiedByName, act => act.MapFrom(src => src.LastUpdatedByUser.Name))
                 .ForMember(dest => dest.lastModifiedById, act => act.MapFrom(src => src.LastUpdatedByUser.UserId))
                 .ForMember(dest => dest.LastModified, act => act.MapFrom(src => src.LastUpdatedOn))
                 .ForMember(dest => dest.Key, act => act.MapFrom(src => src.FormId))
                 .ForMember(dest => dest.childAndDependentsForms, act => act.MapFrom(src => GetChildAndDependentsForms(src)))
                 .ReverseMap();
                //Metadata to Metadata
                cfg.CreateMap<BL.Models.MetaData, Metadata>(MemberList.None)
               .ReverseMap();

                //Fee to Fees
                cfg.CreateMap<IEnumerable<BL.Models.Fee>, Fees>(MemberList.None)
                .ReverseMap();

                cfg.CreateMap<Phasebanner, BL.Models.Form>()
                .ForMember(dest => dest.PhaseBanner, act => act.MapFrom(src => src.phase));

                cfg.CreateMap<string, Phasebanner>(MemberList.None)
                .ForAllMembers(act => act.Ignore());

                cfg.CreateMap<IEnumerable<BL.Models.Form>, Phasebanner>()
                .ForMember(dest => dest.phase, act => act.MapFrom(src => src.FirstOrDefault().PhaseBanner));

                cfg.CreateMap<IEnumerable<BL.Models.Form>, Feedback>()
                .ForMember(dest => dest.feedbackForm, act => act.MapFrom(src => src.FirstOrDefault().FeedbackForm))
                .ForMember(dest => dest.url, act => act.MapFrom(src => src.FirstOrDefault().Url))
                .ReverseMap();

                //List to List
                cfg.CreateMap<BL.Models.List, List>()
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Lstname))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Lsttitle))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Lsttype))
                .ForMember(dest => dest.dataset, act => act.MapFrom(src => src.Lstdataset))
                .ForMember(dest => dest.items, act => act.MapFrom(src => src.ListItems))
                .ReverseMap();

                //Listitem to items
                cfg.CreateMap<BL.Models.ListItem, Item>()
                .ForMember(dest => dest.text, act => act.MapFrom(src => src.LstitemsText))
                .ForMember(dest => dest.value, act => act.MapFrom(src => src.LstitemValue))
                .ForMember(dest => dest.description, act => act.MapFrom(src => src.LstitemDesc))
                .ForMember(dest => dest.condition, act => act.MapFrom(src => src.LstitemCondition))
                .ForMember(dest => dest.links, act => act.MapFrom(src => src.Lstitemlinks))
                .ForMember(dest => dest.Order, act => act.MapFrom(src => src.LSTOrder))
                .ReverseMap();

                //section to section
                cfg.CreateMap<BL.Models.Section, Section>()
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Scname))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Sctitle))
                .ForMember(dest => dest.numberComp, act => act.MapFrom(src => src.Scnumbercomp))
                .ForMember(dest => dest.conditionComp, act => act.MapFrom(src => src.Scconditioncomp))
                .ForMember(dest => dest.repeatableSection, act => act.MapFrom(src => src.repeatableSection))
                .ReverseMap();

                //conditions to conditions
                cfg.CreateMap<BL.Models.Condition, Condition>()
                .ForMember(dest => dest.displayName, act => act.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ReverseMap();

                //value to conditiondetails
                cfg.CreateMap<Value, IEnumerable<BL.Models.ConditionDetail>>(MemberList.None)
                .ForAllMembers(act => act.Ignore());

                //conditiondetails  to value
                cfg.CreateMap<IEnumerable<BL.Models.ConditionDetail>, Value>()
               .ForMember(dest => dest.name, act => act.MapFrom(src => src.FirstOrDefault().Cn.DisplayName))
                 .ForMember(dest => dest.conditions, act => act.MapFrom(src => src.FirstOrDefault().Cn.ConditionDetails));

                //conditiondetails  to Condition1
                cfg.CreateMap<BL.Models.ConditionDetail, Condition1>()
                .ForMember(dest => dest._operator, act => act.MapFrom(src => (src.Cn.ConditionDetails.Where(w => w.PropType == "operator" && w.PropName == "operator").FirstOrDefault().PropValue)))
                .ForMember(dest => dest.conditionType, act => act.MapFrom(src => (src.Cn.ConditionDetails.Where(w => w.PropType == "conditionType" && w.PropName == "conditionType").FirstOrDefault().PropValue)))
                .ForMember(dest => dest.datasetId, act => act.MapFrom(src => (src.Cn.ConditionDetails.Where(w => w.PropType == "datasetId" && w.PropName == "datasetId").FirstOrDefault().PropValue)))
                .ForMember(dest => dest.field, act => act.MapFrom(src => new Field
                {
                    type = src.Cn.ConditionDetails.Where(w => w.PropType == "field" && w.PropName == "type").FirstOrDefault().PropValue,
                    display = src.Cn.ConditionDetails.Where(w => w.PropType == "field" && w.PropName == "display").FirstOrDefault().PropValue,
                    name = src.Cn.ConditionDetails.Where(w => w.PropType == "field" && w.PropName == "name").FirstOrDefault().PropValue
                }))
                .ForMember(dest => dest.value, act => act.MapFrom(src => new Value1
                {
                    type = src.Cn.ConditionDetails.Where(w => w.PropType == "value" && w.PropName == "type").FirstOrDefault().PropValue,
                    display = src.Cn.ConditionDetails.Where(w => w.PropType == "value" && w.PropName == "display").FirstOrDefault().PropValue,
                    value = src.Cn.ConditionDetails.Where(w => w.PropType == "value" && w.PropName == "value").FirstOrDefault().PropValue
                }));

                //Output to Output
                cfg.CreateMap<BL.Models.Output, Output>()
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ReverseMap();

                // outputConfiguration to OutputDetails
                cfg.CreateMap<Outputconfiguration, IEnumerable<BL.Models.OutputDetail>>(MemberList.None)
                .ForAllMembers(act => act.Ignore());

                //OutputDetails to outputConfiguration
                cfg.CreateMap<IEnumerable<BL.Models.OutputDetail>, Outputconfiguration>()
                .ForMember(dest => dest.emailAddress, act => act.MapFrom(src => (src.FirstOrDefault().PropName == "emailAddress") ? src.FirstOrDefault().PropValue : ""))
                .ForMember(dest => dest.personalisation, act => act.MapFrom(src => new string[] { (src.FirstOrDefault().PropName == "personalisation") ? src.FirstOrDefault().PropValue : "" }))
                .ForMember(dest => dest.templateId, act => act.MapFrom(src => (src.FirstOrDefault().PropName == "templateId") ? src.FirstOrDefault().PropValue : ""))
                .ForMember(dest => dest.apiKey, act => act.MapFrom(src => (src.FirstOrDefault().PropName == "apiKey") ? src.FirstOrDefault().PropValue : ""))
                .ForMember(dest => dest.emailField, act => act.MapFrom(src => (src.FirstOrDefault().PropName == "emailField") ? src.FirstOrDefault().PropValue : ""))
                .ForMember(dest => dest.addReferencesToPersonalisation, act => act.MapFrom(src => Convert.ToBoolean((src.FirstOrDefault().PropName == "addReferencesToPersonalisation") ? src.FirstOrDefault().PropValue : false)));


                //Document to Importeddataset
                cfg.CreateMap<BL.Models.Document, Importeddataset>()
                .ForMember(dest => dest.fileId, act => act.MapFrom(src => src.FileId))
                .ForMember(dest => dest.fileTitle, act => act.MapFrom(src => src.FileTitle))
                .ForMember(dest => dest.fileName, act => act.MapFrom(src => src.FileName))
                .ForMember(dest => dest.uploadedDate, act => act.MapFrom(src => src.UploadedOn))
                .ReverseMap();

                //Document to Document
                cfg.CreateMap<BL.Models.Document, Document>()
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.FileId))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.FileTitle))
                .ForMember(dest => dest.fileName, act => act.MapFrom(src => src.FileName))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.FileType))
                .ForMember(dest => dest.path, act => act.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.uploadedDate, act => act.MapFrom(src => src.UploadedOn))
                .ReverseMap();

                //DesignDataSet to Designeddataset
                cfg.CreateMap<BL.Models.DesignDataSet, Designeddataset>()
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.DdsetId))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.csvUsed, act => act.MapFrom(src => src.CsvUsed))
                .ForMember(dest => dest.keyIdentifier, act => act.MapFrom(src => src.KeyIdentifier))
                .ForMember(dest => dest.data, act => act.MapFrom(src => src.DesignDataSetDetails))
                .ForMember(dest => dest.uploadedDate, act => act.MapFrom(src => src.UploadedOn))
                .ForMember(dest => dest.data, act => act.MapFrom(src => src.DesignDataSetDetails))
                .ReverseMap();

                ////DesignDataSetDetail to Datum
                cfg.CreateMap<DesignDataSetDetail, Datum>()
                .ForMember(dest => dest.index, act => act.MapFrom(src => src.Index))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ForMember(dest => dest.value, act => act.MapFrom(src => src.Value))
                .ForMember(dest => dest.bold, act => act.MapFrom(src => src.Bold))
                .ForMember(dest => dest.calc, act => act.MapFrom(src => src.MarkForCalculation))
                .ForMember(dest => dest.numeric, act => act.MapFrom(src => src.MarkAsNumber))
                .ForMember(dest => dest._checked, act => act.MapFrom(src => src._checked))
                .ForMember(dest => dest.format, act => act.MapFrom(src => src.Format))
                .ReverseMap();


                // Calculation to Calculation
                cfg.CreateMap<BL.Models.Calculation, Calculation>()
                .ForMember(dest => dest.displayName, act => act.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.hint, act => act.MapFrom(src => src.Hint))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ForMember(dest => dest.pageLocation, act => act.MapFrom(src => src.PageLocation))
                .ForMember(dest => dest.expression, act => act.MapFrom(src => src.Expressions))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.hideResult, act => act.MapFrom(src => src.HideResult))
                .ForMember(dest => dest.repeatable, act => act.MapFrom(src => src.Repeatable))
                .ForMember(dest => dest.components, act => act.MapFrom(src => src.CalculationComponentDetails.Select(s => s.Cmp)))
                .ReverseMap();

                cfg.CreateMap<BL.Models.ParentChild, ParentChild>()
                .ForMember(dest => dest.isMainParent, act => act.MapFrom(src => src.IsMainParent))
                .ForMember(dest => dest.parentChildConfig, opt => opt.MapFrom(src => src.ParentChildConfig))
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.Name))
                .ReverseMap();

                cfg.CreateMap<BL.Models.ParentChildConfig, ParentChildConfig>()
                .ForMember(dest => dest.description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.childHeading, act => act.MapFrom(src => src.ChildHeading))
                // .ForMember(dest => dest.childConfigs, act => act.MapFrom(src => src.ChildConfigs))
                .ReverseMap();

                cfg.CreateMap<ChildConfiguration, BL.Models.ChildConfig>()
                .ForMember(dest => dest.ChildId, act => act.MapFrom(src => src.childId))
                .ForMember(dest => dest.DateComponent, act => act.MapFrom(src => src.dateComponent))
                .ForMember(dest => dest.HelpText, act => act.MapFrom(src => src.helpText))
                .ForMember(dest => dest.ParentId, act => act.MapFrom(src => src.parentId))
                .ForMember(dest => dest.ConditionName, act => act.MapFrom(src => src.conditionName))
                .ForMember(dest => dest.Condition, act => act.MapFrom(src => src.condition))
                .ForMember(dest => dest.IsMainChild, act => act.MapFrom(src => src.isMainChild))
                .ReverseMap();

                cfg.CreateMap<Dependentform, BL.Models.DependentForm>()
                .ForMember(dest => dest.FormId, act => act.MapFrom(src => src.id))
                .ForMember(dest => dest.DependentStatus, act => act.MapFrom(src => src.status))
                 .ForMember(dest => dest.MainParentId, act => act.Ignore())
                .ReverseMap();

                //Pages to pages
                cfg.CreateMap<BL.Models.Page, Page>()
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.path, act => act.MapFrom(src => src.Path))
                .ForMember(dest => dest.section, act => act.MapFrom(src => src.Section))
                .ForMember(dest => dest.controller, act => act.MapFrom(src => src.Controller))
                .ForMember(dest => dest.next, act => act.MapFrom(src => src.PageChildSettings))
                .ReverseMap();

                //PageChildSetting to Next
                cfg.CreateMap<BL.Models.PageChildSetting, Next>()
                .ForMember(dest => dest.path, act => act.MapFrom(src => src.Path))
                .ForMember(dest => dest.condition, act => act.MapFrom(src => src.Condition))
                .ReverseMap();

                //Component to Component
                cfg.CreateMap<BL.Models.Component, Component>()
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ForMember(dest => dest.isEditingTabs, act => act.MapFrom(src => src.IsEditingTabs))
                .ForMember(dest => dest.nameHasError, act => act.MapFrom(src => src.NameHasError))
                .ForMember(dest => dest.hint, act => act.MapFrom(src => src.Hint))
                .ForMember(dest => dest.componentEdited, act => act.MapFrom(src => src.ComponentEdited))
                .ForMember(dest => dest._checked, act => act.MapFrom(src => src.Checked))
                .ForMember(dest => dest.content, act => act.MapFrom(src => src.Content))
                .ReverseMap();

                //ComponentDatasetSchemaDetail to Column
                cfg.CreateMap<BL.Models.ComponentDatasetSchemaDetail, Column>(MemberList.None)
                .ForMember(dest => dest.columnId, act => act.MapFrom(src => src.ColumnId))
                .ForMember(dest => dest.columnType, act => act.MapFrom(src => src.ColumnType))
                .ForMember(dest => dest.CDSDID, act => act.MapFrom(src => src.Cdsdid))
                .ForMember(dest => dest.selectedColumnHeaderType, act => act.MapFrom(src => src.SelectedColumnHeaderType))
                .ForMember(dest => dest.selectedColumnHeaderValue, act => act.MapFrom(src => src.SelectedColumnHeaderValue))
                .ForMember(dest => dest.isEdited, act => act.MapFrom(src => src.IsEdited))
                .ForMember(dest => dest.columnSchema, act => act.Ignore())
                .ForMember(dest => dest.ColumnOrder, act => act.MapFrom(src => src.ColumnOrder));

                //Column to ComponentDatasetSchemaDetail to 


                cfg.CreateMap<Column, BL.Models.ComponentDatasetSchemaDetail>(MemberList.None)
               .ForMember(dest => dest.ColumnId, act => act.MapFrom(src => src.columnId))
               .ForMember(dest => dest.ColumnType, act => act.MapFrom(src => src.columnType))
               .ForMember(dest => dest.SelectedColumnHeaderType, act => act.MapFrom(src => src.selectedColumnHeaderType))
               .ForMember(dest => dest.SelectedColumnHeaderValue, act => act.MapFrom(src => src.selectedColumnHeaderValue))
               .ForMember(dest => dest.IsEdited, act => act.MapFrom(src => src.isEdited))
               .ForMember(dest => dest.ColumnOrder, act => act.MapFrom(src => src.ColumnOrder))
               .ForMember(dest => dest.ColumnSchema, act => act.MapFrom(src => src.columnSchema != null ? true : false));



                //ComponentDatasetSchemaPropertyDetail to Columnschema
                cfg.CreateMap<BL.Models.ComponentDatasetSchemaPropertyDetail, Columnschema>()
                    .ForMember(dest => dest.minNumber, act => act.MapFrom(src => (src.PropName == "minNumber") ? src.PropValue : null))
                    .ForMember(dest => dest.maxNumber, act => act.MapFrom(src => (src.PropName == "maxNumber") ? src.PropValue : null))
                    .ForMember(dest => dest.precisionNumber, act => act.MapFrom(src => (src.PropName == "precisionNumber") ? src.PropValue : null))
                    .ForMember(dest => dest.maxLength, act => act.MapFrom(src => (src.PropName == "maxLength") ? src.PropValue : null))
                    .ForMember(dest => dest.maxNumber, act => act.MapFrom(src => (src.PropName == "maxDaysInPast") ? src.PropValue : null))
                    .ForMember(dest => dest.precisionNumber, act => act.MapFrom(src => (src.PropName == "maxDaysInFuture") ? src.PropValue : null))
                    .ForMember(dest => dest.addressRequired, act => act.MapFrom(src => (src.PropName == "addressRequired") ? src.PropValue : null))
                    .ReverseMap();


                // tabchilddetail to tabdata
                cfg.CreateMap<BL.Models.ComponentAdditionalSetting, Tabdata>()
                .ForMember(dest => dest.tabLabel, act => act.MapFrom(src => src.TabChildDetails.Select(s => s.TabLabel)))
                .ForMember(dest => dest.tabHeader, act => act.MapFrom(src => src.TabChildDetails.Select(s => s.TabHeader)))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.TabChildDetails.Select(s => s.Type)))
                .ForMember(dest => dest.value, act => act.MapFrom(src => src.TabChildDetails.Select(s => s.Value)))
                .ReverseMap();



                // TabDetail to Tab
                cfg.CreateMap<BL.Models.TabDetail, Tab>()
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.TabName))
                .ForMember(dest => dest.tabData, act => act.MapFrom(src => src.TabChildDetails))
                .ReverseMap();


                // TabChildDetail to Tabdata
                cfg.CreateMap<BL.Models.TabChildDetail, Tabdata>()
                .ForMember(dest => dest.tabLabel, act => act.MapFrom(src => src.TabLabel))
                .ForMember(dest => dest.tabHeader, act => act.MapFrom(src => src.TabHeader))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ForMember(dest => dest.value, act => act.MapFrom(src => src.Value))
                .ReverseMap();

                cfg.CreateMap<BL.Models.SubmissionFormLog, SubmissionForm>()
               .ForMember(dest => dest.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
               .ForMember(dest => dest.EmailHadAttachment, act => act.MapFrom(src => src.EmailHadAttachment))
               .ForMember(dest => dest.EmailSent, act => act.MapFrom(src => src.EmailSent))
               .ForMember(dest => dest.EmailSentOn, act => act.MapFrom(src => src.EmailSentOn))
               .ForMember(dest => dest.id, act => act.MapFrom(src => src.id))
               .ForMember(dest => dest.TemplateId, act => act.MapFrom(src => src.TemplateId))
               .ReverseMap();

            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => _mapper.Value;

        private static readonly Lazy<IMapper> _submissionMapper = new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BL.Models.SubmissionFormLog, SubmissionForm>()
                   .ForMember(dest => dest.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
                   .ForMember(dest => dest.EmailHadAttachment, act => act.MapFrom(src => src.EmailHadAttachment))
                   .ForMember(dest => dest.EmailSent, act => act.MapFrom(src => src.EmailSent))
                   .ForMember(dest => dest.EmailSentOn, act => act.MapFrom(src => src.EmailSentOn))
                   .ForMember(dest => dest.id, act => act.MapFrom(src => src.id))
                   .ForMember(dest => dest.TemplateId, act => act.MapFrom(src => src.TemplateId));

                cfg.CreateMap<SubmissionForm, BL.Models.SubmissionFormLog>()
                   .ForMember(dest => dest.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
                   .ForMember(dest => dest.EmailHadAttachment, act => act.MapFrom(src => src.EmailHadAttachment))
                   .ForMember(dest => dest.EmailSent, act => act.MapFrom(src => src.EmailSent))
                   .ForMember(dest => dest.EmailSentOn, act => act.MapFrom(src => src.EmailSentOn))
                   .ForMember(dest => dest.id, act => act.MapFrom(src => src.id))
                   .ForMember(dest => dest.TemplateId, act => act.MapFrom(src => src.TemplateId));
            });
            return config.CreateMapper();
        });

        private static readonly Lazy<IMapper> _dcdatamapper = new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BL.Models.DCData, DocData>()
                   .ForMember(dest => dest.DCDID, act => act.MapFrom(src => src.DCDID))
                   .ForMember(dest => dest.fileName, act => act.MapFrom(src => src.fileName))
                   .ForMember(dest => dest.filePath, act => act.MapFrom(src => src.filePath))
                   .ForMember(dest => dest.fileStatus, act => act.MapFrom(src => src.fileStatus))
                   .ForMember(dest => dest.sourceSystem, act => act.MapFrom(src => src.sourceSystem))
                   .ForMember(dest => dest.fileId, act => act.MapFrom(src => src.fileId))
                   .ForMember(dest => dest.scanStatus, act => act.MapFrom(src => src.scanStatus)).ReverseMap();
            });
            return config.CreateMapper();
        });

        public static IMapper SubmissionMapper => _submissionMapper.Value;

        public static IMapper DCDataMapper => _dcdatamapper.Value;

        private static string[] GetChildAndDependentsForms(Form src)
        {
            string[] childIds = src.ParentChild?.ParentChildConfig?.ChildConfigs.Select(s => s.ChildId).ToArray();
            string[] dependentForms = src.ParentChild?.ParentChildConfig?.ChildConfigs?.SelectMany(s => s.DependentForms?.Select(f => f.FormId)).ToArray();
            return childIds.Concat(dependentForms).Distinct().ToArray();
        }

        private static readonly Lazy<IMapper> _responseMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<BL.Models.Response, Responses>()
                .ForMember(dest => dest.formId, act => act.MapFrom(src => src.Fid))
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.FormName))
                .ForMember(dest => dest.questions, act => act.MapFrom(src => src.ResponseQuestions))
                //.ForMember(dest=> dest.user, act=>act.Ignore())
                .ForMember(dest => dest.metadata, act => act.MapFrom(src => src.Mtdt));
                // .ForMember(dest => dest.user, act => act.MapFrom(src => src.User))
                //.ForMember( dest => dest.id.Length , act => act.MapFrom(src=>src.UserId))

                cfg.CreateMap<Responses, BL.Models.Response>()
                .ForMember(dest => dest.Fid, act => act.MapFrom(src => src.formId))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.id))
                .ForMember(dest => dest.FormName, act => act.MapFrom(src => src.name))
                .ForMember(dest => dest.ResponseQuestions, act => act.MapFrom(src => src.questions))
                .ForMember(dest => dest.UserId, act => act.Ignore());

                cfg.CreateMap<BL.Models.ResponseQuestion, Question>()
                .ForMember(dest => dest.question, act => act.MapFrom(src => src.Question))
                .ForMember(dest => dest.fields, act => act.MapFrom(src => src.ResponseQuestionData))
                .ReverseMap();

                cfg.CreateMap<BL.Models.ResponseQuestionDatum, ComponentsData>()
                .ForMember(dest => dest.key, act => act.MapFrom(src => src.Key))
                .ForMember(dest => dest.title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.type, act => act.MapFrom(src => src.Type))
                .ForMember(dest => dest.answer, act => act.MapFrom(src => src.Answer))
                .ReverseMap();

                cfg.CreateMap<Metadatum, BL.Models.MetaData>()
                .ForMember(dest => dest.PropName, act => act.MapFrom(src => src.paymentSkipped))
                .ForMember(dest => dest.PropValue, act => act.MapFrom(src => src.paymentSkipped))
                .ForMember(dest => dest.PropType, act => act.MapFrom(src => src.paymentSkipped))
                .ReverseMap();



                cfg.CreateMap<BL.Models.UserDetail, Users>()
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.email, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.id, act => act.MapFrom(src => src.UserId))
                // .ForMember(dest => dest.organization, act => act.MapFrom(src=>src.OrganisationDetails))
                .ForMember(dest => dest.userid, act => act.MapFrom(src => src.Uid))
                .ReverseMap();

                //cfg.CreateMap<List<BL.Models.OrganisationDetail>, Organization>()
                //.ForMember(dest => dest.name, act => act.MapFrom(src => src.FirstOrDefault().Name))
                //.ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.FirstOrDefault().Ukprn))
                //.ForMember(dest => dest.urn, act => act.MapFrom(src => src.FirstOrDefault().Urn))
                //.ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.FirstOrDefault().AdminCode));

                //cfg.CreateMap<Organization, List<BL.Models.OrganisationDetail>>(MemberList.None)
                //.ForAllMembers(dest => dest.Ignore());

                //.ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.FirstOrDefault().Ukprn))
                //.ForMember(dest => dest.urn, act => act.MapFrom(src => src.FirstOrDefault().Urn))
                //.ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.FirstOrDefault().AdminCode))



                cfg.CreateMap<BL.Models.OrganisationDetail, Organization>()
                .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.Ukprn))
                .ForMember(dest => dest.urn, act => act.MapFrom(src => src.Urn))
                .ForMember(dest => dest.ukprn, act => act.MapFrom(src => src.AdminCode))
                .ReverseMap();

                cfg.CreateMap<BL.Models.ResponseQuestion, Question>()
                .ForMember(dest => dest.question, act => act.MapFrom(src => src.Question))
                .ForMember(dest => dest.fields, act => act.MapFrom(src => src.ResponseQuestionData))
                .ReverseMap();


                cfg.CreateMap<CustomResponse, Response>()
                  .ForMember(dest => dest.Rspid, act => act.MapFrom(src => src.Rspid))
                .ForMember(dest => dest.Fid, act => act.MapFrom(src => src.Fid))
                .ForMember(dest => dest.UpdatedOn, act => act.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.UpdatedBy, act => act.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.ResponseStatus, act => act.MapFrom(src => src.ResponseStatus))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.FormName, act => act.MapFrom(src => src.FormName))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MtdtId, act => act.MapFrom(src => src.MtdtId))
                .ForMember(dest => dest.isUAT, act => act.MapFrom(src => src.isUAT))
                .ReverseMap();
            });
            return config.CreateMapper();
        });
        public static IMapper ResponseMapper => _responseMapper.Value;

        private static readonly Lazy<IMapper> _draftResponseMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<BL.Serialized.Models.Output, BL.Models.Output>().ReverseMap();
                  cfg.CreateMap<BL.Serialized.Models.Responses, BL.Models.Response>().ReverseMap();

                  // Mapping from BL.Models.DraftResponse to Serialized.Models.DraftResponse
                  cfg.CreateMap<BL.Models.DraftResponse, BL.Serialized.Models.DraftResponse>()
                     .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     // .ForMember(dest => dest.OrgUKPRN, act => act.Ignore())
                     //.ForMember(dest => dest.DsiSignInEmail, act => act.Ignore())
                     .ForMember(dest => dest.Progress, act => act.Ignore())
                     .ForMember(dest => dest.FormData, act => act.Ignore())
                     .ForMember(dest => dest.DataImportStatus, act => act.Ignore())
                     .ForMember(dest => dest.PreviousPage, act => act.Ignore())
                     .ForMember(dest => dest.SelectField, act => act.Ignore())
                     .ForMember(dest => dest.Reference, act => act.MapFrom(src => src.Reference))
                     .ForMember(dest => dest.ReferenceIsStored, act => act.MapFrom(src => src.ReferenceIsStored))
                     .ForMember(dest => dest.UserCompletedSummary, act => act.MapFrom(src => src.UserCompletedSummary))
                     .ForMember(dest => dest.FormDataId, act => act.MapFrom(src => src.FormDataId))
                     .ForMember(dest => dest.Formid, act => act.MapFrom(src => src.Formid))
                     .ForMember(dest => dest.outputs, act => act.Ignore());

                  cfg.CreateMap<BL.Serialized.Models.DraftResponse, BL.Models.DraftResponse>()
                     .ForMember(dest => dest.Outputs, opt => opt.MapFrom(src => src.outputs.FirstOrDefault()));


                  cfg.CreateMap<BL.Serialized.Models.Users, BL.Models.UserDetail>()
                      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.id))
                      .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.userid))
                      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                      .ForMember(dest => dest.Status, opt => opt.Ignore())
                      .ForMember(dest => dest.UserOrganisationDetails, opt => opt.Ignore())
                      .ForMember(dest => dest.Responses, opt => opt.Ignore())
                      .ForMember(dest => dest.FormLastUpdatedByUsers, opt => opt.Ignore())
                      .ForMember(dest => dest.FormCreatedByUsers, opt => opt.Ignore())
                      .ReverseMap();

                  cfg.CreateMap<BL.Serialized.Models.Organization, BL.Models.OrganisationDetail>()
                      .ForMember(dest => dest.Name, act => act.MapFrom(src => src.name))
                      .ForMember(dest => dest.Ukprn, act => act.MapFrom(src => src.ukprn))
                      .ForMember(dest => dest.Urn, act => act.MapFrom(src => src.urn))
                      .ForMember(dest => dest.AdminCode, act => act.MapFrom(src => src.ukprn))
                      .ReverseMap();

              });
            return config.CreateMapper();
        });

        public static IMapper DraftResponseMapper => _draftResponseMapper.Value;

        //private static Output[] MapOutputs(BL.Models.Output output)
        //{
        //    // Assuming Output is a single object, map it to an array of DigitalForms.BL.Serialized.Models.Output
        //    var mappedOutput = new DigitalForms.BL.Serialized.Models.Output();
        //    // Map properties from BL.Models.Output to DigitalForms.BL.Serialized.Models.Output
        //    // Example:
        //    // mappedOutput.Property = output.Property;
        //    return new Output[] { mappedOutput };
        //}
        //public static Mapper InitializeResponseAPIMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<CustomResponse, Response>()
        //         .ForMember(dest => dest.Rspid, act => act.MapFrom(src => src.Rspid))
        //       .ForMember(dest => dest.Fid, act => act.MapFrom(src => src.Fid))
        //       .ForMember(dest => dest.UpdatedOn, act => act.MapFrom(src => src.UpdatedOn))
        //       .ForMember(dest => dest.UpdatedBy, act => act.MapFrom(src => src.UpdatedBy))
        //       .ForMember(dest => dest.ResponseStatus, act => act.MapFrom(src => src.ResponseStatus))
        //       .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
        //       .ForMember(dest => dest.FormName, act => act.MapFrom(src => src.FormName))
        //       .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
        //       .ForMember(dest => dest.MtdtId, act => act.MapFrom(src => src.MtdtId))
        //       .ForMember(dest => dest.isUAT, act => act.MapFrom(src => src.isUAT))
        //       .ReverseMap();

        //        cfg.CreateMap<CustomUserDetail, UserDetail>()
        //        .ForMember(dest => dest.Uid, act => act.MapFrom(src => src.Uid))
        //        .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
        //        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
        //        .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
        //        .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
        //        //.ForMember(dest => dest.OrganisationDetails, opt => opt.Ignore())
        //        .ForMember(dest => dest.Responses, opt => opt.Ignore())
        //        .ForMember(dest => dest.Responses, opt => opt.Ignore())
        //        .ForMember(dest => dest.FormLastUpdatedByUsers, opt => opt.Ignore())
        //        .ForMember(dest => dest.FormCreatedByUsers, opt => opt.Ignore())
        //        .ReverseMap();

        //        cfg.CreateMap<CustomOrganisationDetail, OrganisationDetail>()
        //          .ForMember(dest => dest.Orgid, act => act.MapFrom(src => src.Orgid))
        //          //.ForMember(dest => dest.Uid, act => act.MapFrom(src => src.Uid))
        //          .ForMember(dest => dest.Ukprn, act => act.MapFrom(src => src.Ukprn))
        //          .ForMember(dest => dest.Urn, act => act.MapFrom(src => src.Urn))
        //          .ForMember(dest => dest.AdminCode, act => act.MapFrom(src => src.AdminCode))
        //          .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
        //          //.ForMember(dest => dest.UidNavigation, opt => opt.Ignore()) 
        //          .ReverseMap();

        //        cfg.CreateMap<CustomUserDetail, UserDetail>()
        //        .ForMember(dest => dest.Uid, act => act.MapFrom(src => src.Uid))
        //        .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
        //        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
        //        .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
        //        .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
        //        //.ForMember(dest => dest.OrganisationDetails, act => act.MapFrom(src => src.OrganisationDetails))
        //        .ForMember(dest => dest.Responses, opt => opt.Ignore())
        //        .ForMember(dest => dest.FormLastUpdatedByUsers, opt => opt.Ignore())
        //        .ForMember(dest => dest.FormCreatedByUsers, opt => opt.Ignore())
        //        .ReverseMap();

        //        cfg.CreateMap<CustomResponseQuestion, ResponseQuestion>()
        //        .ForMember(dest => dest.RspQstId, act => act.MapFrom(src => src.RspQstId))
        //        .ForMember(dest => dest.RspId, act => act.MapFrom(src => src.RspId))
        //        .ForMember(dest => dest.Question, act => act.MapFrom(src => src.Question))
        //        .ReverseMap();

        //        cfg.CreateMap<CustomResponseQuestionDatum, ResponseQuestionDatum>()
        //        .ForMember(dest => dest.RspQstDataId, act => act.MapFrom(src => src.RspQstDataId))
        //        .ForMember(dest => dest.RspQstId, act => act.MapFrom(src => src.RspQstId))
        //        .ForMember(dest => dest.Key, act => act.MapFrom(src => src.Key))
        //        .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
        //        .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
        //        .ForMember(dest => dest.Answer, act => act.MapFrom(src => src.Answer))
        //        .ReverseMap();
        //    });
        //    var mapper = new Mapper(config);
        //    return mapper;
        //}


        public static Mapper InitializeProviderMappingMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderMappings, ProviderMapping>()
                .ForMember(dest => dest.Fid, act => act.MapFrom(src => src.id))
                //.ForMember(dest=>dest.AdminCodeProvidersData,act=>act.MapFrom(src => src.providers))
                //.ForMember(dest=>dest.UKPRNProvidersData,act=>act.MapFrom(src => src.providers))
                //.ForMember(dest=>dest.URNProvidersData,act=>act.MapFrom(src => src.providers))               
                .ReverseMap();

                cfg.CreateMap<ProvidersData, AdminCodeProvidersData>()
                .ForMember(dest => dest.AdminCode, act => act.MapFrom(src => src.AdminCode.ToArray().Select(s => s.ToString())));

                cfg.CreateMap<ProvidersData, UKPRNProvidersData>()
                .ForMember(dest => dest.Ukprn, act => act.MapFrom(src => src.UKPRN.ToArray().Select(s => Convert.ToInt32(s.ToString()))));

                cfg.CreateMap<ProvidersData, URNProvidersData>()
                .ForMember(dest => dest.Urn, act => act.MapFrom(src => src.URN.ToArray().Select(s => Convert.ToInt32(s.ToString()))));

            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public static Mapper InitialiseMapperforUpdateSametype()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BL.Models.ConditionDetail, BL.Models.ConditionDetail>()
                    .ForMember(dest => dest.Cndtlid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cnid, opt => opt.Ignore())
                .ForMember(dest => dest.Cn, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ParentChild, BL.Models.ParentChild>()
                    .ForMember(dest => dest.Pcid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore())
                .ForMember(dest => dest.FidNavigation, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Condition, BL.Models.Condition>()
                   .ForMember(dest => dest.Cnid, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore())
                   .ForMember(dest => dest.ConditionDetails, opt => opt.Ignore())
                   .ForMember(dest => dest.FidNavigation, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Calculation, BL.Models.Calculation>()
                   .ForMember(dest => dest.Calcid, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore())
                   .ForMember(dest => dest.CalculationComponentDetails, opt => opt.Ignore())
                   .ForMember(dest => dest.FidNavigation, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.CalculationComponentDetail, BL.Models.CalculationComponentDetail>()
                .ForMember(dest => dest.Calcoid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Document, BL.Models.Document>()
                    .ForMember(dest => dest.Docid, opt => opt.Ignore())
                    .ForMember(dest => dest.Components, opt => opt.Ignore())
                    .ForMember(dest => dest.DesignDataSets, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Section, BL.Models.Section>()
                   .ForMember(dest => dest.Scid, opt => opt.Ignore())
                   .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.DesignDataSet, BL.Models.DesignDataSet>()
                    .ForMember(dest => dest.Ddsid, opt => opt.Ignore())
                    //.ForMember(dest => dest.Docid, opt => opt.Ignore())
                    .ForMember(dest => dest.Components, opt => opt.Ignore())
                    .ForMember(dest => dest.DesignDataSetDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Doc, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.DesignDataSetDetail, BL.Models.DesignDataSetDetail>()
                    .ForMember(dest => dest.Ddsid, opt => opt.Ignore())
                    .ForMember(dest => dest.Dds, opt => opt.Ignore())
                    .ForMember(dest => dest.Ddsdtlid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.List, BL.Models.List>()
                    .ForMember(dest => dest.Lstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Components, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.ListItems, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ListItem, BL.Models.ListItem>()
                    .ForMember(dest => dest.Lstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Lst, opt => opt.Ignore())
                    .ForMember(dest => dest.Lstitemid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Output, BL.Models.Output>()
                    .ForMember(dest => dest.Ouid, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.OutputDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.OutputDetail, BL.Models.OutputDetail>()
                    .ForMember(dest => dest.Ouid, opt => opt.Ignore())
                    .ForMember(dest => dest.Ou, opt => opt.Ignore())
                    .ForMember(dest => dest.Oudtlid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.TabDetail, BL.Models.TabDetail>()
                    .ForMember(dest => dest.Tabid, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.TabChildDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.TabChildDetail, BL.Models.TabChildDetail>()
                    .ForMember(dest => dest.Tabcid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cadstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cadst, opt => opt.Ignore())
                    .ForMember(dest => dest.Tab, opt => opt.Ignore())
                    .ForMember(dest => dest.Tabid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Page, BL.Models.Page>()
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore())
                    .ForMember(dest => dest.Components, opt => opt.Ignore())
                    .ForMember(dest => dest.PageChildSettings, opt => opt.Ignore())
                    .ForMember(dest => dest.FidNavigation, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.PageChildSetting, BL.Models.PageChildSetting>()
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore())
                    .ForMember(dest => dest.Pgchstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Pg, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Component, BL.Models.Component>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    //.ForMember(dest => dest.Ddsid, opt => opt.Ignore())
                    //.ForMember(dest => dest.Lstid, opt => opt.Ignore())
                    //.ForMember(dest => dest.Docid, opt => opt.Ignore())
                    .ForMember(dest => dest.CalculationComponentDetails, opt => opt.Ignore())
                     .ForMember(dest => dest.ComponentAdditionalSettings, opt => opt.Ignore())
                    .ForMember(dest => dest.ComponentDatasetSchemaDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Dds, opt => opt.Ignore())
                    .ForMember(dest => dest.Lst, opt => opt.Ignore())
                    .ForMember(dest => dest.Doc, opt => opt.Ignore())
                    .ForMember(dest => dest.Pg, opt => opt.Ignore())
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentAdditionalSetting, BL.Models.ComponentAdditionalSetting>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cmp, opt => opt.Ignore())
                    .ForMember(dest => dest.TabChildDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Cadstid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentDatasetSchemaDetail, BL.Models.ComponentDatasetSchemaDetail>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    .ForMember(dest => dest.ColumnSchema, opt => opt.Ignore())
                    .ForMember(dest => dest.ComponentDatasetSchemaPropertyDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.DatasetDataDetails, opt => opt.Ignore())
                    .ForMember(dest => dest.Cmp, opt => opt.Ignore())
                    .ForMember(dest => dest.Cdsdid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentDatasetSchemaPropertyDetail, BL.Models.ComponentDatasetSchemaPropertyDetail>()
                    .ForMember(dest => dest.Cdspdid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cdsd, opt => opt.Ignore())
                    .ForMember(dest => dest.Cdsdid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Form, BL.Models.Form>()
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

            });
            var mapper = new Mapper(config);
            return mapper;
        }


        public static Mapper InitialiseMapperforAddingSametype()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BL.Models.ConditionDetail, BL.Models.ConditionDetail>()
                    .ForMember(dest => dest.Cndtlid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cnid, opt => opt.Ignore());


                cfg.CreateMap<BL.Models.Condition, BL.Models.Condition>()
                   .ForMember(dest => dest.Cnid, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Calculation, BL.Models.Calculation>()
                   .ForMember(dest => dest.Calcid, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.CalculationComponentDetail, BL.Models.CalculationComponentDetail>()
                .ForMember(dest => dest.Calcoid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Document, BL.Models.Document>()
                    .ForMember(dest => dest.Docid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Section, BL.Models.Section>()
                   .ForMember(dest => dest.Scid, opt => opt.Ignore())
                   .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.DesignDataSet, BL.Models.DesignDataSet>()
                    .ForMember(dest => dest.Ddsid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.DesignDataSetDetail, BL.Models.DesignDataSetDetail>()
                    .ForMember(dest => dest.Ddsid, opt => opt.Ignore())
                    .ForMember(dest => dest.Ddsdtlid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.List, BL.Models.List>()
                    .ForMember(dest => dest.Lstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ListItem, BL.Models.ListItem>()
                    .ForMember(dest => dest.Lstid, opt => opt.Ignore())
                    .ForMember(dest => dest.Lstitemid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Output, BL.Models.Output>()
                    .ForMember(dest => dest.Ouid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.OutputDetail, BL.Models.OutputDetail>()
                    .ForMember(dest => dest.Ouid, opt => opt.Ignore())
                    .ForMember(dest => dest.Oudtlid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.TabDetail, BL.Models.TabDetail>()
                    .ForMember(dest => dest.Tabid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.TabChildDetail, BL.Models.TabChildDetail>()
                    .ForMember(dest => dest.Tabcid, opt => opt.Ignore())
                    .ForMember(dest => dest.Tabid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Page, BL.Models.Page>()
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore())
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.PageChildSetting, BL.Models.PageChildSetting>()
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore())
                    .ForMember(dest => dest.Pgchstid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Component, BL.Models.Component>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    .ForMember(dest => dest.Pgid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentAdditionalSetting, BL.Models.ComponentAdditionalSetting>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cadstid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentDatasetSchemaDetail, BL.Models.ComponentDatasetSchemaDetail>()
                    .ForMember(dest => dest.Cmpid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cdsdid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.ComponentDatasetSchemaPropertyDetail, BL.Models.ComponentDatasetSchemaPropertyDetail>()
                    .ForMember(dest => dest.Cdspdid, opt => opt.Ignore())
                    .ForMember(dest => dest.Cdsdid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.Form, BL.Models.Form>()
                    .ForMember(dest => dest.Fid, opt => opt.Ignore());

                cfg.CreateMap<BL.Models.CalculationAdditionalSetting, BL.Models.CalculationAdditionalSetting>()
                    .ForMember(dest => dest.Calasid, opt => opt.Ignore())
                    .ForMember(dest => dest.Calcid, opt => opt.Ignore());



            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }

    // public class NoMapAttribute : System.Attribute
    // {
    // }

    // //Extension Method: 
    // //The Class Should Be Static
    // //Method Should Be Static
    // //First Parameter is the class which which we can access the Method
    // public static class IgnoreNoMapExtensions
    // {
    //     //Method is Generic and Hence we can use with any TSource and TDestination Type
    //     public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
    //         this IMappingExpression<TSource, TDestination> expression)
    //     {
    //         //Fetching Type of the TSource
    //         var sourceType = typeof(TSource);
    //         //Fetching All Properties of the Source Type using GetProperties() method
    //         foreach (var property in sourceType.GetProperties())
    //         {
    //             //Get the Property Name
    //             PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
    //             //Check if Property is Decorated with the NoMapAttribute
    //             NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
    //             if (attribute != null)
    //             {
    //                 //If Property is Decorated with NoMap Attribute, call the Ignore Method
    //                 expression.ForMember(property.Name, opt => opt.Ignore());
    //             }
    //         }
    //         return expression;
    //     }

    //     public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination>(
    //this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
    //     {
    //         map.ForMember(selector, config => config.Ignore());
    //         return map;
    //     }
    // }


}
