﻿using LibertyConsignmentAPI.Converters;
using Newtonsoft.Json;
using System;

namespace LibertyConsignmentAPI
{
    public partial class GetItemPriceHistoryResponse
    {
        [JsonProperty("response")]
        public GetItemPriceHistoryResponseObject Response { get; set; }
    }

    public partial class GetItemPriceHistoryResponseObject
    {
        [JsonProperty("high_price")]
        public decimal HighPrice { get; set; }

        [JsonProperty("low_price")]
        public decimal LowPrice { get; set; }

        [JsonProperty("avg_price")]
        public decimal AvgPrice { get; set; }

        [JsonProperty("price_data")]
        public PriceDatum[] PriceData { get; set; }
    }

    public partial class PriceDatum
    {
        [JsonProperty("price_element")]
        public PriceElement PriceElement { get; set; }
    }

    public partial class PriceElement
    {
        [JsonProperty("sort")]
        public int Sort { get; set; }

        [JsonProperty("week")]
        public int Week { get; set; }

        [JsonProperty("high_price")]
        public decimal HighPrice { get; set; }

        [JsonProperty("low_price")]
        public decimal LowPrice { get; set; }

        [JsonProperty("avg_price")]
        public decimal AvgPrice { get; set; }

        [JsonProperty("low_date")]
        public string LowDate { get; set; }

        public DateTime LowDateDateTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LowDate))
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return new LibertyDate(LowDate).Value;
                }
            }
        }
    }

    public partial class GetItemPriceHistoryResponse
    {
        public static GetItemPriceHistoryResponse FromJson(string json) => JsonConvert.DeserializeObject<GetItemPriceHistoryResponse>(json, Converter.Settings);
    }

    public partial class CheckLicenseResponse
    {
        [JsonProperty("response")]
        public CheckLicenseResponseObject Response { get; set; }
    }

    public partial class CheckLicenseResponseObject
    {
        [JsonProperty("license_info")]
        public LicenseInfo LicenseInfo { get; set; }
    }

    public partial class LicenseInfo
    {
        [JsonProperty("storeid")]
        public long Storeid { get; set; }

        [JsonProperty("storename")]
        public string Storename { get; set; }

        [JsonProperty("licensename")]
        public string Licensename { get; set; }
    }

    public partial class CheckLicenseResponse
    {
        public static CheckLicenseResponse FromJson(string json) => JsonConvert.DeserializeObject<CheckLicenseResponse>(json, Converter.Settings);
    }
}
