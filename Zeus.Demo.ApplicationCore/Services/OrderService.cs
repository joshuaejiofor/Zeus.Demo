using AutoMapper;
using Microsoft.Extensions.Configuration;
using Zeus.Demo.ApplicationCore.Services.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using Serilog;
using Zeus.Demo.Core.Models;

namespace Zeus.Demo.ApplicationCore.Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration) 
        : ServiceBase(unitOfWork, mapper, logger, configuration), IOrderService
    {
        public async Task AddToCartAsync(Order order)
        {
            order.OrderStatus = OrderStatus.InCart;
            var existingOrder = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(c => c.OrderStatus == order.OrderStatus && c.ProductId == order.ProductId && c.UserId == order.UserId);

            if (existingOrder != null)
                ++existingOrder.Quantity;
            else
                await _unitOfWork.OrderRepository.AddAsync(order);

            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveFromCartAsync(Order order)
        {
            var existingOrder = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(c => c.OrderStatus == order.OrderStatus && c.ProductId == order.ProductId && c.UserId == order.UserId);

            if (existingOrder == null) return;

            if (existingOrder.Quantity > 1)
                --existingOrder.Quantity;
            else
                _unitOfWork.OrderRepository.Remove(existingOrder);

            await _unitOfWork.CompleteAsync();
        }

    }
}