
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;
[Table("ChildConfigs")]

public class ChildConfig
{
    [Key]
    [Column("CCID")]
    public long Ccid { get; set; }
    [Column("PCCID")]
    public long Pccid { get; set; }
    [Column("ChildId")]
    public string? ChildId { get; set; } 
    [Column("CardOrder")]
    public int? CardOrder { get; set; }
    [Column("IsDependentForms")]
    public bool? IsDependentForms { get; set; }
    [Column("DateComponent")]
    public string? DateComponent { get; set; }
    [Column("HelpText")]
    public string? HelpText { get; set; }
    [Column("ParentId")]
    public string? ParentId { get; set; }
    [Column("Condition")]
    public string? Condition { get; set; }
    [Column("ConditionName")]
    public string? ConditionName { get; set; }
    [Column("IsMainChild")]
    public bool? IsMainChild { get; set; }

    [InverseProperty("DFChildConfigs")]
    public virtual ICollection<DependentForm> DependentForms { get; set; } = new List<DependentForm>();
    [ForeignKey("Pccid")]
    [InverseProperty("ChildConfigs")]
    public virtual ParentChildConfig CCParentChildConfig { get; set; } = null!;
}


