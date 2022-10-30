using AutoMapper;
using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using HS4_Blog_Project.Domain.Entities;
using HS4_Blog_Project.Domain.Enum;
using HS4_Blog_Project.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task Create(CreateGenreDTO model)
        {
           Genre genre = _mapper.Map<Genre>(model);
            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            genre.Status = Status.Passive;
            genre.DeleteDate = DateTime.Now;
            await _genreRepository.Delete(genre);
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateGenreDTO>(genre);
            return model;
        }

        public async Task<List<GenreVM>> GetGenres()
        {
            var genres = await _genreRepository.GetFilteredList(
                selector: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                expression: x => x.Status != Status.Passive,
                orderby: x=> x.OrderBy(x=>x.Name)
                );
            return genres;
        }

        public async Task Update(UpdateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);
            await _genreRepository.Update(genre);
        }
    }
}
