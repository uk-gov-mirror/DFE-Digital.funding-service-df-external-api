using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Models.Generated
{
    [Table("UKPRNProvidersData")]
    public partial class UKPRNProvidersData
    {
        [Key]
        [Column("UKPDID")]
        public long Ukpdid { get; set; }

        public long Pmid { get;set; }

        public long Ukprn { get; set; }

        [ForeignKey("Pmid")]
        [InverseProperty("UKPRNProvidersData")]
        public virtual ProviderMapping PmUKPRN { get; set; } = null!;
    }
}
