using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_cndn_fid")]
public partial class Condition
{
    [Key]
    [Column("CNID")]
    public long Cnid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(250)]
    public string? DisplayName { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    [InverseProperty("Cn")]
    public virtual ICollection<ConditionDetail> ConditionDetails { get; set; } = new List<ConditionDetail>();

    [ForeignKey("Fid")]
    [InverseProperty("Conditions")]
    public virtual Form FidNavigation { get; set; } = null!;
}
