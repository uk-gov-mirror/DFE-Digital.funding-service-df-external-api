using DigitalForms.BL.Interfaces;
using DigitalForms.BL.Models;
using DigitalForms.DL.Data;
using DigitalForms.DL.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DigitalForms.BL.Serialized.Models;
using DigitalForms.BL.Data;

namespace DigitalForms.DL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DFSqlContext _context;
        protected readonly s255d01dbDfSharedContext _readcontext;
        public UnitOfWork(DFSqlContext context, s255d01dbDfSharedContext readcontext)
        {
            _context = context;
            _readcontext = readcontext;
            FormsRepository = new FormsRepository(_context, _readcontext);
            PagesRepository = new GenericRepository<BL.Models.Page>(_context, _readcontext);
            PageChildsRepository = new GenericRepository<BL.Models.PageChildSetting>(_context, _readcontext);
            ComponentRepository = new GenericRepository<BL.Models.Component>(_context, _readcontext);
            ComponentAdditionalSettingsRepository = new GenericRepository<BL.Models.ComponentAdditionalSetting>(_context, _readcontext);
            ComponentDatasetSchemaDetailsRepository = new GenericRepository<BL.Models.ComponentDatasetSchemaDetail>(_context, _readcontext);
            ComponentDatasetSchemaPropertyDetailsRepository = new GenericRepository<BL.Models.ComponentDatasetSchemaPropertyDetail>(_context, _readcontext);
            userRepository = new GenericRepository<UserDetail>(_context, _readcontext);
            formStatusRepository = new GenericRepository<FormStatus>(_context, _readcontext);
            CalculationsRepository = new GenericRepository<BL.Models.Calculation>(_context, _readcontext);
            CalculationAdditionalSettingsRepository = new GenericRepository<BL.Models.CalculationAdditionalSetting>(_context, _readcontext);
            CalculationComponentDetailsRepository = new GenericRepository<BL.Models.CalculationComponentDetail>(_context, _readcontext);
            ConditionsRepository = new GenericRepository<BL.Models.Condition>(_context, _readcontext);
            ConditionDetailsRepository = new GenericRepository<BL.Models.ConditionDetail>(_context, _readcontext);
            DocumentsRepository = new GenericRepository<BL.Models.Document>(_context, _readcontext);
            SectionsRepository = new GenericRepository<BL.Models.Section>(_context, _readcontext);
            ListsRepository = new GenericRepository<BL.Models.List>(_context, _readcontext);
            ListsItemsRepository = new GenericRepository<BL.Models.ListItem>(_context, _readcontext);
            TabDetailsRepository = new GenericRepository<BL.Models.TabDetail>(_context, _readcontext);
            TabChildDetailsRepository = new GenericRepository<BL.Models.TabChildDetail>(_context, _readcontext);
            OutputsRepository = new GenericRepository<BL.Models.Output>(_context, _readcontext);
            OutputDetailsRepository = new GenericRepository<BL.Models.OutputDetail>(_context, _readcontext);
            DesignDataSetsRepository = new GenericRepository<BL.Models.DesignDataSet>(_context, _readcontext);
            DesignDataSetDetailsRepository = new GenericRepository<BL.Models.DesignDataSetDetail>(_context, _readcontext);
            ResponseRepository = new GenericRepository<Response>(_context, _readcontext);
            ProviderMappingRepository = new GenericRepository<ProviderMapping>(_context, _readcontext);
            ResponsesRepository = new ResponsesRepository(_context, _readcontext);
            organisationRepository = new GenericRepository<OrganisationDetail>(_context, _readcontext);
            userOrganisationRepository = new GenericRepository<UserOrganisationDetail>(_context, _readcontext);
            ParentChildRepository = new GenericRepository<BL.Models.ParentChild>(_context, _readcontext);
            ParentChildConfigRepository = new GenericRepository<BL.Models.ParentChildConfig>(_context, _readcontext);
            ChildConfigRepository = new GenericRepository<BL.Models.ChildConfig>(_context, _readcontext);
            DependentFormRepository = new GenericRepository<BL.Models.DependentForm>(_context, _readcontext);
            DraftResponseRepository = new DraftResponseRepository(_context, _readcontext);
            SubmissionFormLogRepository = new GenericRepository<BL.Models.SubmissionFormLog>(_context, _readcontext);
            RepeatableFormsDataRepository = new GenericRepository<BL.Models.RepeatableFormsData>(_context, _readcontext);
            RepeatableSectionsDataRepository = new GenericRepository<BL.Models.RepeatableSectionsData>(_context, _readcontext);
            RepeatableFormsMapperRepository = new GenericRepository<BL.Models.RepeatableFormsMapper>(_context, _readcontext);
            DCDATARepository = new GenericRepository<BL.Models.DCData>(_context, _readcontext);

            _context.ChangeTracker.LazyLoadingEnabled = false;
        }
        public IFormsRepository FormsRepository { get; private set; }

        public IGenericRepository<UserDetail> userRepository { get; private set; }
        public IGenericRepository<OrganisationDetail> organisationRepository { get; private set; }
        public IGenericRepository<UserOrganisationDetail> userOrganisationRepository { get; private set; }

        public IGenericRepository<FormStatus> formStatusRepository { get; private set; }

        public IGenericRepository<BL.Models.Page> PagesRepository { get; private set; }

        public IGenericRepository<BL.Models.PageChildSetting> PageChildsRepository { get; private set; }
        public IDraftResponseRepository DraftResponseRepository { get; private set; }

        public IGenericRepository<BL.Models.Component> ComponentRepository { get; private set; }
        public IGenericRepository<BL.Models.CalculationComponentDetail> CalculationComponentDetailsRepository { get; private set; }
        public IGenericRepository<BL.Models.ComponentAdditionalSetting> ComponentAdditionalSettingsRepository { get; private set; }
        public IGenericRepository<BL.Models.ComponentDatasetSchemaDetail> ComponentDatasetSchemaDetailsRepository { get; private set; }
        public IGenericRepository<BL.Models.ComponentDatasetSchemaPropertyDetail> ComponentDatasetSchemaPropertyDetailsRepository { get; private set; }

        public IGenericRepository<BL.Models.Calculation> CalculationsRepository { get; private set; }
        public IGenericRepository<BL.Models.CalculationAdditionalSetting> CalculationAdditionalSettingsRepository { get; private set; }

        public IGenericRepository<BL.Models.Condition> ConditionsRepository { get; private set; }
        public IGenericRepository<BL.Models.ConditionDetail> ConditionDetailsRepository { get; private set; }

        public IGenericRepository<BL.Models.Document> DocumentsRepository { get; private set; }

        public IGenericRepository<BL.Models.Section> SectionsRepository { get; private set; }

        public IGenericRepository<BL.Models.DesignDataSet> DesignDataSetsRepository { get; private set; }

        public IGenericRepository<BL.Models.DesignDataSetDetail> DesignDataSetDetailsRepository { get; private set; }

        public IGenericRepository<BL.Models.List> ListsRepository { get; private set; }
        public IGenericRepository<BL.Models.ListItem> ListsItemsRepository { get; private set; }

        public IGenericRepository<BL.Models.Output> OutputsRepository { get; private set; }
        public IGenericRepository<BL.Models.OutputDetail> OutputDetailsRepository { get; private set; }

        public IGenericRepository<BL.Models.TabDetail> TabDetailsRepository { get; private set; }
        public IGenericRepository<BL.Models.TabChildDetail> TabChildDetailsRepository { get; private set; }
        public IGenericRepository<BL.Models.Response> ResponseRepository { get; private set; }

        public IGenericRepository<ProviderMapping> ProviderMappingRepository { get; private set; }
        public IGenericRepository<BL.Models.ParentChild> ParentChildRepository { get; private set; }

        public IGenericRepository<BL.Models.ParentChildConfig> ParentChildConfigRepository { get; private set; }
        public IGenericRepository<BL.Models.ChildConfig> ChildConfigRepository { get; private set; }
        public IGenericRepository<BL.Models.DependentForm> DependentFormRepository { get; private set; }

        public IGenericRepository<BL.Models.SubmissionFormLog> SubmissionFormLogRepository { get; private set; }
        public IGenericRepository<BL.Models.RepeatableFormsData> RepeatableFormsDataRepository { get; private set; }
        public IGenericRepository<BL.Models.RepeatableSectionsData> RepeatableSectionsDataRepository { get; private set; }
        public IGenericRepository<BL.Models.RepeatableFormsMapper> RepeatableFormsMapperRepository { get; private set; }

        public IGenericRepository<BL.Models.DCData> DCDATARepository { get; private set; }

        public IResponsesRepository ResponsesRepository { get; private set; }


        public async Task<int> Complete()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.DetectChanges();
             var result =await _context.SaveChangesAsync();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }       

        public void Dispose()
        {

            _context.Dispose();
            _readcontext.Dispose();
        }
    }
}
