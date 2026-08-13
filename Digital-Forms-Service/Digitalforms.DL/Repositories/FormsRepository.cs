using DigitalForms.BL.Models;
using DigitalForms.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalForms.DL.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DigitalForms.BL.Serialized.Models;
using AutoMapper;
using Microsoft.SqlServer.Server;
using DigitalForms.BL.Data;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DigitalForms.DL.UnitOfWork;

namespace DigitalForms.DL.Repositories
{
    public class FormsRepository : GenericRepository<Form>, IFormsRepository
    {
        public FormsRepository(DFSqlContext context, s255d01dbDfSharedContext readcontext ) : base(context,readcontext)
        {
            context.Database.SetCommandTimeout(180);
        }
        public IEnumerable<Form> getConfiguration(string formId, bool usereadcontext=false)
        {

            if (usereadcontext)
            {               

                var data = _readcontext.Forms.Where(w => formId != string.Empty ? w.FormId == formId : w.Fid > 0).Where(ws => ws.Status == true)
                 .Include(d => d.Pages).ThenInclude(d => d.PageChildSettings)
                 .Include(d => d.Pages).ThenInclude(d => d.Components.OrderBy(o => o.CmpOrder)).ThenInclude(d => d.ComponentAdditionalSettings)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.ComponentDatasetSchemaDetails).ThenInclude(d => d.ComponentDatasetSchemaPropertyDetails)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.ComponentDatasetSchemaDetails).ThenInclude(d => d.DatasetDataDetails)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.CalculationComponentDetails)
                 .Include(d => d.CreatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                 .Include(d => d.LastUpdatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                 .Include(d => d.Fs)
                 .Include(d => d.Calculations).ThenInclude(d => d.CalculationComponentDetails)
                 .Include(d => d.Calculations).ThenInclude(d => d.CalculationAdditionalSettings)
                 .Include(d => d.Conditions).ThenInclude(d => d.ConditionDetails.OrderBy(d => d.Cnid).ThenBy(d => d.SubsetNo).ThenBy(d => d.PropType).ThenBy(d => d.PropName))
                 .Include(d => d.DesignDataSets).ThenInclude(d => d.DesignDataSetDetails)
                 .Include(d => d.Documents).ThenInclude(d => d.DesignDataSets).ThenInclude(d => d.DesignDataSetDetails)
                 .Include(d => d.Fees)
                 .Include(d => d.Lists).ThenInclude(d => d.ListItems.OrderBy(o => o.LSTOrder))
                 .Include(d => d.MetaData)
                 .Include(d => d.Outputs).ThenInclude(d => d.OutputDetails)
                 .Include(d => d.Sections)
                 .Include(d => d.TabDetails).ThenInclude(d => d.TabChildDetails)
                 .Include(d => d.ParentChild).ThenInclude(d => d.ParentChildConfig).ThenInclude(d => d.ChildConfigs).ThenInclude(d => d.DependentForms)

                 .AsSplitQuery()
                 .ToList();               
                return data;
            }
            else
            {
               
                var data = _context.Forms.Where(w => formId != string.Empty ? w.FormId == formId : w.Fid > 0).Where(ws => ws.Status == true)
                 .Include(d => d.Pages).ThenInclude(d => d.PageChildSettings)
                 .Include(d => d.Pages).ThenInclude(d => d.Components.OrderBy(o => o.CmpOrder)).ThenInclude(d => d.ComponentAdditionalSettings)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.ComponentDatasetSchemaDetails).ThenInclude(d => d.ComponentDatasetSchemaPropertyDetails)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.ComponentDatasetSchemaDetails).ThenInclude(d => d.DatasetDataDetails)
                 .Include(d => d.Pages).ThenInclude(d => d.Components).ThenInclude(d => d.CalculationComponentDetails)
                 .Include(d => d.CreatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                 .Include(d => d.LastUpdatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                 .Include(d => d.Fs)
                 .Include(d => d.Calculations).ThenInclude(d => d.CalculationComponentDetails)
                 .Include(d => d.Calculations).ThenInclude(d => d.CalculationAdditionalSettings)
                 .Include(d => d.Conditions).ThenInclude(d => d.ConditionDetails.OrderBy(d => d.Cnid).ThenBy(d => d.SubsetNo).ThenBy(d => d.PropType).ThenBy(d => d.PropName))
                 .Include(d => d.DesignDataSets).ThenInclude(d => d.DesignDataSetDetails)
                 .Include(d => d.Documents).ThenInclude(d => d.DesignDataSets).ThenInclude(d => d.DesignDataSetDetails)
                 .Include(d => d.Fees)
                 .Include(d => d.Lists).ThenInclude(d => d.ListItems.OrderBy(o => o.LSTOrder))
                 .Include(d => d.MetaData)
                 .Include(d => d.Outputs).ThenInclude(d => d.OutputDetails)
                 .Include(d => d.Sections)
                 .Include(d => d.TabDetails).ThenInclude(d => d.TabChildDetails)
                 .Include(d => d.ParentChild).ThenInclude(d => d.ParentChildConfig).ThenInclude(d => d.ChildConfigs).ThenInclude(d => d.DependentForms)

                 .AsSplitQuery()
                 .ToList();               
                return data;
            }
        }        
        public ParentDetail getParentById(string formId)
        {
            string data = _context.ChildConfigs.Where(w => w.ChildId == formId)?.Select(s => s.ParentId).FirstOrDefault();
            if (data.IsNullOrEmpty())
                data = _context.DependentForms.Where(w => w.FormId == formId)?.Select(s => s.MainParentId).FirstOrDefault();

            if (!data.IsNullOrEmpty())
                return _context.Forms.Where(w => w.FormId == data)?.Select(s => new ParentDetail { parentId = s.FormId, parentName = s.Displayname }).FirstOrDefault();
            else
                return null;
        }
        public BL.Serialized.Models.ParentChild getParentChild(BL.Serialized.Models.ParentChild ParentChild)
        {
            var childConfigs = ParentChild?.parentChildConfig?.childConfigs;
            if (childConfigs == null || !childConfigs.Any())
                return ParentChild;

            var allFormIds = childConfigs
                .Select(s => s.childId)
                .Concat(childConfigs.SelectMany(s => s.dependentforms.Select(d => d.id)))
                .Distinct()
                .ToHashSet();

            List<Form> forms = _context.Forms.Where(w=> allFormIds.Contains(w.FormId)).ToList();

            foreach (ChildConfiguration item in ParentChild.parentChildConfig.childConfigs)
            {
               Form selectedform = forms.Where(w=>w.FormId == item.childId).FirstOrDefault();
                item.childFormName = selectedform.Displayname;
                item.childFormTitle = selectedform.Name;
                foreach (Dependentform dependentform in item.dependentforms)
                {
                    Form selectedChildform = forms.Where(w => w.FormId == dependentform.id).FirstOrDefault();
                    dependentform.name = selectedChildform.Displayname;
                    dependentform.title = selectedChildform.Name;
                }
            }

            return ParentChild;
        }
        //public List<KeyValuePair<string, string>> getAllParentById()
        //{
        //    List<KeyValuePair<string, string>> data = _context.ChildConfigs?.Select(s => new KeyValuePair<string, string>(s.ChildId, s.ParentId)).ToList();
        //    data = _context.DependentForms.Where(w => w.FormId == formId)?.Select(s => s.MainParentId).FirstOrDefault();
        //    return data;
        //}

