using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public static class HttpClientHelper
    {
        public static async Task<T> GetRequest<T>(string apiUrl)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var result = default(T);
                httpClient.DefaultRequestHeaders.Add("X-Authy-API-Key", "0IVTNu6BmhtiOTesMMj1WPTbBPW3fdbe");
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(apiResponse);
                }

                return result;
            }
        }

        public static async Task<O> GetRequestwithReturnEntity<O, I>(string apiUrl, I model)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var result = default(O);
                httpClient.DefaultRequestHeaders.Add("X-Authy-API-Key", "0IVTNu6BmhtiOTesMMj1WPTbBPW3fdbe");
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<O>(apiResponse);
                }

                return result;
            }
        }


        public static async Task<O> PostRequest<O, I>(string apiUrl, I model)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var result = default(O);
                System.Net.Http.StringContent content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                content.Headers.Add("X-Authy-API-Key", "0IVTNu6BmhtiOTesMMj1WPTbBPW3fdbe");

                using (var response = await httpClient.PostAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<O>(apiResponse);
                }

                return result;
            }
        }

    }
}
