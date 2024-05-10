using Kolokwium01.Models;

namespace Kolokwium01.Repository;

public interface IOrdersRepository
{
    public Task<bool> DoesOrderExist(int id);
    public Task<Order> GetOrder(int id);
    Task<bool> DoesClientExist(int id);

    Task DeleteClient(int id);
}