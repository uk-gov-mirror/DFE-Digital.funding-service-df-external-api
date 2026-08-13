using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("FormId", Name = "index_forms_formid")]
public partial class Form
{
    [Key]
    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(100)]
    public string? FormId { get; set; }

    [StringLength(250)]
    public string? Displayname { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    [Column("FSID")]
    public long Fsid { get; set; }

    public int? Version { get; set; }

    public bool? SkipSummary { get; set; }

    public bool? SignInRequired { get; set; }

    public long? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? LastUpdatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedOn { get; set; }

    public bool? Status { get; set; }

    [StringLength(250)]
    public string? File { get; set; }

    [StringLength(250)]
    public string? StartPage { get; set; }

    [StringLength(2500)]
    public string? ConfirmationMsg { get; set; }

    [StringLength(1000)]
    public string? Declaration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastDownloaded { get; set; }

    [StringLength(250)]
    public string? PhaseBanner { get; set; }

    public bool? FeedbackForm { get; set; }

    [StringLength(250)]
    public string? Url { get; set; }

    public string? CustomSummaryMessage { get; set; }

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Calculation> Calculations { get; set; } = new List<Calculation>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();

    [ForeignKey("CreatedByUserId")]
    [InverseProperty("FormCreatedByUsers")]
    public virtual UserDetail? CreatedByUser { get; set; }

    [InverseProperty("FidNavigation")]
    public virtual ICollection<DesignDataSet> DesignDataSets { get; set; } = new List<DesignDataSet>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Fee> Fees { get; set; } = new List<Fee>();

    [ForeignKey("Fsid")]
    [InverseProperty("Forms")]
    public virtual FormStatus Fs { get; set; } = null!;

    [ForeignKey("LastUpdatedByUserId")]
    [InverseProperty("FormLastUpdatedByUsers")]
    public virtual UserDetail? LastUpdatedByUser { get; set; }

    [InverseProperty("FidNavigation")]
    public virtual ICollection<List> Lists { get; set; } = new List<List>();

    [InverseProperty("FidNavigation")]
    public virtual MetaData MetaData { get; set; } = new MetaData();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Output> Outputs { get; set; } = new List<Output>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    [InverseProperty("FidNavigation")]
    public virtual ICollection<TabDetail> TabDetails { get; set; } = new List<TabDetail>();

    [InverseProperty("FidNavigation")]
    public virtual ParentChild? ParentChild { get; set; }
}
