
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PieceTracker.Common
{
    /// <summary>
    /// Class Web API Helper.
    /// </summary>
    public class WebApiHelper
    {
        #region WebAPi Common Method
        private static string _baseUrl = Convert.ToString(WebConfigurationManager.AppSettings["ApiURL"]);
        /// <summary>
        /// HTTPs the client request response.
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>Return T object</returns>
        public static async Task<T> HttpClientRequestResponse<T>(T value, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var response = await client.GetStringAsync(uri);
            if (response.Length > 0)
            {
                var result = JsonConvert.DeserializeObject<T>(response);
                //var result = await response.Content.ReadAsAsync<T>();
                return (T)Convert.ChangeType(result, typeof(T));
            }
            return default(T);
        }

        /// <summary>
        /// HTTPs the client request response synchronize.
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>return T object</returns>
        public static T HttpClientRequestResponseSync<T>(T value, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var httpresult = client.GetAsync(uri).Result;

            if (!httpresult.IsSuccessStatusCode)
                return default(T);

            var result = httpresult.Content.ReadAsAsync<T>();
            return (T)Convert.ChangeType(result.Result, typeof(T));
        }

        /// <summary>
        /// HTTPs the client post.
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>Return T object</returns>
        //public static async Task<string> HttpClientPost<T>(T value, string uri)
        //{
        //    var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var response = await client.PostAsJsonAsync(uri, value);
        //    return response.IsSuccessStatusCode ? string.Empty : response.StatusCode.ToString();
        //}

        public static async Task<T> HttpClientPost<T>(T value, string uri)
        {
            uri = Uri.EscapeUriString(uri);
            HttpResponseMessage response = new HttpResponseMessage();

            //[ToDo : remove below comment.]
            //WebApiAuthHelper objAuth = new WebApiAuthHelper();
            //AuthenticationHeaderValue token = objAuth.GenerateToken(_baseUrl + uri, "POST");
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
                //httpClient.DefaultRequestHeaders.Authorization = token;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                response = await httpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonResponse);
                    // var result = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(result, typeof(T));
                    //do something with json response here
                }
            }
            return default(T);
        }

        /// <summary>
        /// HTTPs the client post pass entity return entity.
        /// </summary>
        /// <typeparam name="O">O object</typeparam>
        /// <typeparam name="I">I object</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>Return T Object</returns>
        public static async Task<O> HttpClientPostPassEntityReturnEntity<O, I>(I value, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var response = await client.PostAsJsonAsync(uri, value);
            if (!response.IsSuccessStatusCode)
                return default(O);

            var result = await response.Content.ReadAsAsync<O>();
            return (O)Convert.ChangeType(result, typeof(O));
        }

        /// <summary>
        /// HTTPs the client post pass model return base API.
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>Return T Object</returns>
        public static async Task<T> HttpClientPostPassModelReturnBaseApi<T>(T value, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigItems.OandaFxTradeKey);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var response = await client.PostAsJsonAsync(uri, value);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsAsync<T>();
            return (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// Clients the delete request.
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns>Return T object</returns>
        public static async Task<T> ClientDeleteRequest<T>(string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var response = await client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsAsync<T>();
            return (T)Convert.ChangeType(result, typeof(T));
        }
        #endregion
    }
}