        public Form getFormById(string formId)
        {
            var data = _context.Forms.Where(w => formId != string.Empty ? w.FormId == formId : w.Fid > 0).Where(ws => ws.Status == true).FirstOrDefault();
            return data;
        }
        public bool checkFormExists(string formId, string formname)
        {
            bool result = false;

            result = _context.Forms.Where(w => (formId != null) ? w.FormId == formId : w.Displayname == formname).Where(ws => ws.Status == true).ToList().Count > 0 ? true : false;

            return result;

        }
        public List<KeyValuePair<string, string>> getFormNamesById(string[] formId)
        {
            var data = _context.Forms
                            .Where(w => formId.Contains(w.FormId))
                            .Select(s => new KeyValuePair<string, string>(s.FormId, s.Name))
                            .ToList();
            return data;
        }

        public List<KeyValuePair<string, bool>> checkMultipleFormExists(string[] formIds)
        {
            List<string?> existingFormIds = _context.Forms.Where(f => formIds.Contains(f.FormId)).Where(ws => ws.Status == true).Select(f => f.FormId).ToList();
            List<KeyValuePair<string, bool>> result = new List<KeyValuePair<string, bool>>();
            foreach (string formId in formIds)
            {
                bool exists = existingFormIds.Contains(formId);
                result.Add(new KeyValuePair<string, bool>(formId, exists));
            }
            return result;
        }


