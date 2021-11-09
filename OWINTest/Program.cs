// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace OWINTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:4312";
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                   {
                       {"grant_type", "password"},
                       {"username", "jignesh"},
                       {"password", "user123456"},
                   };
                var tokenResponse = client.PostAsync(baseAddress + "/oauth/token", new FormUrlEncodedContent(form)).Result;
                //var token = tokenResponse.Content.ReadAsStringAsync().Result;  
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (string.IsNullOrEmpty(token.Error))
                {
                    Console.WriteLine("Token issued is: {0}", token.AccessToken);
                }
                else
                {
                    Console.WriteLine("Error : {0}", token.Error);
                }
                Console.Read();
            }

           


        }
    }
}
