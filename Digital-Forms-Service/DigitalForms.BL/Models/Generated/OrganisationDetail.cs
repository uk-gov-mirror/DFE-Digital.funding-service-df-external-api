using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;
 
public partial class OrganisationDetail
{
    [Key]
    [Column("ORGID")]
    public long Orgid { get; set; }

    //[Column("UID")]
    //public long? Uid { get; set; }

    [Column("UKPRN")]
    public string Ukprn { get; set; }

    [Column("URN")]
    public long? Urn { get; set; }

    [StringLength(50)]
    public string? AdminCode { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    [InverseProperty("OrganisationDetails")]
    public virtual ICollection<UserOrganisationDetail> UserOrganisationDetails { get; set; } = new List<UserOrganisationDetail>();

}
