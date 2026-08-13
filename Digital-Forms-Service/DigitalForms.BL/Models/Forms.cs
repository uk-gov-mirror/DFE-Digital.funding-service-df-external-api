
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using DigitalForms.BL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace DigitalForms.BL.Serialized.Models;



public class FormConfiguration
{


    [JsonProperty(PropertyName = "UserId")]
    [JsonPropertyName("UserId")]
    [DataMember(Name = "UserId")]
    public string? UserId { get; set; }

    [JsonProperty(PropertyName = "CreatedBy")]
    [JsonPropertyName("CreatedBy")]
    [DataMember(Name = "CreatedBy")]
    public string? CreatedBy { get; set; }

    [JsonProperty(PropertyName = "Key")]
    [JsonPropertyName("Key")]
    [DataMember(Name = "Key")]
    public string? Key { get; set; }

    [JsonProperty(PropertyName = "Name")]
    [JsonPropertyName("Name")]
    [DataMember(Name = "Name")]
    public string? Name { get; set; }

    [JsonProperty(PropertyName = "DisplayName")]
    [JsonPropertyName("DisplayName")]
    [DataMember(Name = "DisplayName")]
    public string? DisplayName { get; set; }

    [JsonProperty(PropertyName = "LastModified")]
    [JsonPropertyName("LastModified")]
    [DataMember(Name = "LastModified")]
    public string? LastModified { get; set; }

    [JsonProperty(PropertyName = "FormStatus")]
    [JsonPropertyName("FormStatus")]
    [DataMember(Name = "FormStatus")]
    public string? FormStatus { get; set; }
    public string? lastModifiedByName { get; set; }
    public string? lastModifiedById { get; set; }
    public bool? signInRequired { get; set; }
    public bool? feedbackForm { get; set; }
    public string[] childAndDependentsForms { get; set; }

}
public class Forms
{
    public Metadata? metadata { get; set; }
    public string? startPage { get; set; }
    public Page[]? pages { get; set; }
    public List[]? lists { get; set; }
    public Section[]? sections { get; set; }
    public Condition[]? conditions { get; set; }
    public Fees[]? fees { get; set; }
    public Output[]? outputs { get; set; }
    public int? version { get; set; }
    public string? userId { get; set; }
    public string? createdBy { get; set; }
    public string? id { get; set; }
    public string? key { get; set; }
    public string? displayName { get; set; }
    public string? name { get; set; }
    public string? lastModified { get; set; }
    public string? formStatus { get; set; }
    public string? _rid { get; set; }
    public string? _self { get; set; }
    public string? _etag { get; set; }
    public string? _attachments { get; set; }
    public string? lastUpdatedByName { get; set; }
    public string? lastUpdatedById { get; set; }
    public bool? skipSummary { get; set; }
    public bool? signInRequired { get; set; }
    public string? file { get; set; }
    public Importeddataset[]? importedDataSets { get; set; }
    public Designeddataset[]? designedDataSets { get; set; }
    public Document[]? documents { get; set; }
    public Calculation[]? calculations { get; set; }
    public string? confirmationMsg { get; set; }
    public Feedback? feedback { get; set; }
    public Phasebanner? phaseBanner { get; set; }
    public string? declaration { get; set; }
    public string? lastDownloaded { get; set; }
    public Tab[]? tabs { get; set; }
    public ParentDetail? parentDetails { get; set; }
    // public string? parentId { get; set; }
    public ParentChild? parentChild { get; set; }

    public dynamic? _ts { get; set; }
    //not required to expose, commenting out
    // public long Fid { get; set; }

    public string? customSummaryMessage { get; set; }

    //public string Phasebanner { get; set; }

    //public bool feedbackForm { get; set; } 
    public string? ukprn { get; set; } = null;
    public string? pageSequence { get; set; } = null;
}
public class ParentDetail
{
    public string parentId { get; set; }

    public string parentName { get; set; }

}
public class ParentChild
{
    public string? id { get; set; }

    public bool? isMainParent { get; set; }

    public ParentChildConfig parentChildConfig { get; set; }

}

public class ParentChildConfig
{
    public string? description { get; set; }
    public string? childHeading { get; set; }
    public ChildConfiguration[] childConfigs { get; set; }

}

public class ChildConfiguration
{
    public string? childId { get; set; }
    public string? childFormName { get; set; }
    public string? childFormTitle { get; set; }
    public int? cardOrder { get; set; }
    public Dependentform[]? dependentforms { get; set; }
    public string? dateComponent { get; set; }
    public string? helpText { get; set; }
    public string? parentId { get; set; }
    public string? condition { get; set; }
    public string? conditionName { get; set; }
    public bool? isMainChild { get; set; }
}

