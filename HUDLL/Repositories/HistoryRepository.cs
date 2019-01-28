using System;
using System.Collections.Generic;
using System.Linq;
using HU.Models;
using HUDLL.Models;
using HUDLL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HUDLL.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryContext _context;

        private List<UserViewModel> _users = new List<UserViewModel>
        {
            new UserViewModel { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public HistoryRepository(HistoryContext context)
        {
            _context = context;
        }

        public List<TimePeriodViewModel> GetTimePeriods()
        {
            try
            {
                return _context.TimePeriods.Select(p => p.MapToViewModel()).OrderBy(p => p.StartDate).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<PostViewModel> GetPosts()
        {
            try
            {
                return _context.Posts
                    .OrderBy(p => p.PostDate)
                    .Include(x => x.PostEventTypes)
                    .Include(x => x.PostFigures)
                    .Include(x => x.TimePeriod)
                    .Select(p => p.MapToViewModel())
                    .ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public PostViewModel GetPostByPostId(int id)
        {
            try
            {
                return _context.Posts
                    .SingleOrDefault(p => p.PostId == id).MapToViewModelById();            
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<EventTypeViewModel> GetEventTypes()
        {
            try
            {
                return _context.EventTypes.Select(e => new EventTypeViewModel
                {
                    EventTypeId = e.EventTypeId,
                    EventTypeName = e.EventTypeName
                }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<FigureTypeViewModel> GetFigureTypes()
        {
            try
            {
                return _context.Figures.Select(e => new FigureTypeViewModel
                {
                    FigureTypeId = e.FigureId,
                    FigureTypeName = e.FigureType
                }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddPost(PostViewModel postViewModel)
        {
            try
            {
                var post = new Post
                {
                    Title = postViewModel.Title,
                    Content = postViewModel.Content,
                    TimePeriodId = postViewModel.TimePeriodId,
                    PostDate = postViewModel.PostDate,
                    MediaContent = postViewModel.Image
                };

                if (postViewModel.FigureTypes != null)
                {
                    post.PostFigures = new List<PostFigure>(postViewModel.FigureTypes.Select(GetFigure).ToList()
                        .Select(p => new PostFigure
                        {
                            Post = post,
                            Figure = p
                        }));
                }

                if (postViewModel.EventTypes != null)
                {
                    post.PostEventTypes = new List<PostEventType>(postViewModel.EventTypes.Select(GetEventType).ToList()
                        .Select(p => new PostEventType
                        {
                            Post = post,
                            EventType = p
                        }));
                }

                _context.Posts.Add(post);

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateEventType(EventTypeViewModel eventTypeViewModel)
        {
            try
            {
                var eventType =
                    _context.EventTypes.FirstOrDefault(e => e.EventTypeId == eventTypeViewModel.EventTypeId);

                if (eventType == null)
                {
                    _context.EventTypes.Add(new EventType
                    {
                        EventTypeName = eventTypeViewModel.EventTypeName
                    });
                }
                else
                {
                    eventType.EventTypeName = eventTypeViewModel.EventTypeName;
                    _context.EventTypes.Update(eventType);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteEventType(int eventTypeId)
        {
            try
            {
                var eventType = _context.EventTypes.FirstOrDefault(e => e.EventTypeId == eventTypeId);

                if (eventType == null)
                {
                    return false;
                }

                _context.EventTypes.Remove(eventType);

                if (eventType.PostEventTypes != null)
                {
                    _context.PostEventTypes.RemoveRange(eventType.PostEventTypes);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFigureType(FigureTypeViewModel figureTypeViewModel)
        {
            try
            {
                var figureType = _context.Figures.FirstOrDefault(e => e.FigureId == figureTypeViewModel.FigureTypeId);

                if (figureType == null)
                {
                    _context.Figures.Add(new Figure
                    {
                        FigureType = figureTypeViewModel.FigureTypeName
                    });
                }
                else
                {
                    figureType.FigureType = figureTypeViewModel.FigureTypeName;

                    _context.Figures.Update(figureType);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteFigureType(int figureTypeId)
        {
            try
            {
                var figure = _context.Figures.FirstOrDefault(e => e.FigureId == figureTypeId);

                if (figure == null)
                {
                    return false;
                }

                _context.Figures.Remove(figure);

                if (figure.PostFigures != null)
                {
                    _context.PostFigures.RemoveRange(figure.PostFigures);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePost(PostViewModel postViewModel)
        {
            try
            {

                var post = _context.Posts
                    .Include(p => p.PostEventTypes)
                    .Include(p => p.PostFigures)
                    .FirstOrDefault(p => p.PostId == postViewModel.PostId);
                if (post == null)
                {
                    return false;
                }

                post.Title = postViewModel.Title;
                post.Content = postViewModel.Content;
                post.TimePeriodId = postViewModel.TimePeriodId;
                post.PostDate = postViewModel.PostDate;
                post.MediaContent = postViewModel.Image;

                if (postViewModel.FigureTypes != null)
                {
                    post.PostFigures = new List<PostFigure>(postViewModel.FigureTypes.Select(GetFigure)
                        .Select(figure => new PostFigure
                        {
                            Post = post,
                            Figure = figure
                        }));
                }

                if (postViewModel.EventTypes != null)
                {
                    post.PostEventTypes = new List<PostEventType>(postViewModel.EventTypes.Select(GetEventType)
                        .Select(eventType => new PostEventType
                        {
                            Post = post,
                            EventType = eventType
                        }));

                }

                _context.Posts.Update(post);

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePost(int postId)
        {
            try
            {
                var post = _context.Posts.Include(p => p.PostEventTypes).Include(p => p.PostFigures)
                    .FirstOrDefault(p => p.PostId == postId);
                if (post == null)
                {
                    return false;
                }

                _context.Posts.Remove(post);


                if (post.PostEventTypes != null)
                {
                    _context.PostEventTypes.RemoveRange(post.PostEventTypes);
                }

                if (post.PostFigures != null)
                {
                    _context.PostFigures.RemoveRange(post.PostFigures);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PostViewModel GetPostById(int postId)
        {
            try
            {
                return _context.Posts
                    .Include(x => x.PostEventTypes)
                    .Include(x => x.PostFigures)
                    .Include(x => x.TimePeriod)
                    .FirstOrDefault(p => p.PostId == postId)
                    .MapToViewModel();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EventTypeViewModel GetEventTypeById(int eventTypeId)
        {
            try
            {

                var eventType = _context.EventTypes.FirstOrDefault(e => e.EventTypeId == eventTypeId);

                if (eventType == null)
                {
                    return null;
                }

                return new EventTypeViewModel
                {
                    EventTypeId = eventType.EventTypeId,
                    EventTypeName = eventType.EventTypeName
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FigureTypeViewModel GetFigureTypeById(int figureTypeId)
        {
            try
            {

                var figureType = _context.Figures.FirstOrDefault(e => e.FigureId == figureTypeId);

                if (figureType == null)
                {
                    return null;
                }

                return new FigureTypeViewModel
                {
                    FigureTypeId = figureType.FigureId,
                    FigureTypeName = figureType.FigureType
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            return user != null;
        }

        private EventType GetEventType(int eventTypeId)
        {
            try
            {
                return _context.EventTypes.FirstOrDefault(p => p.EventTypeId == eventTypeId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Figure GetFigure(int figureId)
        {
            try
            {
                return _context.Figures.FirstOrDefault(p => p.FigureId == figureId);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }

    public static class Mapper
    {
        public static PostViewModel MapToViewModel(this Post post)
        {
          return new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Content,
                Content = post.Content,
                TimePeriodId = post.TimePeriodId,
                PeriodName = post.TimePeriod.Name,
                PostDate = post.PostDate,
                Image = post.MediaContent,
                EventTypes = post.PostEventTypes.Select(p => p.EventTypeId).ToList(),
                FigureTypes = post.PostFigures.Select(p => p.FigureId).ToList()
            };
        }

        public static PostViewModel MapToViewModelById(this Post post)
        {
            return new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Image = post.MediaContent
            };
        }

        public static TimePeriodViewModel MapToViewModel(this TimePeriod period)
        {
            return new TimePeriodViewModel
            {
                TimePeriodId = period.TimePeriodId,
                StartDate = period.StartDate,
                EndDate = period.EndDate ?? DateTime.Now,
                Name = period.Name
            };
        }
    }
}
