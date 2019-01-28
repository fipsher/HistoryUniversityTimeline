using HUDLL.Models;
using HUDLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using HU.Models;

namespace HUDLL.Repositories
{
    public interface IHistoryRepository
    {
        List<TimePeriodViewModel> GetTimePeriods();

        List<PostViewModel> GetPosts();
        PostViewModel GetPostById(int postId);
        PostViewModel GetPostByPostId(int postId);

        List<EventTypeViewModel> GetEventTypes();
        List<FigureTypeViewModel> GetFigureTypes();

        bool UpdatePost(PostViewModel post);
        bool DeletePost(int postId);
        bool AddPost(PostViewModel post);

        bool UpdateEventType(EventTypeViewModel eventTypeViewModel);
        bool DeleteEventType(int eventTypeId);
        EventTypeViewModel GetEventTypeById(int eventTypeId);

        bool UpdateFigureType(FigureTypeViewModel figureTypeViewModel);
        bool DeleteFigureType(int figureTypeId);
        FigureTypeViewModel GetFigureTypeById(int figureTypeId);

        bool Authenticate(string username, string password);
    }
}
