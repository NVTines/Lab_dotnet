using AutoMapper;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CartItemModel>> GetAllAsync()
        {
            var cartItems = await _unitOfWork.cartItemRepository.FindByCondition(c => true, false).ToListAsync();
            return _mapper.Map<List<CartItemModel>>(cartItems);
        }

        public async Task<CartItemModel?> GetByIdAsync(int id)
        {
            var cartItem = await _unitOfWork.cartItemRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            return _mapper.Map<CartItemModel?>(cartItem);
        }

        public async Task<CartItemModel?> GetByCartIdAsync(int cartId)
        {
            var cartItem = await _unitOfWork.cartItemRepository.GetFirstOrDefaultAsync(c => c.CartId.Equals(cartId));
            return _mapper.Map<CartItemModel?>(cartItem);
        }

        public async Task<CartItemModel> CreateAsync(CartItemModel item)
        {
            var cartItem = _mapper.Map<CartItem>(item);

            await _unitOfWork.cartItemRepository.AddAsync(cartItem);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CartItemModel>(cartItem);
        }

        public async Task<CartItemModel> UpdateAsync(int id, CartItemModel item)
        {
            var cartItem = await _unitOfWork.cartItemRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (cartItem == null) throw new Exception("CartItem not found");

            _mapper.Map(item, cartItem);

            await _unitOfWork.cartItemRepository.UpdateAsync(cartItem);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CartItemModel>(cartItem);
        }

        public async Task DeleteAsync(int id)
        {
            var cartItem = await _unitOfWork.cartItemRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (cartItem == null) throw new Exception("CartItem not found");

            await _unitOfWork.cartItemRepository.DeleteAsync(cartItem);
            await _unitOfWork.CommitAsync();
        }
    }
}