public class Dependentform
{
    public string? id { get; set; }
    public string? status { get; set; }
    public string? name { get; set; }
    public string? title { get; set; }
}
public class Metadata
{
}

public class Fees
{
}

public class File
{
}

public class Feedback
{
    public bool? feedbackForm { get; set; }
    public string? url { get; set; }
}

public class Phasebanner
{
    public string? phase { get; set; }
}

public class Page
{
    public string? title { get; set; }
    public string? path { get; set; }
    public Component[]? components { get; set; }
    public Next[]? next { get; set; }
    public string? section { get; set; }
    public string? pageSequence { get; set; }
    public string? controller { get; set; }
}

public class Component
{
    public string? name { get; set; }
    public Options? options { get; set; }
    public string? type { get; set; }
    public Boolean? isEditingTabs { get; set; }
    public string? title { get; set; }
    public Boolean? nameHasError { get; set; }
    public Schema? schema { get; set; }
    public string? hint { get; set; }
    public Boolean? componentEdited { get; set; }
    public Boolean? _checked { get; set; }
    public string? prefixType { get; set; }
    public string? prefixValue { get; set; }
    public string? suffixValue { get; set; }
    public string? selectedDocument { get; set; }
    public string? list { get; set; }
    public Values? values { get; set; }
    public string? content { get; set; }
    public string? displayName { get; set; }
    public string? expression { get; set; }
    public string? dataset { get; set; }
    public string? fileId { get; set; }
    public string? selectTitle { get; set; }
    public bool? isBtnDisabled { get; set; }
    public int? tabsNumber { get; set; }
    public int? totalTabs { get; set; }
    public bool? reset { get; set; }
    public Tabdata[]? tabData { get; set; }
    public bool? allTabsSelected { get; set; }
    public string? tabInputType { get; set; }
    public string? paragraphVal { get; set; }
    public string? documentName { get; set; }
    public string[]? columnNames { get; set; }
    public Column[]? columns { get; set; }
    public string[]? addedFileTypes { get; set; }
    public string? calculationName { get; set; }
    public date? date { get; set; }
    public bool? addTime { get; set; }
}

public class date
{
    public bool? hideDay { get; set; }
    public bool? hideMonth { get; set; }
    public bool? hideYear { get; set; }
}

public class Options
{
    public string? autocomplete { get; set; }
    public string? classes { get; set; }
    public string? condition { get; set; }
    public string? maxDaysInPast { get; set; }
    public string? maxDaysInFuture { get; set; }
    public string? rows { get; set; }
    public string? customValidationMessage { get; set; }
    public string? prefixType { get; set; }
    public string? prefixValue { get; set; }
    public string? suffixType { get; set; }
    public bool? bold { get; set; }
    public bool? hideResultOnSummary { get; set; }
    public bool? hideResultOnPage { get; set; }
    public string? suffixValue { get; set; }

    public bool? hideTitle { get; set; }
    public bool? required { get; set; }
    public bool? optionalText { get; set; }
    public bool? hideResult { get; set; }

    public string? format { get; set; }

    public string? dateRangeStart { get; set; }
    public string? dateRangeEnd { get; set; }
}


public class Schema
{
    public dynamic? min { get; set; }
    public dynamic? max { get; set; }
    public dynamic? length { get; set; }
    public string? regex { get; set; }
    public string? precision { get; set; }
    public string? error { get; set; }
}

public class Values
{
    public string? type { get; set; }
}

public class Tabdata
{
    public string? tabLabel { get; set; }
    public string? tabHeader { get; set; }
    public string? type { get; set; }
    public string? value { get; set; }
}

public class Column
{
    public string? columnId { get; set; }
    public string? columnType { get; set; }
    public string? selectedColumnHeaderType { get; set; }
    public string? selectedColumnHeaderValue { get; set; }
    public Columnschema? columnSchema { get; set; }
    public bool? isEdited { get; set; }

    [JsonProperty(PropertyName = "ColumnOrder")]
    [JsonPropertyName("ColumnOrder")]
    [DataMember(Name = "ColumnOrder")]
    public int? ColumnOrder { get; set; }
    [JsonProperty(PropertyName = "CDSDID")]
    [JsonPropertyName("CDSDID")]
    [DataMember(Name = "CDSDID")]
    public long CDSDID { get; set; }
}

public class Columnschema
{
    public string? minNumber { get; set; }
    public string? maxNumber { get; set; }
    public string? precisionNumber { get; set; }
    public string? maxLength { get; set; }

    public string? maxDaysInPast { get; set; }
    public string? maxDaysInFuture { get; set; }
    [System.Text.Json.Serialization.JsonConverter(typeof(BoolToStringJsonConverter))]
    public string? addressRequired { get; set; }

