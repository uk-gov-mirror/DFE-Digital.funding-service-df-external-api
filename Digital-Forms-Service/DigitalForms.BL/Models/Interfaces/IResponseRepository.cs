using DigitalForms.BL.Models;
using DigitalForms.BL.Serialized.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Interfaces
{
    public interface IResponsesRepository : IGenericRepository<Response>
    {
        IAsyncEnumerable<object> GetResponses(string FormId, string StartDate, string EndDate);
        IAsyncEnumerable<object> GetResponseQuestion(string FormId, string StartDate, string EndDate);
        IAsyncEnumerable<object> GetResponseQuestionData(string FormId, string StartDate, string EndDate);
        bool checkResponseExists(string submissionId);
        public List<Tuple<string, string, string>> GetSubmittedStatusByParentId(string formId, string ukprn, bool isUAT);
        IEnumerable<BL.Models.Output> GetOutput(long? OUID);
        IEnumerable<BL.Models.UserOrganisationDetail> GetUserOrgDetails(long? UserOrgID);

    }
}
