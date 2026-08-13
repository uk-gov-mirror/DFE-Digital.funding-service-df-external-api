using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalForms.BL.Serialized.Models
{
    public class ProviderMappings
    {
        public string id { get; set; }
        public ProvidersData providers { get; set; }
        public string date {  get; set; }
        //public string _rid { get; set; }  
        //public string _self { get; set; }  
        //public string _etag { get; set; }  
        //public string _attachments { get; set; }  
        //public int _ts { get; set; }  
    }

    public class ProvidersData
    {
        public int[]? UKPRN { get; set; }
        public int[]? URN { get; set; }
        [JsonPropertyName("adminCode")]
        [DataMember(Name = "adminCode")]
        [JsonProperty(PropertyName = "adminCode")]
        public string[]? AdminCode { get; set; }
    }

    public class ProviderMappingsPayload
    {
        public string id { get; set; }
        public string providers { get; set; }
        public string date { get; set; }
        //public string _rid { get; set; }  
        //public string _self { get; set; }  
        //public string _etag { get; set; }  
        //public string _attachments { get; set; }  
        //public int _ts { get; set; }  
    }

    public class CsvProviderRecord
    {
        public int? establishment_URN { get; set; }
        public int? establishment_UKPRN { get; set; }
        public string? district_administrative_code { get; set; }
    }

    public static class ProviderMappingsConverter
    {
        public static ProviderMappings ConvertPayloadToMapping(ProviderMappingsPayload payload)
        {
            var providerItems = payload.providers.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var numericValues = providerItems
                .Where(item => item.All(char.IsDigit))
                .Select(int.Parse)
                .Distinct()
                .ToArray();

            var alphanumericValues = providerItems
                .Where(item => !item.All(char.IsDigit))
                .Distinct()
                .ToArray();

            var providersData = new ProvidersData
            {
                UKPRN = numericValues,
                AdminCode = alphanumericValues,
                URN = Array.Empty<int>()
            };

            return new ProviderMappings
            {
                id = payload.id,
                providers = providersData,
                date = payload.date
            };

        }
    }
}
