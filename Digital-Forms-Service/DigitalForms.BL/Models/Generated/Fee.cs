using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_fees_fid")]
public partial class Fee
{
    [Key]
    [Column("FEID")]
    public long Feid { get; set; }

    [Column("FID")]
    public long? Fid { get; set; }

    [StringLength(250)]
    public string? Desciption { get; set; }

    [ForeignKey("Fid")]
    [InverseProperty("Fees")]
    public virtual Form? FidNavigation { get; set; }
}
