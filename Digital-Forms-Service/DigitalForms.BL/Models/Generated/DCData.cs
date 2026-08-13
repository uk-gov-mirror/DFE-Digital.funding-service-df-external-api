using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Models;
[Table("DCData")]
public partial class DCData
{
    [Key]
    public long?   DCDID { get; set; }
    public string fileName { get; set; }
    public string filePath { get; set; }
    public string fileStatus { get; set; }
    public string sourceSystem { get; set; }
    public string scanStatus { get; set; }

    public string fileId { get; set; }


}


