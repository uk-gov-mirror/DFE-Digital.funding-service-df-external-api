using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;
[Table("SubmissionFormLog")]
public partial class SubmissionFormLog
{
    [Key]
    public long SFid { get; set; }
    public string id { get; set; }
    public string SubmissionFormName { get; set; }
    public bool EmailSent { get; set; }
    public bool EmailHadAttachment { get; set; }
    public string EmailAddress { get; set; }
    public string TemplateId { get; set; }
    public string EmailSentOn { get; set; }
    public DateTime Createdon { get; set; }


}
