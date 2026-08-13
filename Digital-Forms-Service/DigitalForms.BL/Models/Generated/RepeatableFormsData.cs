namespace DigitalForms.BL.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("RepeatableFormsData")]
public partial class RepeatableFormsData
{
    [Key]
    public long RFID { get; set; }
    public string ID { get; set; }
    public byte[] FormData { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool? Status { get; set; }

    [InverseProperty("RepeatableFormsDatum")]
    public virtual ICollection<RepeatableFormsMapper> RepeatableFormsMapper { get; set; } = new List<RepeatableFormsMapper>();
}