using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;

[Table("providersmappingjson")]
public partial class Providersmappingjson
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("data")]
    public string? Data { get; set; }
}
