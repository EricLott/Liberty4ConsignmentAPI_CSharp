using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyConsignmentAPI
{
    public partial class ClientResponse
    {
        [JsonProperty("response")]
        public ClientResponseResponse Response { get; set; }
    }

    public partial class ClientResponseResponse
    {
        [JsonProperty("basic_client")]
        public Client Client { get; set; }
    }

    public partial class AddClientClass
    {
        [JsonProperty("basic_client")]
        public Client Client { get; set; }
    }

    public partial class ClientResponse
    {
        public static ClientResponse FromJson(string json) => JsonConvert.DeserializeObject<ClientResponse>(json, ClientConverter.Settings);
    }

    public partial class Client
    {
        [JsonProperty("clientid")]
        public long Clientid { get; set; }

        [JsonProperty("account")]
        public long Account { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("tax_exempt")]
        public long TaxExempt { get; set; }

        [JsonProperty("tax_exempt_no")]
        public string TaxExemptNo { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("birth_month")]
        public long BirthMonth { get; set; }

        [JsonProperty("client_type_id")]
        public int ClientTypeID { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2")]
        public string Line2 { get; set; }

        [JsonProperty("line3")]
        public string Line3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        [JsonConverter(typeof(ClientParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("primary")]
        public long Primary { get; set; }
    }

    public partial class Client
    {
        public static Client FromJson(string json) => JsonConvert.DeserializeObject<Client>(json, ClientConverter.Settings);
    }

    public static class ClientSerialize
    {
        public static string ToJson(this AddClientClass self) => JsonConvert.SerializeObject(self, ClientConverter.Settings);
    }

    internal static class ClientConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Default,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ClientParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());

            return;
        }

        public static readonly ClientParseStringConverter Singleton = new ClientParseStringConverter();
    }
}
