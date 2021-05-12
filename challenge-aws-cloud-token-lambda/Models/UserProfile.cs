using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.cloud.security
{
    public class UserProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        public JObject ToJson()
        {
            return JObject.FromObject(this);
        }
    }
}
