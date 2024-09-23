using Zeus.Demo.Core.IRepositories;

namespace Zeus.Demo.Core.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }

        Task ExecuteSqlAsync(string statement);
        Task<int> CompleteAsync();
    }
}
