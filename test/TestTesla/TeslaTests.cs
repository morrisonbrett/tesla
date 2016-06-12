using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Flurl.Http;
using Flurl.Http.Testing;
using Tesla;
using Xunit;
using Moq;
using Xunit.Abstractions;
// ReSharper disable InconsistentNaming

namespace TestTesla
{
    public class TeslaTests
    {
        [Theory]
        [InlineData("theclientid", "thesecret", "the@email.com", "thepassword", "thetoken")]
        public async Task AuthTest(string ClientID, string ClientSecret, string Email, string Password, string Token)
        {
            var client = new Client();
            var auth = new Auth { ClientID = ClientID, ClientSecret = ClientSecret, Email = Email, Password = Password };

            using (var httpTest = new HttpTest())
            {
                var moqT = new Token{ AccessToken = Token };

                httpTest.RespondWith(200, JsonConvert.SerializeObject(moqT));

                var t = await client.Authorize(auth);

                httpTest.ShouldHaveCalled(ServiceURL.AuthURL);

                Assert.Equal(t.AccessToken, Token);
            }
        }

        [Theory]
        [InlineData("theclientid", "thesecret", "the@email.com", "thepassword", "thetoken")]
        public async Task AuthFailTest(string ClientID, string ClientSecret, string Email, string Password, string Token)
        {
            var client = new Client();
            var auth = new Auth { ClientID = ClientID, ClientSecret = ClientSecret, Email = Email, Password = Password };

            using (var httpTest = new HttpTest())
            {
                var moqT = new Token { AccessToken = Token };

                httpTest.RespondWith(401, JsonConvert.SerializeObject(moqT));

                try
                {
                    var t = await client.Authorize(auth);
                }
                catch (Exception e)
                {
                    Assert.True(e.Message.Contains("401"));
                }

                httpTest.ShouldHaveCalled(ServiceURL.AuthURL);
            }
        }

        [Theory]
        [InlineData("theclientid", "thesecret", "the@email.com", "thepassword", "thetoken")]
        public async Task AuthTimeoutTest(string ClientID, string ClientSecret, string Email, string Password, string Token)
        {
            var client = new Client();
            var auth = new Auth { ClientID = ClientID, ClientSecret = ClientSecret, Email = Email, Password = Password };

            using (var httpTest = new HttpTest())
            {
                httpTest.SimulateTimeout();

                try
                {
                    var t = await client.Authorize(auth);
                }
                catch (Exception e)
                {
                    Assert.True(e.Message.Contains("timeout"));
                }

                httpTest.ShouldHaveCalled(ServiceURL.AuthURL);
            }
        }

        [Theory]
        [InlineData("thetoken", 999)]
        public async Task VehiclesTest(string Token, int ID)
        {
            var client = new Client();

            using (var httpTest = new HttpTest())
            {
                var moqVehicles = new Vehicle[1];
                moqVehicles[0] = new Vehicle { ID = ID };
                var moqVehicleResponse = new VehicleResponse { Count = 1, Vehicles = moqVehicles };

                httpTest.RespondWith(200, JsonConvert.SerializeObject(moqVehicleResponse));

                var vehicles = await client.Vehicles(new Token { AccessToken = Token });

                httpTest.ShouldHaveCalled($"{ServiceURL.BaseURL}/vehicles");

                Assert.Equal(vehicles[0].ID, ID);
            }
        }
    }
}