    [JsonProperty(PropertyName = "CDSPDID")]
    [JsonPropertyName("CDSPDID")]
    [DataMember(Name = "CDSPDID")]
    public long? CDSPDID { get; set; }
}
public class BoolToStringJsonConverter : System.Text.Json.Serialization.JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString() ?? String.Empty;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var stringValue = reader.GetDouble();
            return stringValue.ToString();
        }
        else if (reader.TokenType == JsonTokenType.False ||
            reader.TokenType == JsonTokenType.True)
        {
            return reader.GetBoolean().ToString();
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Skip();
            return "(not supported)";
        }
        else
        {
            Console.WriteLine($"Unsupported token type: {reader.TokenType}");

            throw new System.Text.Json.JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}

public class Next
{
    public string? path { get; set; }
    public string? condition { get; set; }
}

public class List
{
    public string? title { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
    public string? dataset { get; set; }
    public Item[]? items { get; set; }
}

public class Item
{
    public string? text { get; set; }
    public dynamic? value { get; set; }
    public string? description { get; set; }
    public string? condition { get; set; }
    public string? links { get; set; }

    [JsonProperty(PropertyName = "Order")]
    [JsonPropertyName("Order")]
    [DataMember(Name = "Order")]
    public string? Order { get; set; }
}

public class Section
{
    public string? name { get; set; }
    public string? title { get; set; }
    public string? conditionComp { get; set; }
    public string? numberComp { get; set; }
    public string? triggerCompValue { get; set; }

    private bool? _repeatableSection;

    public bool repeatableSection
    {
        get => _repeatableSection ?? false;
        set => _repeatableSection = value;
    }
}

public class Condition
{
    public string? displayName { get; set; }
    public string? name { get; set; }
    public Value? value { get; set; }
}

public class Value
{
    public string? name { get; set; }
    public Condition1[]? conditions { get; set; }
}

public class Condition1
{
    public string? coordinator { get; set; }
    public Field? field { get; set; }

    [JsonProperty(PropertyName = "operator")]
    [JsonPropertyName("operator")]
    [DataMember(Name = "operator")]
    public string? _operator { get; set; }
    public string? conditionType { get; set; }
    public string? datasetId { get; set; }
    public Value1? value { get; set; }
}

public class Field
{
    public string? name { get; set; }
    public string? type { get; set; }
    public string? display { get; set; }
}

public class Value1
{
    public string? type { get; set; }
    public string? value { get; set; }
    public string? display { get; set; }
}

public class Output
{
    public string? name { get; set; }
    public string? title { get; set; }
    public string? type { get; set; }
    public Outputconfiguration? outputConfiguration { get; set; }
}

public class Outputconfiguration
{
    public string? emailAddress { get; set; }
    public string[]? personalisation { get; set; }
    public string? templateId { get; set; }
    public string? apiKey { get; set; }
    public string? emailField { get; set; }
    public bool? addReferencesToPersonalisation { get; set; }
}

public class Importeddataset
{
    public string? fileTitle { get; set; }
    public string? fileName { get; set; }
    public DateTime? uploadedDate { get; set; }
    public string? fileId { get; set; }
}

public class Designeddataset
{
    public string? id { get; set; }
    public string? title { get; set; }
    public DateTime? uploadedDate { get; set; }
    public string? csvUsed { get; set; }
    public string? keyIdentifier { get; set; }
    public Datum[][] data { get; set; }
}

public class Datum
{
    public string? index { get; set; }
    public string? type { get; set; }
    public string? value { get; set; }
    public bool? bold { get; set; }
    public bool? numeric { get; set; }
    public string? format { get; set; }
    public string? designedDataSetId { get; set; }
    public bool? calc { get; set; }
    [JsonPropertyName("checked")]
    [JsonProperty(PropertyName = "checked")]
    [DataMember(Name = "checked")]
    public bool? _checked { get; set; }

}
public class ComputeList
{
    public string? id { get; set; }
    public string? type { get; set; }
    public int? order { get; set; }
    public string? value { get; set; }
    public string? entity { get; set; } 
}

public class Document
{
    public string? id { get; set; }
    public string? title { get; set; }
    public DateTime? uploadedDate { get; set; }
    public string? type { get; set; }
    public string? fileName { get; set; }
    public string? path { get; set; }
}

public class Calculation
{
    public string? displayName { get; set; }
    public string? hint { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? pageLocation { get; set; }
    public Component[]? components { get; set; }
    public string? expression { get; set; }
    public string? title { get; set; }
    public bool? hideResult { get; set; }
    public bool? repeatable { get; set; }
    public Datum[]? datasets { get; set; }
    public ComputeList[]? computeList { get; set; }
    public string[]? calculationsMapped { get; set; }
}

public class Tab
{
    public string? id { get; set; }
    public Tabdata[]? tabData { get; set; }
}