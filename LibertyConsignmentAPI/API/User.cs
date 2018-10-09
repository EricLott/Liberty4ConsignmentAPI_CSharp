﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Security;

namespace LibertyConsignmentAPI
{
    public partial class LoginResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public partial class User
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("loginid")]
        public string Loginid { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("lockout")]
        public long Lockout { get; set; }

        [JsonProperty("account_list")]
        public string AccountList { get; set; }

        public string Username { get; set; }
        public SecureString Password { get; set; }

        public User()
        {

        }
        /// <summary>
        /// Uses SecureString object as password and immediately clears it.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public User(string user, SecureString pass)
        {
            Username = user;
            Password = pass;
        }
    }

    public partial class LoginResponse
    {
        public static LoginResponse FromJson(string json) => JsonConvert.DeserializeObject<LoginResponse>(json, LoginConverter.Settings);
    }

    public static class LoginSerialize
    {
        public static string ToJson(this LoginResponse self) => JsonConvert.SerializeObject(self, LoginConverter.Settings);
    }

    internal static class LoginConverter
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
