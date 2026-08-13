using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalForms.BL.Models;

public partial class CalculationAdditionalSetting
{
     
    [Key]
    [Column("CALASID")]
    public long Calasid { get; set; }
    [Column("CALCID")]
    public long Calcid { get; set; }
    public string PropName { get; set; }
    public string PropValue { get; set; }
    public string PropType { get; set; }
    public int RowNumber { get; set; }

    [ForeignKey("Calcid")]
    [InverseProperty("CalculationAdditionalSettings")]
    public virtual Calculation Calc { get; set; } = null!;
}
