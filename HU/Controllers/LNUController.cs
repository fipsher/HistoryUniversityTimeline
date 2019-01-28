using System;
using System.Collections.Generic;
using HU.Models;
using HUDLL.Repositories;
using HUDLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HU.Controllers
{
    [Route("api/[controller]")]
    public class LnuController : Controller
    {
        private readonly IHistoryRepository _repository;

        public LnuController(IHistoryRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("[action]")]
        public IActionResult PostsByPeriod()
        {
            try
            {
                var getPostsViewModel = new PostsByPeriodViewModel
                {
                    PostsByPeriod = _repository.GetPosts(),
                    Periods = _repository.GetTimePeriods(),
                    EventTypes = _repository.GetEventTypes(),
                    FigureTypes = _repository.GetFigureTypes()
                };
                
                return View(getPostsViewModel);
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpGet("[action]")]
        public ActionResult PostsById(int postId)
        {
            try
            {
                return Json(_repository.GetPostByPostId(postId));
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpGet("[action]/")]
        public IEnumerable<TimePeriodViewModel> GetTimePeriods()
        {
            try
            {
                return _repository.GetTimePeriods();
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPost("[action]")]
        public bool AddPost(PostViewModel postViewModel)
        {
            try
            {
                return _repository.AddPost(postViewModel);
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
