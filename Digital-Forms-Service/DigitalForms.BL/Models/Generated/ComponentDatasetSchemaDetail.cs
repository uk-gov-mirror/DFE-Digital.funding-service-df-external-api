using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Cmpid", Name = "index_cmpdatschdtl_cmpid")]
public partial class ComponentDatasetSchemaDetail
{
    [Key]
    [Column("CDSDID")]
    public long Cdsdid { get; set; }

    [Column("CMPID")]
    public long Cmpid { get; set; }

    [StringLength(250)]
    public string? ColumnId { get; set; }

    [StringLength(250)]
    public string? ColumnType { get; set; }

    [StringLength(250)]
    public string? SelectedColumnHeaderType { get; set; }

    [StringLength(1000)]
    public string? SelectedColumnHeaderValue { get; set; }

    public bool? IsEdited { get; set; }

    [Column("columnSchema")]
    public bool? ColumnSchema { get; set; }

    public int? ColumnOrder { get; set; }

    [ForeignKey("Cmpid")]
    [InverseProperty("ComponentDatasetSchemaDetails")]
    public virtual Component Cmp { get; set; } = null!;

    [InverseProperty("Cdsd")]
    public virtual ICollection<ComponentDatasetSchemaPropertyDetail> ComponentDatasetSchemaPropertyDetails { get; set; } = new List<ComponentDatasetSchemaPropertyDetail>();

    [InverseProperty("Cdsd")]
    public virtual ICollection<DatasetDataDetail> DatasetDataDetails { get; set; } = new List<DatasetDataDetail>();
}
