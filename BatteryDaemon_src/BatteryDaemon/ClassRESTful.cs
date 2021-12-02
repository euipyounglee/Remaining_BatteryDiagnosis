using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDaemon
{
    class ClassRESTful
    {

        private string requestURL = "https://yourURL";
        private string headerUsername = "username";
        private string headerPassword = "password";

        void Start()
        {
            HttpClientExampleAsync();
        }

        async void HttpClientExampleAsync()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue(
                                    "Basic", Convert.ToBase64String(
                                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                                           $"{headerUsername}:{headerPassword}")));

            Dictionary<string, string> values = new Dictionary<string, string>
        {
        { "bodyParam1", "bodyParam1Value" },
        { "bodyParam2", "bodyParam2Value" }
        };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values); // Content-Type: application/json

            var response = await client.PostAsync(requestURL, content);

            var responseString = await response.Content.ReadAsStringAsync();

           // Debug.Log(responseString);
            Debug.WriteLine(responseString);
        }

    }
}
