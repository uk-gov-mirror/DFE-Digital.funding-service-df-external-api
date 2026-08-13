

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalForms.BL.Models;
[Table("RepeatableSectionsData")]
public partial class RepeatableSectionsData
{
    [Key]
    public long RFSID { get; set; }
    public string FID { get; set; } 
    public string SectionName { get; set; }
    public int  NoofRepeats { get; set; }    
    public bool Status { get; set; }    
    public byte[] SectionData { get; set; }

    [InverseProperty("RepeatableSectionsDatum")]
    public virtual ICollection<RepeatableFormsMapper> RepeatableFormsMapper { get; set; } = new List<RepeatableFormsMapper>();
}
