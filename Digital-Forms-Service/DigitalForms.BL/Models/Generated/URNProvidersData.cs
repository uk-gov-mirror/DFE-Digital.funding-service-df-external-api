using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Models.Generated
{
    [Table("URNProvidersData")]
    public partial class URNProvidersData
    {
        [Key]
        [Column("URPDID")]
        public long Urpdid { get; set; }

        public long Pmid { get; set; }

        public long Urn { get; set; }

        [ForeignKey("Pmid")]
        [InverseProperty("URNProvidersData")]
        public virtual ProviderMapping PmURN { get; set; } = null!;
    }
}
