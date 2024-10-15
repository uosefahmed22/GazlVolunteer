using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.IRepositories
{
    public interface INotificationsRepository
    {
        Task<ApiResponse> AddNotification(string userId,NotificationsDto model);
        Task<ApiResponse> GetNotificationsForUser(string UserId);
        Task<ApiResponse> MarkNotificationAsRead(int notificationId, string userId);
        Task<ApiResponse> DeleteNotification(int notificationId);
        Task<ApiResponse> getById(int notificationId);
    }
}
