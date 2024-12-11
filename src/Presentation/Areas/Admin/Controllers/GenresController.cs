using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var genres = await _genresService.GetGenresAsync();
            return View(genres);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre); 
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description")] GenreModel genreModel)
        {
            await _genresService.CreateAsync(genreModel);
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description")] GenreModel genreModel)
        {
            if (id != genreModel.Id)
            {
                return NotFound();
            }

            await _genresService.UpdateAsync(id, genreModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre); 
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _genresService.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
