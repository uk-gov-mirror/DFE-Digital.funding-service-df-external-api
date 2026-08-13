
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;
[Table("ParentChildConfig")]

public class ParentChildConfig
{
    [Key]
    [Column("PCCID")]
    public long Pccid { get; set; }
    [Column("PCID")]
    public long Pcid { get; set; }
    [Column("Description")]
    public string? Description { get; set; }
    [Column("ChildHeading")]
    public string? ChildHeading { get; set; }
    [Column("IsChildConfigs")]
    public bool? IsChildConfigs { get; set; }
    [InverseProperty("CCParentChildConfig")]
    public virtual ICollection<ChildConfig> ChildConfigs { get; set; } = new List<ChildConfig>();
    [ForeignKey("Pcid")]
    [InverseProperty("ParentChildConfig")]
    public virtual ParentChild PCCParentChild { get; set; } = null!;
}


