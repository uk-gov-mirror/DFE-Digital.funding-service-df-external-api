using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("RspQstId", Name = "IX_ResponseQuestionData")]
public partial class ResponseQuestionDatum
{
    [Key]
    public long RspQstDataId { get; set; }

    public long RspQstId { get; set; }

    [StringLength(250)]
    public string? Key { get; set; }

    public string? Title { get; set; }

    [StringLength(250)]
    public string? Type { get; set; }

    public string? Answer { get; set; }

    [ForeignKey("RspQstId")]
    [InverseProperty("ResponseQuestionData")]
    public virtual ResponseQuestion RspQst { get; set; } = null!;
}
