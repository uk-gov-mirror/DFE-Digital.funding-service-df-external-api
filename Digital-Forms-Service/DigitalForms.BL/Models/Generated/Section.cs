using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_sec_fid")]
public partial class Section
{
    [Key]
    [Column("SCID")]
    public long Scid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [Column("SCName")]
    [StringLength(250)]
    public string? Scname { get; set; }

    [Column("SCTitle")]
    [StringLength(250)]
    public string? Sctitle { get; set; }

    [Column("Scnumbercomp")]
    [StringLength(250)]
    public string? Scnumbercomp { get; set; }

    [Column("Scconditioncomp")]
    [StringLength(250)]
    public string? Scconditioncomp { get; set; }

    public bool? repeatableSection { get; set; }

    [ForeignKey("Fid")]
    [InverseProperty("Sections")]
    public virtual Form FidNavigation { get; set; } = null!;
}
