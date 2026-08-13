using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Table("DesignDataSet")]
[Index("Fid", Name = "index_designdatast_fid")]
public partial class DesignDataSet
{
    [Key]
    [Column("DDSID")]
    public long Ddsid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [Column("DOCID")]
    public long? Docid { get; set; }

    [Column("DDSetID")]
    [StringLength(250)]
    public string? DdsetId { get; set; }

    [StringLength(250)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? KeyIdentifier { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadedOn { get; set; }

    [Column("csvUsed")]
    [StringLength(250)]
    public string? CsvUsed { get; set; }

    [InverseProperty("Dds")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [InverseProperty("Dds")]
    public virtual ICollection<DesignDataSetDetail> DesignDataSetDetails { get; set; } = new List<DesignDataSetDetail>();

    [ForeignKey("Docid")]
    [InverseProperty("DesignDataSets")]
    public virtual Document? Doc { get; set; } 

    [ForeignKey("Fid")]
    [InverseProperty("DesignDataSets")]
    public virtual Form FidNavigation { get; set; } = null!;
}
