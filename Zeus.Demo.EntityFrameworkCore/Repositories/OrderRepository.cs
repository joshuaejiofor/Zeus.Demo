using Zeus.Demo.Core.IRepositories;
using Zeus.Demo.Core.Models;
using Zeus.Demo.EntityFrameworkCore.Repositories.Base;
using Serilog;

namespace Zeus.Demo.EntityFrameworkCore.Repositories
{
    public class OrderRepository(ZeusDemoDbContext zeusDemoDbContext, ILogger logger) 
        : Repository<Order>(zeusDemoDbContext, logger), IOrderRepository
    {
    }
}
