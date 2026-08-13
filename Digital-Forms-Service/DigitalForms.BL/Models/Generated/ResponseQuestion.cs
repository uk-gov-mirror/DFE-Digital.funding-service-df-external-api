using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("RspId", Name = "IX_ResponseQuestions")]
public partial class ResponseQuestion
{
    [Key]
    public long RspQstId { get; set; }

    public long RspId { get; set; }

    public string? Question { get; set; }

    [InverseProperty("RspQst")]
    public virtual ICollection<ResponseQuestionDatum> ResponseQuestionData { get; set; } = new List<ResponseQuestionDatum>();

    [ForeignKey("RspId")]
    [InverseProperty("ResponseQuestions")]
    public virtual Response Rsp { get; set; } = null!;
}
