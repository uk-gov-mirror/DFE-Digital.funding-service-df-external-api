using DigitalForms.BL.Models;
using DigitalForms.BL.Serialized.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Interfaces
{
    public interface IFormsRepository : IGenericRepository<Form>
    {
        IEnumerable<Form> getConfiguration(string formId, bool useredcontext = false);
        Form getFormById(string formId);
        ParentDetail getParentById(string formId);
        BL.Serialized.Models.ParentChild getParentChild(BL.Serialized.Models.ParentChild parentChild);
        bool checkFormExists(string formId, string formname);

        IEnumerable<Form> listFormConfigurations();

        IEnumerable<TblId> GetIdGeneration();
        Task<int> Delete(string formid);

        Task<int> Updateform( Form updatedentity);

        Task<long> getFormFID(string formId);
        IQueryable<UserDetail> GetUserDetails(Guid userId);
        IQueryable<OrganisationDetail> GetOrganisationDetail(string Ukprn);
        IQueryable<UserOrganisationDetail> GetUserOrganisationDetail(long? UserId, long? OrgId);
        //IEnumerable<UserDetail> GetUserDetails(Guid UserId);
        //IEnumerable<OrganisationDetail> GetOrganisationDetail(string Ukprn);
        //IEnumerable<UserOrganisationDetail> GetUserOrganisationDetail(long? UserId, long? OrgId);

        Task<bool> GetLatestProviderMappingbyId(string id, int? ukprn, int? urn, string? admincode);
        Task<List<ProviderMapping>> GetLatestProviderMapping (string id, DateTime date);
        IEnumerable<KeyValuePair<string, string>> GetAllChild();
        List<KeyValuePair<string, string>> getFormNamesById(string[] formId);
        List<KeyValuePair<string, bool>> checkMultipleFormExists(string[] formIds);
    }
}
