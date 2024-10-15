using AutoMapper;
using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using GazlVolunteer.Core.IRepositories;
using GazlVolunteer.Core.Models;
using GazlVolunteer.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Repository.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public NotificationsRepository(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddNotification(string userId, NotificationsDto model)
        {
            try
            {
                var notification = _mapper.Map<Notifications>(model);
                notification.UserId = userId;
                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم اضافة الاشعار بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteNotification(int notificationId)
        {
            try
            {
                var notification = await _dbContext.Notifications.FindAsync(notificationId);
                if (notification == null)
                {
                    return new ApiResponse(404, "الاشعار غير موجود");
                }
                _dbContext.Notifications.Remove(notification);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم حذف الاشعار بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> getById(int notificationId)
        {
            try
            {
                var notification = await _dbContext
                    .Notifications
                    .Where(c => c.Id == notificationId)
                    .Select(c => new {
                        c.NotificationMessage,
                        c.Date,
                        c.AppUser.FullName,
                        c.AppUser.Email,
                        c.AppUser.ImageUrl
                    }).FirstOrDefaultAsync();
                if (notification == null)
                {
                    return new ApiResponse(404, "الاشعار غير موجود");
                }
                return new ApiResponse(200, notification);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> GetNotificationsForUser(string UserId)
        {
            try
            {
                var notifications = await _dbContext
                    .Notifications
                    .Where(c => c.UserId == UserId)
                    .Select(c => new
                    {
                        c.Id,
                        c.NotificationMessage,
                        c.Date,
                        c.IsRead,
                        c.AppUser.FullName,
                        c.AppUser.Email,
                        c.AppUser.ImageUrl
                    }).ToListAsync();
                return new ApiResponse(200, notifications);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> MarkNotificationAsRead(int notificationId, string userId)
        {
            try
            {
                var notification = await _dbContext
                    .Notifications
                    .Where(c => c.Id == notificationId && c.UserId == userId)
                    .FirstOrDefaultAsync();
                if (notification == null)
                {
                    return new ApiResponse(404, "الاشعار غير موجود");
                }
                notification.MarkAsRead();
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم تحديث حالة الاشعار بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
    }
}
