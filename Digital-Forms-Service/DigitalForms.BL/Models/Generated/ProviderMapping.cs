using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalForms.BL.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;

[Table("ProviderMapping")]
public partial class ProviderMapping
{
    [Key]
    [Column("PMID")]
    public long Pmid { get; set; }

    [Column("FID")]
    [StringLength(100)]
    public string Fid { get; set; } = null!;

    [StringLength(250)]
    public string? Desciption { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public bool? Status { get; set; }

    [InverseProperty("PmUKPRN")]
    public virtual ICollection<UKPRNProvidersData> UKPRNProvidersData { get; set; } = new List<UKPRNProvidersData>();

    [InverseProperty("PmURN")]
    public virtual ICollection<URNProvidersData> URNProvidersData { get; set; } = new List<URNProvidersData>();

    [InverseProperty("PmAC")]
    public virtual ICollection<AdminCodeProvidersData> AdminCodeProvidersData { get; set; } = new List<AdminCodeProvidersData>();
}
