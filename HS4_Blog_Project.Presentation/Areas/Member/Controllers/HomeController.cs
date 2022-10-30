using HS4_Blog_Project.Application.Services.PostService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Presentation.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        //Üyelerin postlarının gösterildiği sayfa
        public async Task<IActionResult> Index() // member/home/index
        {
            return View(await _postService.GetPostsForMembers());
        }
    }
}
