using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Cnid", Name = "index_cndndtl_cnid")]
public partial class ConditionDetail
{
    [Key]
    [Column("CNDTLID")]
    public long Cndtlid { get; set; }

    [Column("CNID")]
    public long Cnid { get; set; }

    public long? SubsetNo { get; set; }

    [StringLength(250)]
    public string? PropName { get; set; }

    [StringLength(1000)]
    public string? PropValue { get; set; }

    [StringLength(250)]
    public string? PropType { get; set; }

    [ForeignKey("Cnid")]
    [InverseProperty("ConditionDetails")]
    public virtual Condition Cn { get; set; } = null!;
}
