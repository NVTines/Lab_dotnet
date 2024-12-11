using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllAsync()
        {
            var books = await _unitOfWork.bookRepository.FindByCondition(c => true, false).ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel?> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            return _mapper.Map<BookModel?>(book);
        }

        public async Task<BookModel?> GetByNameAsync(string name)
        {
            var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(c => c.Title.Equals(name));
            return _mapper.Map<BookModel?>(book);
        }

        public async Task<BookModel> CreateAsync(BookModel item)
        {
            var book = _mapper.Map<Book>(item);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.bookRepository.AddAsync(book);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<BookModel>(book);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<BookModel?> UpdateAsync(int id, BookModel item)
        {
            var existingBook = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (existingBook == null) return null;

            _mapper.Map(item, existingBook); 

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.bookRepository.UpdateAsync(existingBook);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<BookModel>(existingBook);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _unitOfWork.bookRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (book != null)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();

                    await _unitOfWork.bookRepository.DeleteAsync(book);
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