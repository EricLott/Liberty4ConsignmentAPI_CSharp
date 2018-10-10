using LibertyConsignmentAPI.Converters;
using Newtonsoft.Json;

namespace LibertyConsignmentAPI
{
    public partial class CategoryTreeResponse
    {
        [JsonProperty("response")]
        public CategoryTreeResponseObject Response { get; set; }
    }

    public partial class CategoryTreeResponseObject
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
        public static CategoryTreeResponse FromJson(string json) => JsonConvert.DeserializeObject<CategoryTreeResponse>(json, Converter.Settings);
    }
}
