using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_pages_fid")]
public partial class Page
{
    [Key]
    [Column("PGID")]
    public long Pgid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(500)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Path { get; set; }

    [StringLength(250)]
    public string? Controller { get; set; }

    [StringLength(500)]
    public string? Next { get; set; }

    [StringLength(100)]
    public string? Section { get; set; }

    [InverseProperty("Pg")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [ForeignKey("Fid")]
    [InverseProperty("Pages")]
    public virtual Form FidNavigation { get; set; } = null!;

    [InverseProperty("Pg")]
    public virtual ICollection<PageChildSetting> PageChildSettings { get; set; } = new List<PageChildSetting>();
}
