using AutoMapper;
using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using HS4_Blog_Project.Domain.Entities;
using HS4_Blog_Project.Domain.Enum;
using HS4_Blog_Project.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public PostService(IPostRepository postRepository, IMapper mapper, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task Create(CreatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 500));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = $"/images/defaultpost.jpg";
            }
            await _postRepository.Create(post);
        }

        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepository.GetFilteredList(
                    selector: x => new GenreVM()
                    {
                        Id = x.Id,
                        Name = x.Name
                    },
                    expression: x => x.Status != Status.Passive,
                    orderby: x => x.OrderBy(x => x.Name)),
                Authors = await _authorRepository.GetFilteredList(
                    selector: x => new AuthorVM()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                    },
                    expression: x => x.Status != Status.Passive,
                    orderby: x => x.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
            };
            return model;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);
            post.Status = Status.Passive;
            post.DeleteDate = DateTime.Now;
            await _postRepository.Delete(post);
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                selector: x => new PostVM
                {
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    GenreId = x.GenreId,
                    AuthorId = x.AuthorId
                },
                expression: x => x.Id == id);
            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepository.GetFilteredList(
                selector: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                expression: x => x.Status != Status.Passive,
                orderby: x => x.OrderBy(x => x.FirstName));

            model.Genres = await _genreRepository.GetFilteredList(
                selector: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                expression: x => x.Status != Status.Passive,
                orderby: x => x.OrderBy(x=>x.Name));
            return model;

        }

        public Task<PostDetailsVM> GetPostDetailsVM(int id)
        {
            var post = _postRepository.GetFilteredFirstOrDefault(
                selector: x => new PostDetailsVM
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title
                },
                expression: x => x.Id == id,
                orderby: null,
                include: x => x.Include(x => x.Author)
                );

            return post;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepository.GetFilteredList(
                selector: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName
                },
                expression: x => x.Status != Status.Passive,
                orderby: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre)
                              .Include(x => x.Author)
                );
            return posts;
        }

        public async Task<List<GetPostsVM>> GetPostsForMembers()
        {
            var posts = await _postRepository.GetFilteredList(
                selector: x => new GetPostsVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title,
                    Id = x.Id,
                    AuthorImagePath = x.Author.ImagePath
                },
                expression: x=>x.Status != Status.Passive,
                orderby: x=> x.OrderByDescending(x=>x.CreateDate),
                include: x=> x.Include(x =>x.Author)
                );

            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath !=null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = model.ImagePath;
            }

            await _postRepository.Update(post);
        }
    }
}
