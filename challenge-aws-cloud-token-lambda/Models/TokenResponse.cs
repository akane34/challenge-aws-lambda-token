using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.cloud.security
{
    public class TokenResponse
    {
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("user_profile")]
        public UserProfile UserProfile { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonIgnore]
        public bool HasError => !string.IsNullOrEmpty(Error);

        public JObject ToJson()
        {
            return JObject.FromObject(this);
        }
    }
}
