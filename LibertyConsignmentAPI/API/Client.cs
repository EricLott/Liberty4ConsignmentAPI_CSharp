using LibertyConsignmentAPI.Converters;
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
    public partial class GetClientResponse
    {
        [JsonProperty("response")]
        public GetClientResponseObject Response { get; set; }
    }

    public partial class GetClientResponseObject
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("clients")]
        public SearchClientResponseClients[] Clients { get; set; }
    }

    public partial class SearchClientResponseClients
    {
        [JsonProperty("search_client")]
        public SearchClient SearchClient { get; set; }
    }

    public partial class SearchClient
    {
        [JsonProperty("counter")]
        public long Counter { get; set; }

        [JsonProperty("clientid")]
        public long Clientid { get; set; }

        [JsonProperty("account")]
        public long Account { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("userfield")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Userfield { get; set; }
    }

    public partial class ClientResponse
    {
        [JsonProperty("response")]
        public ClientResponseObject Response { get; set; }
    }

    public partial class ClientResponseObject
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
        public static ClientResponse FromJson(string json) => JsonConvert.DeserializeObject<ClientResponse>(json, Converter.Settings);
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
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("primary")]
        public long Primary { get; set; }
    }

    public partial class Client
    {
        public static Client FromJson(string json) => JsonConvert.DeserializeObject<Client>(json, Converter.Settings);
    }

    public static class ClientSerialize
    {
        public static string ToJson(this AddClientClass self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
