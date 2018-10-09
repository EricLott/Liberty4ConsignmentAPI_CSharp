using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace LibertyConsignmentAPI
{
    public partial class CategoryTreeResponse
    {
        [JsonProperty("response")]
        public CategoryTreeResponseResponse Response { get; set; }
    }

    public partial class CategoryTreeResponseResponse
    {
        [JsonProperty("cat_l1_id")]
        public int Level1CategoryID { get; set; }

        [JsonProperty("cat_l2_id")]
        public int Level2CategoryID { get; set; }

        [JsonProperty("cat_l3_id")]
        public int Level3CategoryID { get; set; }
    }


    public partial class CategoryTreeResponse
    {
        public static CategoryTreeResponse FromJson(string json) => JsonConvert.DeserializeObject<CategoryTreeResponse>(json, GetCategoryTreeResponseConverter.Settings);
    }

    internal static class GetCategoryTreeResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
