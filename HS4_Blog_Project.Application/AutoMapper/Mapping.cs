using AutoMapper;
using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using HS4_Blog_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Author, AuthorDetailVM>().ReverseMap();

            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostsVM>().ReverseMap();

            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();
        }
    }
}
