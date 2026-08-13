using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace DigitalForms.BL.Models;

[Index("Ouid", Name = "index_outputdtl_ouid")]
public partial class OutputDetail
{
    [Key]
    [Column("OUDTLID")]
    public long Oudtlid { get; set; }

    [Column("OUID")]
    public long Ouid { get; set; }

    [StringLength(250)]
    public string? PropName { get; set; }

    [StringLength(1000)]
    public string? PropValue { get; set; }

    [StringLength(250)]
    public string? PropType { get; set; }

    [ForeignKey("Ouid")]
    [InverseProperty("OutputDetails")]
    public virtual Output Ou { get; set; } = null!;
}
