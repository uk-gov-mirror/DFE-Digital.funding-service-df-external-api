using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;
[Table("UserOrganisationDetails")]
public partial class UserOrganisationDetail
{
    [Key] 
    public long UserOrgID { get; set; }
    public long UID { get; set; }
    public long ORGID { get; set; }


    [ForeignKey("ORGID")]
    [InverseProperty("UserOrganisationDetails")]
    public virtual OrganisationDetail OrganisationDetails { get; set; } = null!;

    [ForeignKey("UID")]
    [InverseProperty("UserOrganisationDetails")]
    public virtual UserDetail User { get; set; } = null!;

    [InverseProperty("UserOrganisationDetails")]
    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    [InverseProperty("UserOrganisationDetails")]
    public virtual ICollection<DraftResponse> DraftResponses { get; set; } = new List<DraftResponse>();
}
