using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;

public partial class UserDetail
{
    [Key]
    [Column("UID")]
    public long Uid { get; set; }

    public Guid UserId { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    [StringLength(250)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<Form> FormCreatedByUsers { get; set; } = new List<Form>();

    [InverseProperty("LastUpdatedByUser")]
    public virtual ICollection<Form> FormLastUpdatedByUsers { get; set; } = new List<Form>();

    [InverseProperty("User")]
    public virtual ICollection<UserOrganisationDetail> UserOrganisationDetails { get; set; } = new List<UserOrganisationDetail>();


    [InverseProperty("User")]
    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
