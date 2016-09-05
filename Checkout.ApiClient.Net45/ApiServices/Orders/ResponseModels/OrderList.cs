using System.Collections.Generic;

namespace Checkout.ApiServices.Orders.ResponseModels
{
    public class OrderList
    {
        public int Count;
        public List<Order> Orders;
    }
}
