using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Cdsdid", Name = "index_cmpdatschpropdtl_cdsdid")]
public partial class ComponentDatasetSchemaPropertyDetail
{
    [Key]
    [Column("CDSPDID")]
    public long Cdspdid { get; set; }

    [Column("CDSDID")]
    public long Cdsdid { get; set; }

    [StringLength(250)]
    public string? PropName { get; set; }

    [StringLength(1000)]
    public string? PropValue { get; set; }

    [StringLength(250)]
    public string? PropType { get; set; }

    [ForeignKey("Cdsdid")]
    [InverseProperty("ComponentDatasetSchemaPropertyDetails")]
    public virtual ComponentDatasetSchemaDetail Cdsd { get; set; } = null!;
}
