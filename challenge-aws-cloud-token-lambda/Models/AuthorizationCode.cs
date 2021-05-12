using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.cloud.security.Models
{
    public class AuthorizationCode
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty ("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("code_verifier")]
        public string CodeVerifier { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        public JObject ToJson()
        {
            return JObject.FromObject(this);
        }
    }
}
