using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Services.GenreService
{
    public interface IGenreService
    {
        Task<List<GenreVM>> GetGenres();
        Task Create(CreateGenreDTO model);
        Task Update(UpdateGenreDTO model);
        Task Delete(int id);
        Task<UpdateGenreDTO> GetById(int id);
    }
}
