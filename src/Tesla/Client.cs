using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Flurl.Http;
// ReSharper disable InconsistentNaming

namespace Tesla
{
    // Required authorization credentials for the Tesla API
    public struct Auth
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string ClientID { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public string URL { get; set; }

        public string StreamingURL { get; set; }
    }

    // The token and related elements returned after a successful auth by the Tesla API
    public struct Token
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }

    public static class ServiceURL
    {
        public const string AuthURL = "https://owner-api.teslamotors.com/oauth/token";
        public const string BaseURL = "https://owner-api.teslamotors.com/api/1";
    }

	public static class TeslaClient
    {
        public static async Task<T> GetJson<T>(string URL, Token t)
        {
            try
            {
                var response = await URL.WithHeader("Authorization", $"Bearer {t.AccessToken}").GetJsonAsync<T>();

                return response;
            }
            catch (FlurlHttpTimeoutException e)
            {
                throw new Exception($"Failed with timeout exception: {e.Message}", e.InnerException);
            }
            catch (FlurlHttpException e)
            {
                if (e.Call.Response != null)
                {
                    throw new Exception($"Failed with exception : {e.Call.Exception}, response code : {e.Call.Response.StatusCode}", e.InnerException);
                }
                throw new Exception($"Failed before getting a response: {e.Message}", e.InnerException);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class Client
    {
        // Fetches the vehicles associated to a Tesla account via the API
        public async Task<Vehicle[]> Vehicles(Token t)
        {
            var vehicleResponse = await TeslaClient.GetJson<VehicleResponse>($"{ServiceURL.BaseURL}/vehicles", t);

            return vehicleResponse.Vehicles;
        }

        // Authorizes against the Tesla API with the appropriate credentials
        public async Task<Token> Authorize(Auth auth)
        {
            try
            {
                auth.GrantType = "password";
                var token = await ServiceURL.AuthURL.PostJsonAsync(auth).ReceiveJson<Token>();

                return new Token { AccessToken = token.AccessToken, ExpiresIn = token.ExpiresIn, TokenType = token.TokenType };
            }
            catch (FlurlHttpTimeoutException e)
            {
                throw new Exception($"Failed with timeout exception: {e.Message}", e.InnerException);
            }
            catch (FlurlHttpException e)
            {
                if (e.Call.Response != null)
                {
                    throw new Exception($"Failed with exception : {e.Call.Exception}, response code : {e.Call.Response.StatusCode}", e.InnerException);
                }
                throw new Exception($"Failed before getting a response: {e.Message}", e.InnerException);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
