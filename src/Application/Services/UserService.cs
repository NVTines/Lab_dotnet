using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.userRepository.FindByCondition(c => true, false).ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel?> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            return _mapper.Map<UserModel?>(user);
        }

        public async Task<UserModel?> GetByUsernameAsync(string username)
        {
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(c => c.Username.Equals(username));
            return _mapper.Map<UserModel?>(user);
        }

        public async Task<UserModel> CreateAsync(UserModel item)
        {
            var user = _mapper.Map<User>(item);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();

                item.Id = user.Id; 
                return item;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<UserModel> UpdateAsync(int id, UserModel item)
        {
            var existingUser = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (existingUser == null) return null;

            _mapper.Map(item, existingUser);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.userRepository.UpdateAsync(existingUser);
                await _unitOfWork.CommitAsync();

                return item;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (user != null)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();

                    await _unitOfWork.userRepository.DeleteAsync(user);
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
        }
    }
}