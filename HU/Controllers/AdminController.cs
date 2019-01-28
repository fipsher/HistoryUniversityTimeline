
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HU.Models;
using HUDLL.Repositories;
using HUDLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HU.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IHistoryRepository _historyRepository;
     

        public AdminController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public ActionResult Authenticate()
        {
            return View(new UserViewModel());
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult Authenticate(UserViewModel loginModel)
        {
            try
            {
                var isUserExist = _historyRepository.Authenticate(loginModel.Username, loginModel.Password);
                Set("Token", AppSettings.TokenKey, 20);

                if (isUserExist)
                {
                    return RedirectToAction( "AdminPosts", "Admin");
                }

                ViewData["ErrorMessage"] = "Неправильний логін чи пароль.";

                return View(loginModel);
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult LogOut()
        {
            Remove("Token");
            return RedirectToAction("AdminPosts", "Admin");
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult AdminEventTypes()
        {
            var adminEventTypesViewModel = new AdminEventTypesViewModel
            {
                EventTypes = _historyRepository.GetEventTypes()
            };
            return View(adminEventTypesViewModel);
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult AdminFigureTypes()
        {
            var adminFigureTypesViewModel = new AdminFigureTypesViewModel
            {
                FigureTypes = _historyRepository.GetFigureTypes()
            };
            return View(adminFigureTypesViewModel);
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult UpdateEventType(int? eventTypeId)
        {
            if (eventTypeId == null)
            {
                return PartialView(new EventTypeViewModel());
            }

            var eventType = _historyRepository.GetEventTypeById(eventTypeId.Value);
            if (eventType == null)
            {
                return Json(new {success = false});
            }

            return PartialView(eventType);
        }

        [HttpPost("[action]")]
        [HU]
        public ActionResult UpdateEventType(EventTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var isEventTypeUpdated = _historyRepository.UpdateEventType(model);
            return Json(new {success = isEventTypeUpdated});
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult UpdateFigureType(int? figureTypeId)
        {
            if (figureTypeId == null)
            {
                return PartialView(new FigureTypeViewModel());
            }

            var figureType = _historyRepository.GetFigureTypeById(figureTypeId.Value);
            if (figureType == null)
            {
                return Json(new {success = false});
            }

            return PartialView(figureType);
        }

        [HttpPost("[action]")]
        [HU]
        public ActionResult UpdateFigureType(FigureTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var isFigureTypeUpdated = _historyRepository.UpdateFigureType(model);
            return Json(new {success = isFigureTypeUpdated});
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult DeleteEventType(int eventTypeId)
        {
            try
            {
                return Json(new {success = _historyRepository.DeleteEventType(eventTypeId)});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult DeleteFigureType(int figureTypeId)
        {
            try
            {
                return Json(new {success = _historyRepository.DeleteFigureType(figureTypeId)});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult AdminPosts()
        {
            var adminPostsViewModel = new AdminPostsListViewModel
            {
                AdminPosts = _historyRepository.GetPosts()
            };
            return View(adminPostsViewModel);
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult AddPost()
        {
            var addPostViewModel = new UpdatePostViewModel
            {
                PeriodsList = _historyRepository.GetTimePeriods().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.TimePeriodId.ToString()
                }).ToList(),
                FigureTypes = _historyRepository.GetFigureTypes().Select(p => new SelectListItem
                {
                    Text = p.FigureTypeName,
                    Value = p.FigureTypeId.ToString()
                }).ToList(),
                EventTypes = _historyRepository.GetEventTypes().Select(p => new SelectListItem
                {
                    Text = p.EventTypeName,
                    Value = p.EventTypeId.ToString()
                }).ToList()
            };

            return PartialView(addPostViewModel);
        }

        [HttpPost("[action]")]
        [HU]
        public ActionResult AddPost(UpdatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var image = model.Image;
            byte[] fileBytes = { };
            if (model.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            var isPostCreated = _historyRepository.AddPost(new PostViewModel
            {
                Title = model.Title,
                Content = model.Content,
                PostDate = model.PostDate,
                TimePeriodId = model.SelectedPeriodId,
                EventTypes = model.SelectedEventTypes,
                FigureTypes = model.SelectedFigureTypes,
                Image = Convert.ToBase64String(fileBytes),
            });
            return Json(new {success = isPostCreated});
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult DeletePost(int postId)
        {
            try
            {
                return Json(new {success = _historyRepository.DeletePost(postId)});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
        }

        [HttpGet("[action]")]
        [HU]
        public ActionResult UpdatePost(int postId)
        {
            var post = _historyRepository.GetPostById(postId);
            if (post == null)
            {
                return Json(new {success = false});
            }

            var addPostViewModel = new UpdatePostViewModel
            {
                PeriodsList = _historyRepository.GetTimePeriods().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.TimePeriodId.ToString()
                }).ToList(),
                FigureTypes = _historyRepository.GetFigureTypes().Select(p => new SelectListItem
                {
                    Text = p.FigureTypeName,
                    Value = p.FigureTypeId.ToString()
                }).ToList(),
                EventTypes = _historyRepository.GetEventTypes().Select(p => new SelectListItem
                {
                    Text = p.EventTypeName,
                    Value = p.EventTypeId.ToString()
                }).ToList(),
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                SelectedPeriodId = post.TimePeriodId,
                PostDate = post.PostDate,
                SelectedEventTypes = post.EventTypes,
                SelectedFigureTypes = post.FigureTypes
            };

            return PartialView(addPostViewModel);
        }

        [HttpPost("[action]")]
        [HU]
        public ActionResult UpdatePost(UpdatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var image = model.Image;
            byte[] fileBytes = {};
            if (model.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            var isPostUpdated = _historyRepository.UpdatePost(new PostViewModel
            {
                PostId = model.PostId,
                Title = model.Title,
                Content = model.Content,
                PostDate = model.PostDate,
                TimePeriodId = model.SelectedPeriodId,
                EventTypes = model.SelectedEventTypes ?? new List<int>(),
                FigureTypes = model.SelectedFigureTypes ?? new List<int>(),
                Image = Convert.ToBase64String(fileBytes)
            });
            return Json(new {success = isPostUpdated});
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}
