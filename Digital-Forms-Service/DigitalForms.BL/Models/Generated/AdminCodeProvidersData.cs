using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Models.Generated
{
    [Table("AdminCodeProvidersData")]
    public partial class AdminCodeProvidersData
    {
        [Key]
        [Column("ACPDID")]
        public long Acpdid { get; set; }

        public long Pmid { get; set; }

        [StringLength(100)]
        public string? AdminCode { get; set; }

        [ForeignKey("Pmid")]
        [InverseProperty("AdminCodeProvidersData")]
        public virtual ProviderMapping PmAC { get; set; } = null!;
    }
}
