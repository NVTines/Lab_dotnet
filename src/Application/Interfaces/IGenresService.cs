using Domain.Models;

namespace Application.Interfaces
{
    public interface IGenresService
    {
        Task<List<GenreModel>> GetGenresAsync();
        Task<GenreModel?> GetGenreByIdAsync(int id);
        Task<GenreModel?> GetGenreByNameAsync(string name);
        Task CreateAsync(GenreModel genreModel);
        Task UpdateAsync(int id, GenreModel genreModel);
        Task DeleteAsync(int id);
    }
}
