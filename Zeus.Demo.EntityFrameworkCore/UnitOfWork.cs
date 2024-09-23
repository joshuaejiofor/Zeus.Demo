using Zeus.Demo.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.EntityFrameworkCore.Repositories;
using Serilog;
using System.ComponentModel;

namespace Zeus.Demo.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ZeusDemoDbContext _context;
        private readonly ILogger _logger;
        public UnitOfWork(ZeusDemoDbContext aceMdSignalDbContext, ILogger logger)
        {
            _context = aceMdSignalDbContext;
            _logger = logger;

            OrderRepository = new OrderRepository(_context, logger);
            ProductRepository = new ProductRepository(_context, logger);
        }

        public IOrderRepository OrderRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        

        [DisplayName("{0}")]
        public async Task ExecuteSqlAsync(string statement)
        {
            try
            {
                _logger.Warning("ExecuteSqlAsync() started");
                var result = await _context.Database.ExecuteSqlRawAsync(statement);
                _logger.Warning("ExecuteSqlAsync() ended");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"ExecuteSqlAsync , ErrorMessage : {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
        }

    }
}
