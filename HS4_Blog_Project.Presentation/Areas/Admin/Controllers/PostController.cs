using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Models.VMs;
using HS4_Blog_Project.Application.Services.PostService;
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
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        //public async Task<IActionResult> List()
        //{
        //    List<PostVM> model = await _postService.GetPosts();

        //    return View(model);
        //}

        public async Task<IActionResult> List() => View(await _postService.GetPosts());
        public async Task<IActionResult> Create() => View(await _postService.CreatePost()); //CreatePost metodu dropdown menuleri dolduruyor (Author Genre) ve UI'a  bir CreatePostDTO objesi veriyor
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(model);
                TempData["Success"] = "Post has been created!";

                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Post has not been created!";
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id) => View(await _postService.GetPostDetailsVM(id));

        public async Task<IActionResult> Update(int id) => View(await _postService.GetById(id));
        public async Task<IActionResult> Update(UpdatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                TempData["Success"] = "Post has been updated!";

                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Post has not been updated!";

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);

            return RedirectToAction("List");
        }
    }
}
