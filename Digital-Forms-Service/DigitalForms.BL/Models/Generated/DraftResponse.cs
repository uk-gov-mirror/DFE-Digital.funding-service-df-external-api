using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("DraftResponse")]
public partial class DraftResponse
{
    [Key]
    public long DRID { get; set; }
    public string? Id { get; set; }
    public string? Formid { get; set; }
    public long? UserOrgID { get; set; }
    //public string? Progress { get; set; }
    public string? Key { get; set; }
    public string? Answer { get; set; }
    //public string? PreviousPage { get; set; }
    public long? OUID { get; set; }
    public long? RSPID { get; set; }
    public string? FormDataId { get; set; }
    public string? UserCompletedSummary { get; set; }
    public string? Reference { get; set; }
    public string? ReferenceIsStored { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? Type { get; set; }

    [ForeignKey("UserOrgID")]
    [InverseProperty("DraftResponses")]
    public virtual UserOrganisationDetail? UserOrganisationDetails { get; set; } = null;

    [ForeignKey("OUID")]
    [InverseProperty("DraftResponses")]
    public virtual Output? Outputs { get; set; } = null;


    [ForeignKey("RSPID")]
    [InverseProperty("DraftResponses")]
    public virtual Response? Responses { get; set; } = null;

}
