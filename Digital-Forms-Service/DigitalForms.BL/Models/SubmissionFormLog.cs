using DigitalForms.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models;


public class SubmissionForm
{
    public string id { get; set; }
    public string SubmissionFormName { get; set; }
    public bool EmailSent { get; set; }
    public bool EmailHadAttachment { get; set; }
    public string EmailAddress { get; set; }
    public string TemplateId { get; set; }
    public string EmailSentOn { get; set; }
}