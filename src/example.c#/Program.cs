using System;
using Tesla;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Start(args).Wait();
        }

        // ReSharper disable once UnusedParameter.Local
        private static async Task Start(string[] args)
        {
            // Magic clientid and clientsecret available here: http://pastebin.com/fX6ejAHd
            try
            {
                var client = new Client();
                var auth = new Auth
                {
                    ClientID = Environment.GetEnvironmentVariable("TESLA_CLIENT_ID"),
                    ClientSecret = Environment.GetEnvironmentVariable("TESLA_CLIENT_SECRET"),
                    Email = Environment.GetEnvironmentVariable("TESLA_USERNAME"),
                    Password = Environment.GetEnvironmentVariable("TESLA_PASSWORD")
                };

                var t = await client.Authorize(auth);

                Console.WriteLine($"Token: {t.AccessToken}");

                var vehicles = await client.Vehicles(t);

                foreach (var v in vehicles)
                {
                    Console.WriteLine($"Vehicle: {v.DisplayName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
