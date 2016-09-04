using Checkout.ApiServices.Orders.RequestModels;
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
        private ApiHttpClient _client = new ApiHttpClient();

        public HttpResponse<Order> CreateOrder(OrderCreate requestModel)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, "/api/orders");
            return _client.PostRequest<Order>(createOrderUri, string.Format("Bearer {0}", token.AccessToken), requestModel);
        }

        public HttpResponse<Order> GetOrders(string name)
        {
            var token = GetAccessToken();

            var createOrderUri = string.Concat(_baseAddress, string.Format("/api/orders/{0}", name));
            return _client.PostRequest<Order>(createOrderUri, string.Format("Bearer {0}", token.AccessToken), name);
        }



        private Token GetAccessToken()
        {
            var form = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", "jignesh"},
                {"password", "user123456"},
            };

            var tokenResponse = _client.GetAccessToken(_baseAddress + "/oauth/token", new FormUrlEncodedContent(form));

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