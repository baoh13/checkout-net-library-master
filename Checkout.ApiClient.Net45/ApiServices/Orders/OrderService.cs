using Checkout.ApiServices.Orders.ResponseModels;
using Checkout.ApiServices.SharedModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Checkout.ApiServices.Orders
{
    public class OrderService
    {
        private readonly string _baseAddress = "http://localhost:63715";

        public HttpResponse<Order> CreateOrder(Order requestModel)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, "/api/orders");
            return new ApiHttpClient().PostRequest<Order>(createOrderUri, string.Format("Bearer {0}", token.AccessToken), requestModel);
        }

        public HttpResponse<OrderList> GetOrders()
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, "/api/orders");
            return new ApiHttpClient().GetRequest<OrderList>(createOrderUri, string.Format("Bearer {0}", token.AccessToken));
        }

        public HttpResponse<Order> GetOrder(string name)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, string.Format("/api/orders/{0}", name));
            return new ApiHttpClient().GetRequest<Order>(createOrderUri, string.Format("Bearer {0}", token.AccessToken));
        }

        public HttpResponse<Order> UpdateOrder(Order order)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, "/api/orders");
            return new ApiHttpClient().PutRequest<Order>(createOrderUri, string.Format("Bearer {0}", token.AccessToken), order);
        }

        public HttpResponse<OkResponse> DeleteOrder(string name)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, string.Format("/api/orders/{0}", name));
            return new ApiHttpClient().DeleteRequest<OkResponse>(createOrderUri, string.Format("Bearer {0}", token.AccessToken));
        }

        private Token GetAccessToken()
        {
            var form = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", AppSettings.OrderServiceAccessTokenId},
                {"password", AppSettings.OrderServiceAccessTokenKey}
            };

            var httpClient = new HttpClient();
            
            var tokenResponse = httpClient.PostAsync(_baseAddress + "/oauth/token", new FormUrlEncodedContent(form))
                                          .Result;
            string jsonMessage;
            using (var responseStream = tokenResponse.Content.ReadAsStreamAsync().Result)
            {
                jsonMessage = new StreamReader(responseStream).ReadToEnd();
            }

            var token = (Token) JsonConvert.DeserializeObject(jsonMessage, typeof (Token));
            return token;
        }
    }
}