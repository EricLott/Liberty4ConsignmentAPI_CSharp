using LibertyConsignmentAPI.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace LibertyConsignmentAPI
{
    public partial class ItemResponse
    {
        [JsonProperty("response")]
        public ItemResponseObject Response { get; set; }
    }

    public partial class ItemResponse
    {
        public static ItemResponse FromJson(string json) => JsonConvert.DeserializeObject<ItemResponse>(json, Converter.Settings);
    }

    public partial class ItemResponseObject
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }

    public partial class AddItem
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; } = 0;

        [JsonProperty("client_id")]
        public long ClientId { get; set; } = 0;

        [JsonProperty("client_name")]
        public string ClientName { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("account")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Account { get; set; } = 0;

        [JsonProperty("number")]
        public long Number { get; set; } = 0;

        [JsonProperty("purchaseMethod")]
        public long PurchaseMethod { get; set; } = 1;

        [JsonProperty("received")]
        public string Received { get; set; } = DateTime.Now.ToString();

        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("status_id")]
        public long StatusId { get; set; } = 1;

        [JsonProperty("category_qualified_name")]
        public string CategoryQualifiedName { get; set; } = "";

        [JsonProperty("category_id")]
        public long CategoryId { get; set; } = 0;

        [JsonProperty("location_id")]
        public long LocationID { get; set; } = 1;

        [JsonProperty("descript")]
        public string Description { get; set; } = "";

        [JsonProperty("price")]
        public decimal Price { get; set; } = 0;

        [JsonProperty("cost")]
        public long Cost { get; set; } = 0;

        [JsonProperty("item_fee")]
        public long ItemFee { get; set; } = 0;

        [JsonProperty("quantity")]
        public int Quantity { get; set; } = 1;

        [JsonProperty("tag_quantity")]
        public int TagQuantity { get; set; } = 1;

        [JsonProperty("price_code_id")]
        public long PriceCodeId { get; set; } = 1;

        [JsonProperty("tag_color_id")]
        public long TagColorId { get; set; } = 0;

        [JsonProperty("donate_ind")]
        public long DonateInd { get; set; } = 0;

        [JsonProperty("section")]
        public long SectionID { get; set; } = 1;

        [JsonProperty("msrp")]
        public decimal Msrp { get; set; } = 0;

        [JsonProperty("tax_type_id")]
        public long TaxTypeId { get; set; } = 1;

        [JsonProperty("sub_title")]
        public string SubTitle { get; set; } = "";

        [JsonProperty("user_field1")]
        public string UserField1 { get; set; } = "";

        [JsonProperty("user_field2")]
        public string UserField2 { get; set; } = "";

        [JsonProperty("user_field3")]
        public string UserField3 { get; set; } = "";

        [JsonProperty("user_field4")]
        public string UserField4 { get; set; } = "";

        [JsonProperty("user_field5")]
        public string UserField5 { get; set; } = "";

        [JsonProperty("valuation")]
        public long Valuation { get; set; } = 0;

        [JsonProperty("weight")]
        public long Weight { get; set; } = 0;

        [JsonProperty("shipping_fee")]
        public long ShippingFee { get; set; } = 0;

        [JsonProperty("website_id")]
        public long WebsiteId { get; set; } = 0;

        [JsonProperty("web_descript")]
        public string WebDescript { get; set; } = "";

        [JsonProperty("max_quantity")]
        public long MaxQuantity { get; set; } = 1;

        [JsonProperty("web_quantity")]
        public long WebQuantity { get; set; } = 0;

        [JsonProperty("handling_fee")]
        public long HandlingFee { get; set; } = 0;

        [JsonProperty("height")]
        public long Height { get; set; } = 0;

        [JsonProperty("item_length")]
        public long ItemLength { get; set; } = 0;

        [JsonProperty("width")]
        public long Width { get; set; } = 0;

        [JsonProperty("start")]
        public string Start { get; set; } = DateTime.Now.ToString();

        [JsonProperty("attributes")]
        public Attribute[] Attributes { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        public DateTime ReceivedDateTime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Received))
                {
                    return new LibertyDate(Received).Value;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Start))
                {
                    return new LibertyDate(Start).Value;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }
    }

    public partial class Item
    {
        public static Item FromJson(string json) => JsonConvert.DeserializeObject<Item>(json, Converter.Settings);
    }

    public partial class Attribute
    {
        [JsonProperty("item_attr")]
        public ItemAttr ItemAttr { get; set; }
    }

    public partial class ItemAttr
    {
        [JsonProperty("cat_attr_name")]
        public string CatAttrName { get; set; }

        [JsonProperty("attr_value")]
        public string AttrValue { get; set; }

        [JsonProperty("attr_type")]
        public long AttrType { get; set; }

        [JsonProperty("cat_attr_id")]
        public long CatAttrId { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("use_in_title")]
        public long UseInTitle { get; set; }

        [JsonProperty("attr_value_id")]
        public long AttrValueId { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("item_image")]
        public ItemImage ItemImage { get; set; }
    }

    public partial class ItemImage
    {
        [JsonProperty("image_id")]
        public long ImageId { get; set; }

        [JsonProperty("image_file")]
        public string ImageFile { get; set; }

        [JsonProperty("image_number")]
        public long ImageNumber { get; set; }
    }

    public static class ItemSerialize
    {
        public static string ToJson(this AddItem self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

}