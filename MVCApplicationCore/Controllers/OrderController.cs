using Microsoft.AspNetCore.Mvc;
using MVCApplicationCore.Models;

namespace MVCApplicationCore.Controllers
{
    public class OrderController : Controller
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order{ OrderId = 1,OrderPrice = 1000, OrderDate = DateTime.Now,
                OrderItem = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderItemId = 1,OrderId = 1,ItemName="Item name 1",ItemPrice=500,
                    },
                    new OrderItem
                    {
                        OrderItemId = 2,OrderId = 1,ItemName="Item name 2",ItemPrice=500,
                    }
                }
            },
            new Order{ OrderId = 2,OrderPrice = 2000, OrderDate = DateTime.Now,
                OrderItem = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderItemId = 3,OrderId = 2,ItemName="Item name 3",ItemPrice=1500,
                    },
                    new OrderItem
                    {
                        OrderItemId = 4,OrderId = 2,ItemName="Item name 4",ItemPrice=1500,
                    }
                }
            },
        };
        public IActionResult Index()
        {
            return View(_orders);
        }
    }
}
