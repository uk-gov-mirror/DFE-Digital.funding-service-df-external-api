using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Fid", Name = "IX_Responses")]
[Index("UserId", Name = "IX_Responses_1")]
[Index("MtdtId", Name = "IX_Responses_2")]
[Index("Id", Name = "Responses_Uniq_id", IsUnique = true)]
public partial class Response
{
    [Key]
    [Column("RSPID")]
    public long? Rspid { get; set; }

    [Column("FID")]
    [StringLength(100)]
    public string Fid { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public long? UpdatedBy { get; set; }

    public bool? ResponseStatus { get; set; }

    [StringLength(100)]
    public string Id { get; set; } = null!;

    [StringLength(2000)]
    public string? FormName { get; set; }

    public long? UserId { get; set; }

    public long? MtdtId { get; set; }

    public bool? isUAT { get; set; }

    public long? UserOrgID { get; set; }

    [ForeignKey("MtdtId")]
    [InverseProperty("Responses")]
    public virtual MetaData? Mtdt { get; set; } = null;

    [InverseProperty("Rsp")]
    public virtual ICollection<ResponseQuestion> ResponseQuestions { get; set; } = new List<ResponseQuestion>();

    [ForeignKey("UserOrgID")]
    [InverseProperty("Responses")]
    public virtual UserOrganisationDetail? UserOrganisationDetails { get; set; } = null;

    [ForeignKey("UserId")]
    [InverseProperty("Responses")]
    public virtual UserDetail? User { get; set; }

    [InverseProperty("Responses")]
    public virtual ICollection<DraftResponse> DraftResponses { get; set; } = new List<DraftResponse>();
}
