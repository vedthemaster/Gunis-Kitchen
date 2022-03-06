using Gunis.Kitchen.Models;

namespace Gunis.Kitchen.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order Order);
        
    }
}
