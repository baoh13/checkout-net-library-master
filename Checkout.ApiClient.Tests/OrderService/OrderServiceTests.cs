using Checkout;
using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace Tests.OrderService
{
    [TestFixture]
    public class OrderServiceTests : BaseServiceTests
    {
        private APIClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new APIClient(secretKey: "user123456");
        }

        [TestCase("Pepsi", 5)]
        [TestCase("Cola", 4)]
        public void CreateCard(string productName, int quantity)
        {
            var orderCreateModel = TestHelper.GetOrderCreateModel(productName, quantity);
            var response = _client.OrderService
                                  .CreateOrder(orderCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Model.Name.Should().Be(productName);
            response.Model.Quantity.Should().Be(quantity);
        }
    }
}