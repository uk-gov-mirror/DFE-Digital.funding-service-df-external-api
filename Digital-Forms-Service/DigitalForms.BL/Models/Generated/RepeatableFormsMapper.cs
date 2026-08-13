using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalForms.BL.Models;
[Table("RepeatableFormsMapper")]
public partial class RepeatableFormsMapper
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RFMID { get; set; }
    public long RFID { get; set; }
    public long RFSID { get; set; }

    [ForeignKey("RFSID")]
    [InverseProperty("RepeatableFormsMapper")]
    public virtual RepeatableSectionsData RepeatableSectionsDatum { get; set; } = null!;

    [ForeignKey("RFID")]
    [InverseProperty("RepeatableFormsMapper")]
    public virtual RepeatableFormsData RepeatableFormsDatum { get; set; } = null!;
}