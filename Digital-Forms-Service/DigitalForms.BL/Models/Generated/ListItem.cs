using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace DigitalForms.BL.Models;

[Index("Lstid", Name = "index_listitem_lstid")]
public partial class ListItem
{
    [Key]
    [Column("LSTIItemD")]
    public long Lstitemid { get; set; }

    [Column("LSTID")]
    public long Lstid { get; set; }

    [Column("LSTItemsText")]
    [StringLength(500)]
    public string? LstitemsText { get; set; }

    [Column("LSTItemValue")]
    [StringLength(500)]
    public string? LstitemValue { get; set; }

    [Column("LSTItemCondition")]
    [StringLength(250)]
    public string? LstitemCondition { get; set; }

    [Column("LSTItemDesc")]
    [StringLength(2000)]
    public string? LstitemDesc { get; set; }


    [Column("LSTItemLinks")]
    public string? Lstitemlinks { get; set; }
    public int? LSTOrder { get; set; }

    [ForeignKey("Lstid")]
    [InverseProperty("ListItems")]
    public virtual List Lst { get; set; } = null!;
}
