using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_docs_fid")]
public partial class Document
{
    [Key]
    [Column("DOCID")]
    public long Docid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(250)]
    public string? FileTitle { get; set; }

    [StringLength(250)]
    public string? FileName { get; set; }

    [StringLength(250)]
    public string? FilePath { get; set; }

    [StringLength(100)]
    public string? FileType { get; set; }

    [StringLength(100)]
    public string? FileId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadedOn { get; set; }

    [InverseProperty("Doc")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [InverseProperty("Doc")]
    public virtual ICollection<DesignDataSet> DesignDataSets { get; set; } = new List<DesignDataSet>();

    [ForeignKey("Fid")]
    [InverseProperty("Documents")]
    public virtual Form FidNavigation { get; set; } = null!;
}
