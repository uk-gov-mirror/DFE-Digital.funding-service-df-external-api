using DigitalForms.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models;

public class Responses
{

        public string name { get; set; }
        public Question[] questions { get; set; }

        [JsonPropertyName("metadata")]
        [DataMember(Name = "metadata")]
    [JsonProperty(PropertyName = "metadata")]
    public Metadatum metadata { get; set; }

        public string formId { get; set; }
        public string id { get; set; }
        public bool isUAT { get; set; }
        public Users? user { get; set; }
        //public string _rid { get; set; }
        //public string _self { get; set; }
        //public string _etag { get; set; }
        //public string _attachments { get; set; }
        //public int _ts { get; set; }
    }


public class Metadatum
{
    public bool paymentSkipped { get; set; }
}

public class Users
{
    public Guid id { get; set; }

    public long? userid { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public Organization organization { get; set; }
}

public class Organization
{
    public string? ukprn { get; set; }
    public string? urn { get; set; }
    public string name { get; set; }
}

public class Question
    {
        public string question { get; set; }
        [JsonPropertyName("fields")]
        [DataMember(Name = "fields")]
    [JsonProperty(PropertyName = "fields")]
    public ComponentsData[] fields { get; set; }
    }

    public class ComponentsData
{
        public string? key { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
        public object? answer { get; set; }
    }


