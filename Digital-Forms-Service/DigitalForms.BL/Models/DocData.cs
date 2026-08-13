using DigitalForms.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models;


public class DocData
{
    public string?  DCDID { get; set; }
    public string fileName { get; set; }
    public string filePath { get; set; }
    public string fileStatus { get; set; }
    public string sourceSystem { get; set; }
    public string scanStatus { get; set; }

    public string fileId { get; set; }

}