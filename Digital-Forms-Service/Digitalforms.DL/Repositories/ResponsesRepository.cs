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
using System.Security.Cryptography;
using DigitalForms.BL.Models.Generated;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalForms.DL.Repositories
{
    public class ResponsesRepository : GenericRepository<Response>, IResponsesRepository
    {
        public ResponsesRepository(DFSqlContext context, s255d01dbDfSharedContext readcontext) : base(context, readcontext)
        {
            context.Database.SetCommandTimeout(180);
        }

        public IAsyncEnumerable<object> GetResponses(string FormId, string StartDate, string EndDate)
        {
            try
            {
                var query = _context.Responses
                    .AsNoTracking()
                    .AsSplitQuery()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(FormId))
                {
                    var formIdList = FormId
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(f => f.Trim())
                        .ToList();

                    query = query.Where(r => formIdList.Contains(r.Fid));
                }

                if (!string.IsNullOrEmpty(StartDate) &&
                    !string.IsNullOrEmpty(EndDate))
                {
                    var start = DateTime.Parse(StartDate).Date;
                    var end = DateTime.Parse(EndDate).Date.AddDays(1);

                    query = query.Where(r =>
                        r.UpdatedOn >= start &&
                        r.UpdatedOn < end);
                }

                var data = query.Select(r => new
                {
                    r.Rspid,
                    r.Fid,
                    r.UpdatedOn,
                    r.UpdatedBy,
                    r.ResponseStatus,
                    r.Id,
                    r.FormName,
                    r.UserId,
                    r.MtdtId,
                    r.isUAT,
                    UserDetail = r.User == null ? null : new
                    {
                        r.User.Uid,
                        r.User.UserId,
                        r.User.Name,
                        r.User.Email,
                        r.User.Status,
                        OrganisationDetails =
                            r.UserOrganisationDetails.OrganisationDetails == null
                            ? null
                            : new
                            {
                                r.UserOrganisationDetails.OrganisationDetails.Orgid,
                                r.UserOrganisationDetails.OrganisationDetails.Ukprn,
                                r.UserOrganisationDetails.OrganisationDetails.Urn,
                                r.UserOrganisationDetails.OrganisationDetails.AdminCode,
                                r.UserOrganisationDetails.OrganisationDetails.Name
                            }
                    }
                }).AsAsyncEnumerable();

                return data;
            }
            catch
            {
                throw;
            }
        }
        public IAsyncEnumerable<object> GetResponseQuestion(string FormId, string StartDate, string EndDate)
        {
            try
            {
                var query = _context.Responses
                    .AsNoTracking()
                    .AsSplitQuery()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(FormId))
                {
                    var formIdList = FormId
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(f => f.Trim())
                        .ToList();

                    query = query.Where(r => formIdList.Contains(r.Fid)); 
                }

                if (!string.IsNullOrEmpty(StartDate) &&
                    !string.IsNullOrEmpty(EndDate))
                {
                    var start = DateTime.Parse(StartDate).Date;
                    var end = DateTime.Parse(EndDate).Date.AddDays(1);

                    query = query.Where(r =>
                        r.UpdatedOn >= start &&
                        r.UpdatedOn < end);
                }

                var rspIds = query.Select(r => r.Rspid);

                return _context.ResponseQuestions
                    .Where(rq => rspIds.Contains(rq.RspId))
                    .AsNoTracking()
                    .AsSplitQuery().Select(r => new
                    {
                        r.RspQstId,
                        r.RspId,
                        r.Question
                    }).AsAsyncEnumerable();
            }
            catch
            {
                throw;
            }
        }

        public IAsyncEnumerable<object> GetResponseQuestionData(string FormId, string StartDate, string EndDate)
        {
            try
            {
                var query = _context.Responses
                    .AsNoTracking()
                    .AsSplitQuery()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(FormId))
                {
                    var formIdList = FormId
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(f => f.Trim())
                        .ToList();

                    query = query.Where(r => formIdList.Contains(r.Fid));
                }

                if (!string.IsNullOrEmpty(StartDate) &&
                    !string.IsNullOrEmpty(EndDate))
                {
                    var start = DateTime.Parse(StartDate).Date;
                    var end = DateTime.Parse(EndDate).Date.AddDays(1);

                    query = query.Where(r =>
                        r.UpdatedOn >= start &&
                        r.UpdatedOn < end);
                }

                var rspIds = query.Select(r => r.Rspid);

                var responseQuestions = _context.ResponseQuestions
                    .Where(rq => rspIds.Contains(rq.RspId))
                    .AsNoTracking()
                    .AsSplitQuery().Select(r => r.RspQstId);


                return _context.ResponseQuestionData
                    .Where(rq => responseQuestions
                    .Contains(rq.RspQstId))
                    .AsNoTracking()
                    .AsSplitQuery().Select(r => new
                    {
                        r.RspQstDataId,
                        r.RspQstId,
                        r.Key,
                        r.Title,
                        r.Type,
                        r.Answer
                    }).AsAsyncEnumerable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public bool checkResponseExists(string submissionId)
        {
            bool result = false;

            result = _context.Responses.Where(w => (submissionId != null) ? w.Id == submissionId : false).Any();

            return result;

        }
        public List<Tuple<string, string, string>> GetSubmittedStatusByParentId(string formId, string ukprn, bool isUAT)
        {
            if (string.IsNullOrEmpty(formId)) return new List<Tuple<string, string, string>>();

            long fid = _context.Forms.Where(f => f.FormId == formId && f.Status == true)
                .Select(f => f.Fid).FirstOrDefault();

            var childFormIds = _context.ParentChilds.Where(pc => pc.Fid == fid)
                .SelectMany(pc => pc.ParentChildConfig.ChildConfigs.Select(cc => cc.ChildId)).ToHashSet();

            var existingFormIds = _context.Responses.Where(r => r.isUAT == isUAT && r.UserOrganisationDetails != null &&
                  r.UserOrganisationDetails.OrganisationDetails != null && r.UserOrganisationDetails.OrganisationDetails.Ukprn == ukprn)
                .Select(r => r.Fid).ToHashSet();

            var draftFormIds = _context.DraftResponse
                .Where(d => d.Formid != null &&
                    (
                        isUAT
                            ? d.Id.Length >= 13 && d.Id.Substring(10, d.Id.Length - 13) == ukprn
                            : d.Id.Length >= 10 && d.Id.Substring(10, d.Id.Length - 10) == ukprn
                    ))
                .Select(d => d.Formid)
                .Distinct()
                .ToHashSet();

            var providerMappings = _context.ProviderMappings.Where(pm => pm.Status == true && childFormIds.Contains(pm.Fid))
                .Select(pm => new
                {
                    pm.Fid,
                    IsAdminCodeMapped = pm.AdminCodeProvidersData.Any(ac => ac.AdminCode.Contains(ukprn)),
                    IsUKPRNMapped = pm.UKPRNProvidersData.Any(up => up.Ukprn.ToString().Contains(ukprn)),
                    IsURNMapped = pm.URNProvidersData.Any(urn => urn.Urn.ToString().Contains(ukprn))
                }).ToList();
            //var providerMappings = _context.ProviderMappings.Where(pm => childFormIds.Contains(pm.Fid))
            //    .Select(pm => new
            //    {
            //        pm.Fid,
            //        IsMapped = pm.AdminCodeProvidersData.Any(ac => ac.AdminCode.Contains(ukprn)) ||
            //                   pm.UKPRNProvidersData.Any(up => up.Ukprn.ToString().Contains(ukprn)) ||
            //                   pm.URNProvidersData.Any(urn => urn.Urn.ToString().Contains(ukprn))
            //    })
            //    .ToList();
            return childFormIds
                //.Where(childFormId => providerMappings.Any(pm => pm.Fid == childFormId && pm.IsMapped)) // Skip unmapped IDs
                .Select(childFormId =>
                {
                    string status = existingFormIds.Contains(childFormId) ? "Completed"
                                  : draftFormIds.Contains(childFormId) ? "In Progress"
                                  : "Not Started";

                    bool isMapped = providerMappings
                                                     .Where(pm => pm.Fid == childFormId)
                                                     .Any(pm => pm.IsAdminCodeMapped || pm.IsUKPRNMapped || pm.IsURNMapped);

                    return new Tuple<string, string, string>(childFormId, status, isMapped ? "Mapped" : "UnMapped");
                })
                .ToList();
        }


        public IEnumerable<BL.Models.Output> GetOutput(long? OUID)
        {
            var data = _context.Outputs.Where(w => w.Ouid == OUID)
                .Include(i => i.OutputDetails).ToList();
            return data;
        }
        public IEnumerable<BL.Models.UserOrganisationDetail> GetUserOrgDetails(long? UserOrgID)
        {
            var data = _context.UserOrganisationDetails.Where(w => w.UserOrgID == UserOrgID)
                .Include(i => i.User)
                .Include(i => i.OrganisationDetails)
                .ToList();
            return data;
        }

    }
}
