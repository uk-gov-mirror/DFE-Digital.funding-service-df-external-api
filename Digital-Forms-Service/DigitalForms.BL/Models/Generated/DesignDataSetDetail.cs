using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Ddsid", Name = "index_designdatastdtl_ddsid")]
public partial class DesignDataSetDetail
{
    [Key]
    [Column("DDSDTLID")]
    public long Ddsdtlid { get; set; }

    [Column("DDSID")]
    public long Ddsid { get; set; }

    [Column("index")]
    [StringLength(250)]
    public string? Index { get; set; }

    [StringLength(250)]
    public string? Type { get; set; }

    [StringLength(1000)]
    public string? Value { get; set; }

    public bool? Bold { get; set; }

    public bool? MarkForCalculation { get; set; }
    public bool? MarkAsNumber { get; set; }
    public string? Format { get; set; }
    [Column("Checked")]
    public bool? _checked { get; set; }

    public int? Rowno { get; set; }

    [ForeignKey("Ddsid")]
    [InverseProperty("DesignDataSetDetails")]
    public virtual DesignDataSet Dds { get; set; } = null!;
}
