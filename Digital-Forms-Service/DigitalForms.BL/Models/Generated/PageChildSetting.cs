using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace DigitalForms.BL.Models;

[Index("Pgid", Name = "index_pagech_pgid")]
public partial class PageChildSetting
{
    [Key]
    [Column("PGCHSTID")]
    public long Pgchstid { get; set; }

    [Column("PGID")]
    public long Pgid { get; set; }

    [StringLength(500)]
    public string? Path { get; set; }

    [StringLength(250)]
    public string? Condition { get; set; }

    [ForeignKey("Pgid")]
    [InverseProperty("PageChildSettings")]
    public virtual Page Pg { get; set; } = null!;
}
