using DigitalForms.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFormsRepository FormsRepository { get; }
        IGenericRepository<UserDetail> userRepository { get; }
        IGenericRepository<OrganisationDetail> organisationRepository { get; }
        IGenericRepository<UserOrganisationDetail> userOrganisationRepository { get;  }

        IGenericRepository<FormStatus> formStatusRepository { get;  }
        Task<int> Complete();

         IGenericRepository<Page> PagesRepository { get;  }
         IGenericRepository<PageChildSetting> PageChildsRepository { get;  }

        IGenericRepository<BL.Models.Component> ComponentRepository { get; }
        IGenericRepository<BL.Models.CalculationComponentDetail> CalculationComponentDetailsRepository { get; }
        IGenericRepository<BL.Models.ComponentAdditionalSetting> ComponentAdditionalSettingsRepository { get; }
        IGenericRepository<BL.Models.ComponentDatasetSchemaDetail> ComponentDatasetSchemaDetailsRepository { get;  }
        IGenericRepository<BL.Models.ComponentDatasetSchemaPropertyDetail> ComponentDatasetSchemaPropertyDetailsRepository { get; }

        IGenericRepository<Calculation> CalculationsRepository { get; }
        IGenericRepository<BL.Models.CalculationAdditionalSetting> CalculationAdditionalSettingsRepository { get;  }

        IGenericRepository<Condition> ConditionsRepository { get;  }

         IGenericRepository<ConditionDetail> ConditionDetailsRepository { get;  }

         IGenericRepository<Document> DocumentsRepository { get;  }

         IGenericRepository<Section> SectionsRepository { get;  }

         IGenericRepository<DesignDataSet> DesignDataSetsRepository { get;  }
        
        IGenericRepository<DesignDataSetDetail> DesignDataSetDetailsRepository { get; }

        IGenericRepository<List> ListsRepository { get;  }
        IGenericRepository<ListItem> ListsItemsRepository { get;  }

         IGenericRepository<Output> OutputsRepository { get;  }
         IGenericRepository<OutputDetail> OutputDetailsRepository { get;  }

         IGenericRepository<TabDetail> TabDetailsRepository { get;  }
         IGenericRepository<TabChildDetail> TabChildDetailsRepository { get;  }

        IGenericRepository<Response> ResponseRepository { get; }

        IGenericRepository<ProviderMapping> ProviderMappingRepository { get; } 
        IResponsesRepository ResponsesRepository { get; }
        IGenericRepository<ParentChild> ParentChildRepository { get; }
        IGenericRepository<ParentChildConfig> ParentChildConfigRepository { get; }
        IGenericRepository<ChildConfig> ChildConfigRepository { get; }
        IGenericRepository<DependentForm> DependentFormRepository { get; }
        IDraftResponseRepository DraftResponseRepository { get; }
        IGenericRepository<SubmissionFormLog> SubmissionFormLogRepository { get; }
        IGenericRepository<RepeatableFormsData> RepeatableFormsDataRepository { get; }
        IGenericRepository<RepeatableSectionsData> RepeatableSectionsDataRepository { get; }
        IGenericRepository<RepeatableFormsMapper> RepeatableFormsMapperRepository { get; }
        IGenericRepository<DCData> DCDATARepository { get; }


    }
}