        //public long getFormFID(string formId)
        //{
        //    return _context.Forms.Where(w => w.FormId == formId).Where(ws => ws.Status == true).ToList().Count > 0 ? _context.Forms.Where(w => w.FormId == formId).Where(ws => ws.Status == true).FirstOrDefault().Fid : 0;
        //}
        public async Task<long> getFormFID(string formId)
        {
            var form = await _context.Forms
                .Where(w => w.FormId == formId && w.Status == true).AsNoTracking()
                .FirstOrDefaultAsync();

            return form?.Fid ?? 0;
        }
        public IEnumerable<Form> listFormConfigurations()
        {           
            var data = _context.Forms.Where(ws => ws.Status == true)
                        .Include(d => d.CreatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                        .Include(d => d.LastUpdatedByUser)//.ThenInclude(d => d.OrganisationDetails)
                        .Include(d => d.Fs)
                        .Include(d => d.ParentChild).ThenInclude(d => d.ParentChildConfig).ThenInclude(s => s.ChildConfigs).ThenInclude(s => s.DependentForms)
                        .AsSplitQuery()
                        .ToList();
           
            return data;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllChild()
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            data.AddRange(_context.ChildConfigs
                .GroupBy(g => g.ParentId)
                .Select(item => new KeyValuePair<string, string>(
                    item.Key.ToString(), // Convert the Key to string
                    string.Join(",", item.Select(s => s.ChildId.ToString())) // Convert ChildId to string
                )));

            data.AddRange(_context.DependentForms
                .GroupBy(g => g.MainParentId)
                .Select(item => new KeyValuePair<string, string>(
                    item.Key.ToString(), // Convert the Key to string
                    string.Join(",", item.Select(s => s.FormId.ToString())) // Convert FormId to string
                )));

            return data;
        }

        public IEnumerable<TblId> GetIdGeneration()
        {
           
            var data = _context.TblIds
                        .AsSplitQuery()
                        .ToList();
           
            return data;
        }

        public async Task<int> Delete(string formid)
        {
            int result = 0;
            result = await _context.Database.ExecuteSqlRawAsync($"execute DeleteFormData '{formid}'");
            return result;
        }

        public async Task<int> Updateform(Form updatedentity)
        {
            int result = 0;
            //_context.Forms.Attach(updatedentity);
            result = await _context.Forms.ExecuteUpdateAsync(set => set.SetProperty(dbent => dbent, updatedentity));
            return result;
        }

        //public IEnumerable<UserDetail> GetUserDetails(Guid UserId)
        //{
        //    var data = _context.UserDetails.Where(w => w.UserId == UserId)
        //        .AsSplitQuery()
        //        .ToList();
        //    return data;
        //}
        public IQueryable<UserDetail> GetUserDetails(Guid userId)
        {
            return _context.UserDetails
                .Where(w => w.UserId == userId);
        }
        //public IEnumerable<OrganisationDetail> GetOrganisationDetail(string Ukprn)
        //{
        //    var data = _context.OrganisationDetails.Where(w => w.Ukprn == Ukprn)
        //        .AsSplitQuery()
        //        .ToList();
        //    return data;
        //}
        public IQueryable<OrganisationDetail> GetOrganisationDetail(string Ukprn)
        {
            return _context.OrganisationDetails
                .Where(w => w.Ukprn == Ukprn);
        }
        //public IEnumerable<UserOrganisationDetail> GetUserOrganisationDetail(long? UserId,long? OrgId)
        //{
        //    var data = _context.UserOrganisationDetails.Where(w => w.UID == UserId && w.ORGID == OrgId)
        //        .AsSplitQuery()
        //        .ToList();
        //    return data;
        //}
        public IQueryable<UserOrganisationDetail> GetUserOrganisationDetail(long? UserId, long? OrgId)
        {
            return _context.UserOrganisationDetails.Where(w => w.UID == UserId && w.ORGID == OrgId);
        }
        public async Task<List<ProviderMapping>> GetLatestProviderMapping(string id, DateTime dateTime)
        {
            List<ProviderMapping> data = await _context.ProviderMappings
                                                       .Where(w => w.Fid == id)
                                                       .Where(ws => ws.Status == true && ws.UpdatedOn != dateTime)
                                                       .OrderByDescending(o => o.UpdatedOn)
                                                       .AsSplitQuery()
                                                       .ToListAsync();
            return data;
        }

        public async Task<bool> GetLatestProviderMappingbyId(string id, int? ukprn, int? urn, string? admincode)
        {
            return await _context.ProviderMappings.AsNoTracking().AsSplitQuery()
                .Where(w => w.Fid == id && w.Status == true)
                .Where(w =>
                    w.UKPRNProvidersData.Any(p => p.Ukprn == ukprn) ||
                    w.URNProvidersData.Any(p => p.Urn == urn) ||
                    w.AdminCodeProvidersData.Any(p => p.AdminCode == admincode))
                .OrderByDescending(w => w.UpdatedOn)
                .Select(w => true)
                .FirstOrDefaultAsync() == true;
        }
    }
}
