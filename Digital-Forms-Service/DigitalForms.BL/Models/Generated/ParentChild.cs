
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;
[Table("ParentChild")]

public class ParentChild
{
    [Key]
    [Column("PCID")]
    public long Pcid { get; set; }
    [Column("FID")]
    public long Fid { get; set; }
    [Column("IsMainParent")]
    public bool? IsMainParent { get; set; }
    [Column("IsParentChildConfig")]
    public bool? IsParentChildConfig { get; set; }
    [StringLength(250)]
    public string? Name { get; set; }
    [InverseProperty("PCCParentChild")]
    public virtual ParentChildConfig ParentChildConfig { get; set; } = new ParentChildConfig();
    [ForeignKey("Fid")]
    [InverseProperty("ParentChild")]
    public virtual Form FidNavigation { get; set; } = null!;
}


