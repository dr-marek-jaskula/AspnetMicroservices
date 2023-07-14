using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistance;

namespace Ordering.Infrastructure.Repositories;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext orderContext) 
        : base(orderContext)
    {
        
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await _dbContext
            .Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
    }
}