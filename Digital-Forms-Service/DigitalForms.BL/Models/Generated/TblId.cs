using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;

[Table("Tbl_Ids")]
public partial class TblId
{
    [Key]
    public long Id { get; set; }

    [Column("Tbl_name")]
    [StringLength(500)]
    public string? TblName { get; set; }

    [Column("Tbl_nxt_identity")]
    public long? TblNxtIdentity { get; set; }
}
