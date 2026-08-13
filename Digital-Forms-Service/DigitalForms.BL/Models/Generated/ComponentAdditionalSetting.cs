using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace DigitalForms.BL.Models;

[Index("Cmpid", Name = "index_compadst_cmpid")]
public partial class ComponentAdditionalSetting
{
    [Key]
    [Column("CADSTID")]
    public long Cadstid { get; set; }

    [Column("CMPID")]
    public long Cmpid { get; set; }

    [StringLength(250)]
    public string? PropName { get; set; }

    [StringLength(1000)]
    public string? PropValue { get; set; }

    [StringLength(250)]
    public string? PropType { get; set; }

    [ForeignKey("Cmpid")]
    [InverseProperty("ComponentAdditionalSettings")]
    public virtual Component Cmp { get; set; } = null!;

    [InverseProperty("Cadst")]
    public virtual ICollection<TabChildDetail> TabChildDetails { get; set; } = new List<TabChildDetail>();
}
