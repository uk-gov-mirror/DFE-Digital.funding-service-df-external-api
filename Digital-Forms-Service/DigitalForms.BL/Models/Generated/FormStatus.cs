using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;

[Table("FormStatus")]
public partial class FormStatus
{
    [Key]
    [Column("FSID")]
    public long Fsid { get; set; }

    [StringLength(250)]
    public string? Status { get; set; }

    [InverseProperty("Fs")]
    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
