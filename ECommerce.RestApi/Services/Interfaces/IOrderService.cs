using ECommerce.RestApi.Models;
using System.Data;
using System.Runtime.InteropServices;

namespace ECommerce.RestApi.Services
{
    public interface IOrderService
    {

        Order GetOrder(string id);

        List<Order> GetOrdersByUser(string username);

        Order Create(Order order);

        Order Update(Order order);

        bool Delete(Order order);
    }
}
