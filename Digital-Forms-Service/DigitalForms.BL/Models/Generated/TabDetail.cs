using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_tabs_fid")]
public partial class TabDetail
{
    [Key]
    [Column("TABID")]
    public long Tabid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(250)]
    public string? TabName { get; set; }

    [ForeignKey("Fid")]
    [InverseProperty("TabDetails")]
    public virtual Form FidNavigation { get; set; } = null!;

    [InverseProperty("Tab")]
    public virtual ICollection<TabChildDetail> TabChildDetails { get; set; } = new List<TabChildDetail>();
}
