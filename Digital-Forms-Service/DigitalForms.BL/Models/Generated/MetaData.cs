using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_metadat_fid")]
public partial class MetaData
{
    [Key]
    [Column("MTDTID")]
    public long Mtdtid { get; set; }

    [Column("FID")]
    public long? Fid { get; set; }

    [StringLength(250)]
    public string? PropName { get; set; }

    [StringLength(250)]
    public string? PropValue { get; set; }

    [StringLength(250)]
    public string? PropType { get; set; }

    [ForeignKey("Fid")]
    [InverseProperty("MetaData")]
    public virtual Form? FidNavigation { get; set; }

    [InverseProperty("Mtdt")]
    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
