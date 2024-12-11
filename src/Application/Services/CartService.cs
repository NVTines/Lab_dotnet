using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CartModel>> GetAllAsync()
        {
            var carts = await _unitOfWork.cartRepository.FindByCondition(
                c => true,
                false,
                c => c.User,
                c => c.CartItems
            ).ToListAsync();

            return _mapper.Map<List<CartModel>>(carts);
        }

        public async Task<CartModel?> GetByIdAsync(int id)
        {
            var cart = await _unitOfWork.cartRepository.GetFirstOrDefaultAsync(
                c => c.Id.Equals(id),
                false,
                c => c.User,
                c => c.CartItems
            );

            if (cart == null) return null;

            var cartModel = _mapper.Map<CartModel>(cart);

            foreach (var item in cartModel.CartItems)
            {
                var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(b => b.Id.Equals(item.BookId));
                if (book != null)
                {
                    item.Book = _mapper.Map<BookModel>(book);
                }
            }

            return cartModel;
        }

        public async Task<CartModel?> GetByUserIdAsync(int userId)
        {
            var carts = await _unitOfWork.cartRepository.GetFirstOrDefaultAsync(
                c => c.UserId.Equals(userId),
                false,
                c => c.User,
                c => c.CartItems
            );

            var cartModels = _mapper.Map<CartModel>(carts);

            foreach (var item in cartModels.CartItems)
            {
                var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(b => b.Id.Equals(item.BookId));
                if (book != null)
                {
                    item.Book = _mapper.Map<BookModel>(book);
                }
            }

            return cartModels;
        }

        public async Task AddToCartAsync(int userId, int bookId, int quantity)
        {
            await _unitOfWork.BeginTransactionAsync();
            var cart = await _unitOfWork.cartRepository.GetFirstOrDefaultAsync(
                c => c.UserId.Equals(userId),
                true,
                c => c.CartItems
            );

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>(),
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                await _unitOfWork.cartRepository.AddAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    BookId = bookId,
                    Quantity = quantity
                });
            }

            cart.UpdatedOn = DateTime.Now;

            await _unitOfWork.cartRepository.UpdateAsync(cart);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CartModel> UpdateAsync(int cartId, int userId, int cartItemId, int quantity)
        {
            var cart = await _unitOfWork.cartRepository.GetFirstOrDefaultAsync(
                c => c.Id == cartId && c.User.Id == userId, 
                false,
                c => c.User,
                c => c.CartItems
            );

            if (cart == null) throw new Exception("Cart not found");

            cart.UpdatedOn = DateTime.Now;

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity; 
            }
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.cartRepository.UpdateAsync(cart);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CartModel>(cart);
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await _unitOfWork.cartItemRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (cart == null) throw new Exception("Cart not found");

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.cartItemRepository.DeleteAsync(cart);
            await _unitOfWork.CommitAsync();
        }
    }
}
