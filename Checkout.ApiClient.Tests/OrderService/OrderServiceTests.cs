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
            _client = new APIClient();
        }

        [TestCase("Pepsi", 5)]
        [TestCase("coke", 4)]
        public void CreateOrder(string productName, int quantity)
        {
            var orderCreateModel = TestHelper.GetOrderCreateModel(productName, quantity);
            var response = _client.OrderService
                                  .CreateOrder(orderCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Model.Name.Should().Be(productName);
            response.Model.Quantity.Should().Be(quantity);
        }

        [Test]
        public void GetOrder()
        {
            const int quantity = 5;
            var orderCreateModel = TestHelper.GetOrderCreateModel("coke67", quantity);
            _client.OrderService.CreateOrder(orderCreateModel);

            var response = _client.OrderService.GetOrder("coke67");

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetOrders()
        {
            const int quantity = 5;
            var orderCreateModel = TestHelper.GetOrderCreateModel("coke1List1", quantity);
            _client.OrderService.CreateOrder(orderCreateModel);

            var response = _client.OrderService.GetOrders();

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void UpdateOrder()
        {
            const int quantity = 5;
            var orderCreateModel = TestHelper.GetOrderCreateModel("coke3", quantity);
            _client.OrderService.CreateOrder(orderCreateModel);

            var response = _client.OrderService.UpdateOrder(TestHelper.GetOrderCreateModel(name: orderCreateModel.Name, quantity: 6));

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void DeleteOrder()
        {
            const int quantity = 5;
            var orderToBeDeleted = TestHelper.GetOrderCreateModel("cokeToBeDeleted", quantity);
            _client.OrderService.CreateOrder(orderToBeDeleted);

            var response = _client.OrderService.DeleteOrder(orderToBeDeleted.Name);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}