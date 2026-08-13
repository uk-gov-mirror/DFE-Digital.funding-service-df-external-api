using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "index_calcs_fid")]
public partial class Calculation
{
    [Key]
    [Column("CALCID")]
    public long Calcid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [StringLength(250)]
    public string? DisplayName { get; set; }

    [StringLength(250)]
    public string? Hint { get; set; }

    [StringLength(250)]
    public string? PageLocation { get; set; }

    [StringLength(250)]
    public string? Type { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    public bool? HideResult { get; set; }
    public bool? Repeatable { get; set; }

    [StringLength(250)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? Expressions { get; set; }

    [InverseProperty("Calc")]
    public virtual ICollection<CalculationComponentDetail> CalculationComponentDetails { get; set; } = new List<CalculationComponentDetail>();
    [InverseProperty("Calc")]
    public virtual ICollection<CalculationAdditionalSetting> CalculationAdditionalSettings { get; set; } = new List<CalculationAdditionalSetting>();

    [ForeignKey("Fid")]
    [InverseProperty("Calculations")]
    public virtual Form FidNavigation { get; set; } = null!;
}
