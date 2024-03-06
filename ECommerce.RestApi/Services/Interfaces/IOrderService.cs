using ECommerce.RestApi.Models;

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
