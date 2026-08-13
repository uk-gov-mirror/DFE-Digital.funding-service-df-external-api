
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;
[Table("DependentForms")]

public class DependentForm
{
    [Key]
    [Column("DFID")]
    public long Dfid { get; set; }
    [Column("CCID")]
    public long Ccid { get; set; }
    [Column("FormId")]
    public string? FormId { get; set; }
    [Column("DependentStatus")]
    public string? DependentStatus { get; set; }
    [Column("MainParentId")]
    public string? MainParentId { get; set; }
    //[Column("FormName")]
    //public string? FormName { get; set; }

    [ForeignKey("Ccid")]
    [InverseProperty("DependentForms")]
    public virtual ChildConfig DFChildConfigs { get; set; } = null!;
}


