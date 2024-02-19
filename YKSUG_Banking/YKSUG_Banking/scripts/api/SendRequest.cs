using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.api
{
    public class SendRequest
    {
        public static async Task<string> PostRequest(string url, object request, string token)
        {
            HttpClient client = new HttpClient();
        
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> SendGetRequest(string url, string token)
        {
            HttpClient client = new HttpClient();
        
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
            var response = await client.GetAsync(new Uri(url));

            return await response.Content.ReadAsStringAsync();
        }
    }
}