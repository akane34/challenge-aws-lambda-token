using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.cloud.security.Commons
{
    public static class Configuration
    {
        #region attributes
        private static string _tokenUrl;
        private static string _clientSecret;
        #endregion

        #region properties
        public static string TOKEN_URL
        {
            get
            {
                if (_tokenUrl == null)
                {
                    _tokenUrl = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TOKEN_URL"))
                        ?
                        "https://cloudchallenge1.auth.us-east-1.amazoncognito.com/"
                        :
                        Environment.GetEnvironmentVariable("TOKEN_URL");

                    if (!_tokenUrl.EndsWith("/"))
                        _tokenUrl = _tokenUrl + "/";
                }
                return _tokenUrl;
            }
        }

        public static string CLIENT_SECRET
        {
            get
            {
                if (_clientSecret == null)
                {
                    _clientSecret = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CLIENT_SECRET"))
                        ?
                        ""
                        :
                        Environment.GetEnvironmentVariable("CLIENT_SECRET");                    
                }
                return _clientSecret;
            }
        }
        #endregion
    }
}
