using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models
{

    public class ParentChildUpdate
    {
        public string tableName { get; set; }
        public string formId { get; set; }
        public Dictionary<string, object> FieldChanges { get; set; }
    }


}