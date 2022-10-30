using AutoMapper;
using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using HS4_Blog_Project.Application.Services.GenreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetGenres());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateGenreDTO model)
        {
            if (ModelState.IsValid)
            {
                _genreService.Create(model);
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _genreService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO model)
        {
            await _genreService.Update(model);

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _genreService.Delete(id);

            return RedirectToAction("index");
        }
    }
}
