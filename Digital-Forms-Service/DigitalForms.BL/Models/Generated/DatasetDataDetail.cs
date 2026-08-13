using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Cdsdid", Name = "index_datsetdtl_cdsdid")]
public partial class DatasetDataDetail
{
    [Key]
    [Column("DDID")]
    public long Ddid { get; set; }

    [Column("CDSDID")]
    public long Cdsdid { get; set; }

    [StringLength(1000)]
    public string? ColumnData { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? Rowno { get; set; }

    [ForeignKey("Cdsdid")]
    [InverseProperty("DatasetDataDetails")]
    public virtual ComponentDatasetSchemaDetail Cdsd { get; set; } = null!;
}
