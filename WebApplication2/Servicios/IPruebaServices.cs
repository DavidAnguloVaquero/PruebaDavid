using WebApplication2.Models;

namespace WebApplication2.Servicios
{
    public interface IPruebaServices
    {
        Task<object> CreateOrderAsync(CreateOrderRequest request);
        Task<object> GetOrdersPagedAsync(int page, int pageSize);
        Task<object> GetOrderByIdAsync(int id);




    }
}
