using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Calcid", Name = "index_calccomps_calcid")]
[Index("Cmpid", Name = "index_calccomps_cmpid")]
public partial class CalculationComponentDetail
{
    [Key]
    [Column("CALCOID")]
    public long Calcoid { get; set; }

    [Column("CALCID")]
    public long Calcid { get; set; }

    [Column("CMPID")]
    public long Cmpid { get; set; }

    [ForeignKey("Calcid")]
    [InverseProperty("CalculationComponentDetails")]
    public virtual Calculation Calc { get; set; } = null!;

    [ForeignKey("Cmpid")]
    [InverseProperty("CalculationComponentDetails")]
    public virtual Component Cmp { get; set; } = null!;
}
