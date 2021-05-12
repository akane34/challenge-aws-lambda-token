using System;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using challenge.cloud.security.Models;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using challenge.cloud.security.Commons;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace challenge.cloud.security
{
    public class Function
    {
        public async Task<TokenResponse> TokenFunctionHandler(AuthorizationCode input, ILambdaContext context)
        {
            LambdaLogger.Log($"Calling function name: {context.FunctionName}\n");

            if (input != null && input.Code != null)
            {
                LambdaLogger.Log($"#### input: {input.ToJson().ToString()}\n");
                                
                var uri = $"{Configuration.TOKEN_URL}oauth2/token";

                var parameters = new Dictionary<string, string>();
                parameters.Add("grant_type", input.GrantType);
                parameters.Add("client_id", input.ClientId);
                parameters.Add("redirect_uri", input.RedirectUri);
                parameters.Add("code", input.Code);
                parameters.Add("code_verifier", input.CodeVerifier);
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                               $"{input.ClientId}:{Configuration.TOKEN_URL}")));
                var reqToken = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(parameters)};

                var resToken = await httpClient.SendAsync(reqToken);
                                                
                var responseTokenContent = await resToken.Content.ReadAsStringAsync();

                LambdaLogger.Log($"#### uri: {uri}\n");
                LambdaLogger.Log($"#### responseTokenContent: {responseTokenContent}\n");

                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseTokenContent);
                if (tokenResponse != null && tokenResponse.IdToken != null && tokenResponse.AccessToken != null)
                {
                    LambdaLogger.Log($"#### tokenResponse: {tokenResponse.ToJson().ToString()}\n");

                    uri = $"{Configuration.TOKEN_URL}oauth2/userInfo"; 

                    var httpClient2 = new HttpClient();
                    httpClient2.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer", tokenResponse.AccessToken);
                    var reqProfile = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent("", Encoding.UTF8, "application/json") };
                    var resProfile = await httpClient2.SendAsync(reqProfile);

                    var responseProfileContent = await resProfile.Content.ReadAsStringAsync();

                    tokenResponse.UserProfile = JsonConvert.DeserializeObject<UserProfile>(responseProfileContent);
                    
                    LambdaLogger.Log($"#### responseProfileContent: {responseProfileContent}\n");
                }

                return tokenResponse;
            }
            else
            {
                LambdaLogger.Log($"Input is null\n");
                return new TokenResponse();
            }
        }
    }
}
