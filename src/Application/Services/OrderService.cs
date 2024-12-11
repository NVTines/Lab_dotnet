//using Application.Interfaces;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Interfaces;
//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class OrderService : IOrderService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public async Task<List<OrderModel>> GetAllAsync()
//        {
//            var orders = await _unitOfWork.orderRepository.FindByCondition(
//                c => true,
//                false,
//                c => c.User,
//                c => c.OrderItems
//            ).ToListAsync();

//            return _mapper.Map<List<OrderModel>>(orders);
//        }

//        public async Task<OrderModel?> GetByIdAsync(int id)
//        {
//            var order = await _unitOfWork.orderRepository.GetFirstOrDefaultAsync(
//                c => c.Id.Equals(id),
//                false,
//                c => c.User,
//                c => c.OrderItems
//            );

//            return _mapper.Map<OrderModel?>(order);
//        }

//        public async Task<OrderModel?> GetByUserIdAsync(int userId)
//        {
//            var order = await _unitOfWork.orderRepository.GetFirstOrDefaultAsync(
//                c => c.UserId.Equals(userId),
//                false,
//                c => c.User,
//                c => c.OrderItems
//            );

//            return _mapper.Map<OrderModel?>(order);
//        }

//        public async Task PayAsync(int userId, int bookId, int quantity)
//        {
//            //var user = await _unitOfWork.orderRepository.GetFirstOrDefaultAsync(
//            //    c => c.UserId.Equals(userId),
//            //    false,
//            //    c => c.User,
//            //    c => c.OrderItems
//            //);

//            //if (user == null) throw new ArgumentException("User not found");
//            //var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(bookId));

//            //if (book == null) throw new ArgumentException("Book not found");
//            //var order = new OrderModel
//            //{
//            //    UserId = userId,
//            //    OrderDate = DateTime.UtcNow,
//            //    Status = "Paid",
//            //    TotalAmount = book.Price * quantity,
//            //    OrderItems = new List<OrderItemModel>
//            //    {
//            //        new OrderItemModel
//            //        {
//            //            BookId = bookId,
//            //            Quantity = quantity,
//            //            Price = book.Price * quantity
//            //        }
//            //    }
//            //};
//            //await _unitOfWork.BeginTransactionAsync();

//            //await _unitOfWork.orderRepository.AddAsync(order);
//            //await _unitOfWork.CommitAsync();
//        }

//        public async Task<OrderModel> UpdateAsync(int orderId, int userId, int orderItemId, int quantity)
//        {
//            var order = await _unitOfWork.orderRepository.GetFirstOrDefaultAsync(
//                c => c.Id == orderId && c.User.Id == userId,
//                false,
//                c => c.User,
//                c => c.OrderItems
//            );

//            if (order == null) throw new ArgumentException("Order not found");

//            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
//            if (orderItem == null) throw new ArgumentException("Order item not found");

//            order.TotalAmount -= orderItem.Price;
//            orderItem.Quantity = quantity;
//            orderItem.Price = quantity * (orderItem.Price / orderItem.Quantity);
//            order.TotalAmount += orderItem.Price;

//            await _unitOfWork.BeginTransactionAsync();
//            await _unitOfWork.orderRepository.UpdateAsync(order);
//            await _unitOfWork.CommitAsync();
//            return _mapper.Map<OrderModel>(order);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var orderItem = await _unitOfWork.orderItemRepository.GetFirstOrDefaultAsync(
//                c => c.Id.Equals(id),
//                false,
//                c => c.Order,
//                c => c.Book
//            );
//            if (orderItem == null) throw new ArgumentException("Order item not found");

//            var order = await _unitOfWork.orderRepository.GetFirstOrDefaultAsync(
//                c => c.Id.Equals(orderItem.OrderId),
//                false,
//                c => c.OrderItems,
//                c => c.User
//            );
//            if (order != null)
//            {
//                order.TotalAmount -= orderItem.Price;
//            }
//            await _unitOfWork.BeginTransactionAsync();
//            await _unitOfWork.orderItemRepository.DeleteAsync(orderItem);
//            await _unitOfWork.CommitAsync();
//        }
//    }
//}
