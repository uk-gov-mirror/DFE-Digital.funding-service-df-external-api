using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Tabid", Name = "index_tabchdtl_tabid")]
public partial class TabChildDetail
{
    [Key]
    [Column("TABCID")]
    public long Tabcid { get; set; }

    [Column("CADSTID")]
    public long? Cadstid { get; set; }

    [Column("TABID")]
    public long Tabid { get; set; }

    [Column("tabLabel")]
    [StringLength(250)]
    public string? TabLabel { get; set; }

    [Column("tabHeader")]
    [StringLength(250)]
    public string? TabHeader { get; set; }

    [Column("type")]
    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(500)]
    public string? Value { get; set; }

    [ForeignKey("Cadstid")]
    [InverseProperty("TabChildDetails")]
    public virtual ComponentAdditionalSetting? Cadst { get; set; }

    [ForeignKey("Tabid")]
    [InverseProperty("TabChildDetails")]
    public virtual TabDetail Tab { get; set; } = null!;
}
