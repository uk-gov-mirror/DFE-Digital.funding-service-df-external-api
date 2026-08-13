using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_outputs_fid")]
public partial class Output
{
    [Key]
    [Column("OUID")]
    public long Ouid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    [StringLength(250)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [ForeignKey("Fid")]
    [InverseProperty("Outputs")]
    public virtual Form FidNavigation { get; set; } = null!;

    [InverseProperty("Ou")]
    public virtual ICollection<OutputDetail> OutputDetails { get; set; } = new List<OutputDetail>();

    [InverseProperty("Outputs")]
    public virtual ICollection<DraftResponse> DraftResponses { get; set; } = new List<DraftResponse>();
}
