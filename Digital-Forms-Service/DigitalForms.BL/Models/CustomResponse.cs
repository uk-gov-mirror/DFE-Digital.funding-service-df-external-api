using DigitalForms.BL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DigitalForms.BL.Serialized.Models
{

    public class CustomResponse
    {
        [JsonPropertyName("Rspid")]
        [DataMember(Name = "Rspid")]
        [JsonProperty(PropertyName = "Rspid")]
        public long? Rspid { get; set; }

        [JsonPropertyName("Fid")]
        [DataMember(Name = "Fid")]
        [JsonProperty(PropertyName = "Fid")]
        public string Fid { get; set; } = null!;

        [JsonPropertyName("UpdatedOn")]
        [DataMember(Name = "UpdatedOn")]
        [JsonProperty(PropertyName = "UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        [JsonPropertyName("UpdatedBy")]
        [DataMember(Name = "UpdatedBy")]
        [JsonProperty(PropertyName = "UpdatedBy")]
        public long? UpdatedBy { get; set; }

        [JsonPropertyName("ResponseStatus")]
        [DataMember(Name = "ResponseStatus")]
        [JsonProperty(PropertyName = "ResponseStatus")]
        public bool? ResponseStatus { get; set; }

        [JsonPropertyName("Id")]
        [DataMember(Name = "Id")]
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("FormName")]
        [DataMember(Name = "FormName")]
        [JsonProperty(PropertyName = "FormName")]
        public string? FormName { get; set; }

        [JsonPropertyName("UserId")]
        [DataMember(Name = "UserId")]
        [JsonProperty(PropertyName = "UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("MtdtId")]
        [DataMember(Name = "MtdtId")]
        [JsonProperty(PropertyName = "MtdtId")]
        public long? MtdtId { get; set; }

        [JsonPropertyName("isUAT")]
        [DataMember(Name = "isUAT")]
        [JsonProperty(PropertyName = "isUAT")]
        public bool? isUAT { get; set; }

        [JsonPropertyName("UserDetail")]
        [DataMember(Name = "UserDetail")]
        [JsonProperty(PropertyName = "UserDetail")]
        public CustomUserDetail UserDetail { get; set; } = new CustomUserDetail();
    }

    public class CustomUserDetail
    {

        [JsonPropertyName("Uid")]
        [DataMember(Name = "Uid")]
        [JsonProperty(PropertyName = "Uid")]
        public long Uid { get; set; }

        [JsonPropertyName("UserId")]
        [DataMember(Name = "UserId")]
        [JsonProperty(PropertyName = "UserId")]
        public Guid UserId { get; set; }


        [JsonPropertyName("Name")]
        [DataMember(Name = "Name")]
        [JsonProperty(PropertyName = "Name")]
        public string? Name { get; set; }


        [JsonPropertyName("Email")]
        [DataMember(Name = "Email")]
        [JsonProperty(PropertyName = "Email")]
        public string? Email { get; set; }


        [JsonPropertyName("Status")]
        [DataMember(Name = "Status")]
        [JsonProperty(PropertyName = "Status")]
        public string? Status { get; set; }


        [JsonPropertyName("OrganisationDetails")]
        [DataMember(Name = "OrganisationDetails")]
        [JsonProperty(PropertyName = "OrganisationDetails")]
        public CustomOrganisationDetail OrganisationDetails { get; set; }  
    }

    public class CustomOrganisationDetail
    {


        [JsonPropertyName("Orgid")]
        [DataMember(Name = "Orgid")]
        [JsonProperty(PropertyName = "Orgid")]
        public long Orgid { get; set; }


        [JsonPropertyName("Uid")]
        [DataMember(Name = "Uid")]
        [JsonProperty(PropertyName = "Uid")]
        public long? Uid { get; set; }


        [JsonPropertyName("Ukprn")]
        [DataMember(Name = "Ukprn")]
        [JsonProperty(PropertyName = "Ukprn")]
        public string? Ukprn { get; set; }


        [JsonPropertyName("Urn")]
        [DataMember(Name = "Urn")]
        [JsonProperty(PropertyName = "Urn")]
        public long? Urn { get; set; }


        [JsonPropertyName("AdminCode")]
        [DataMember(Name = "AdminCode")]
        [JsonProperty(PropertyName = "AdminCode")]
        public string? AdminCode { get; set; }


        [JsonPropertyName("Name")]
        [DataMember(Name = "Name")]
        [JsonProperty(PropertyName = "Name")]
        public string? Name { get; set; }
    }
    public class CustomResponseQuestion
    {

        [JsonPropertyName("RspQstId")]
        [DataMember(Name = "RspQstId")]
        [JsonProperty(PropertyName = "RspQstId")]
        public long RspQstId { get; set; }

        [JsonPropertyName("RspId")]
        [DataMember(Name = "RspId")]
        [JsonProperty(PropertyName = "RspId")]
        public long RspId { get; set; }

        [JsonPropertyName("Question")]
        [DataMember(Name = "Question")]
        [JsonProperty(PropertyName = "Question")]
        public string? Question { get; set; }

    }

    public class CustomResponseQuestionDatum
    {
        [JsonPropertyName("RspQstDataId")]
        [DataMember(Name = "RspQstDataId")]
        [JsonProperty(PropertyName = "RspQstDataId")]
        public long RspQstDataId { get; set; }

        [JsonPropertyName("RspQstId")]
        [DataMember(Name = "RspQstId")]
        [JsonProperty(PropertyName = "RspQstId")]
        public long RspQstId { get; set; }

        [JsonPropertyName("Key")]
        [DataMember(Name = "Key")]
        [JsonProperty(PropertyName = "Key")]
        [StringLength(250)]
        public string? Key { get; set; }

        [JsonPropertyName("Title")]
        [DataMember(Name = "Title")]
        [JsonProperty(PropertyName = "Title")]
        public string? Title { get; set; }

        [JsonPropertyName("Type")]
        [DataMember(Name = "Type")]
        [JsonProperty(PropertyName = "Type")]
        public string? Type { get; set; }

        [JsonPropertyName("Answer")]
        [DataMember(Name = "Answer")]
        [JsonProperty(PropertyName = "Answer")]
        public string? Answer { get; set; }


    }
}
