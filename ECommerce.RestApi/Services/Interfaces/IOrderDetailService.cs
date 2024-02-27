using ECommerce.RestApi.Models;
using System.Runtime.InteropServices;

namespace ECommerce.RestApi.Services
{
    public interface IOrderDetailService
    {
        public OrderDetail Create(OrderDetail orderDetail, string productId, string orderId);

        public bool Delete(OrderDetail orderDetail, string productId, string orderId);

    }
}
