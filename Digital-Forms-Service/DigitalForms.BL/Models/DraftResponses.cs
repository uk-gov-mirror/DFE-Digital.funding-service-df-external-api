using DigitalForms.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models;


public class DraftResponse
{
    public string Id { get; set; }
    //public string OrgUKPRN { get; set; }
    //public string DsiSignInEmail { get; set; }
    public List<string>? Progress { get; set; }
    public Dictionary<string, object>? FormData { get; set; }
    public Dictionary<string, object>? DataImportStatus { get; set; }
    public Dictionary<string, object>? Progresses { get; set; }
    public Dictionary<string, object>? PreviousPages { get; set; }
    public string PreviousPage { get; set; }
    public Dictionary<string, object>? SelectField { get; set; }
    public string Reference { get; set; }
    public string ReferenceIsStored { get; set; }
    public bool? UserCompletedSummary { get; set; }
    public string FormDataId { get; set; }
    public string Formid { get; set; }
    public Users? user { get; set; }
    public UserOrganisationDetail? UserOrganisationDetails { get; set; }
    //public Response? Responses { get; set; }
    public Output[]? outputs { get; set; }

    public Dictionary<string, object>? otherobjects { get; set; }
}
