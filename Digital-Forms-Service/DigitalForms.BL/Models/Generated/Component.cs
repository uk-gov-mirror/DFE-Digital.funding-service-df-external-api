using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Ddsid", Name = "index_comp_ddsid")]
[Index("Docid", Name = "index_comp_docid")]
[Index("Lstid", Name = "index_comp_lstid")]
[Index("Pgid", Name = "index_comp_pgid")]
public partial class Component
{
    [Key]
    [Column("CMPID")]
    public long Cmpid { get; set; }

    [Column("PGID")]
    public long Pgid { get; set; }

    [StringLength(250)]
    public string? Type { get; set; }

    public bool? IsEditingTabs { get; set; }

    [StringLength(500)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Hint { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    public bool? Status { get; set; }

    public bool? NameHasError { get; set; }

    public bool? ComponentEdited { get; set; }

    public bool? AdditionalSettings { get; set; }

    public string? Content { get; set; }

    public bool? Checked { get; set; }
    public int? CmpOrder { get; set; }

    [Column("DDSID")]
    public long? Ddsid { get; set; }

    [Column("LSTID")]
    public long? Lstid { get; set; }

    [Column("DOCID")]
    public long? Docid { get; set; }

    [InverseProperty("Cmp")]
    public virtual ICollection<CalculationComponentDetail> CalculationComponentDetails { get; set; } = new List<CalculationComponentDetail>();

    [InverseProperty("Cmp")]
    public virtual ICollection<ComponentAdditionalSetting> ComponentAdditionalSettings { get; set; } = new List<ComponentAdditionalSetting>();

    [InverseProperty("Cmp")]
    public virtual ICollection<ComponentDatasetSchemaDetail> ComponentDatasetSchemaDetails { get; set; } = new List<ComponentDatasetSchemaDetail>();

    [ForeignKey("Ddsid")]
    [InverseProperty("Components")]
    public virtual DesignDataSet? Dds { get; set; }

    [ForeignKey("Docid")]
    [InverseProperty("Components")]
    public virtual Document? Doc { get; set; }

    [ForeignKey("Lstid")]
    [InverseProperty("Components")]
    public virtual List? Lst { get; set; }

    [ForeignKey("Pgid")]
    [InverseProperty("Components")]
    public virtual Page Pg { get; set; } = null!;
}
