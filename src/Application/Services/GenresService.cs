using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Application.Services
{
    public class GenresService : IGenresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenresService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GenreModel>> GetGenresAsync()
        {
            var genres = await _unitOfWork.genreRepository.FindByCondition(c => true, false).ToListAsync();
            return _mapper.Map<List<GenreModel>>(genres);
        }

        public async Task<GenreModel?> GetGenreByIdAsync(int id)
        {
            var genre = await _unitOfWork.genreRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            return _mapper.Map<GenreModel?>(genre);
        }

        public async Task<GenreModel?> GetGenreByNameAsync(string name)
        {
            var genre = await _unitOfWork.genreRepository.GetFirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<GenreModel?>(genre);
        }

        public async Task CreateAsync(GenreModel item)
        {
            var genre = _mapper.Map<Genre>(item);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.genreRepository.AddAsync(genre);
                await _unitOfWork.CommitAsync();

                item.Id = genre.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(int id, GenreModel item)
        {
            var existingGenre = await _unitOfWork.genreRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (existingGenre == null) return;

            _mapper.Map(item, existingGenre); 

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.genreRepository.UpdateAsync(existingGenre);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _unitOfWork.genreRepository.GetFirstOrDefaultAsync(c => c.Id.Equals(id));
            if (genre != null)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();

                    await _unitOfWork.genreRepository.DeleteAsync(genre);
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