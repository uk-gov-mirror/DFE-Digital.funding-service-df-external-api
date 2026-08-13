using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Table("List")]
[Index("Fid", Name = "index_list_fid")]
public partial class List
{
    [Key]
    [Column("LSTID")]
    public long Lstid { get; set; }

    [Column("FID")]
    public long Fid { get; set; }

    [Column("LSTTitle")]
    [StringLength(250)]
    public string? Lsttitle { get; set; }

    [Column("LSTName")]
    [StringLength(250)]
    public string? Lstname { get; set; }

    [Column("LSTType")]
    [StringLength(250)]
    public string? Lsttype { get; set; }

    [Column("LSTDataset")]
    [StringLength(100)]
    public string? Lstdataset { get; set; }

    [InverseProperty("Lst")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [ForeignKey("Fid")]
    [InverseProperty("Lists")]
    public virtual Form FidNavigation { get; set; } = null!;

    [InverseProperty("Lst")]
    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();
}
