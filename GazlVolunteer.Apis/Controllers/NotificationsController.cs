using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using GazlVolunteer.Core.IRepositories;
using GazlVolunteer.Core.Models.Auth;
using GazlVolunteer.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GazlVolunteer.Apis.Controllers
{
    public class NotificationsController : BaseApiController
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly UserManager<AppUser> _userManager;

        public NotificationsController(INotificationsRepository notificationsRepository, UserManager<AppUser> userManager)
        {
            _notificationsRepository = notificationsRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// استرجاع الإشعارات الخاصة بالمستخدم.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب جميع الإشعارات المتعلقة بالمستخدم الذي قام بتسجيل الدخول.
        /// </remarks>
        /// <response code="200">تم جلب الإشعارات بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير صحيح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetNotificationsForUser")]
        public async Task<IActionResult> GetNotificationsForUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid user"));
            }
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _notificationsRepository.GetNotificationsForUser(user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// استرجاع إشعار محدد.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب تفاصيل إشعار معين باستخدام معرف الإشعار.
        /// </remarks>
        /// <response code="200">تم جلب الإشعار بنجاح.</response>
        /// <response code="400">طلب غير صالح أو الإشعار غير موجود.</response>
        [HttpGet("getNotification")]
        public async Task<IActionResult> GetNotification(int notificationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _notificationsRepository.getById(notificationId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// إضافة إشعار جديد.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بإضافة إشعار جديد للمستخدم الحالي.
        /// </remarks>
        /// <response code="200">تم إضافة الإشعار بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير صحيح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("addNotification")]
        public async Task<IActionResult> AddNotification([FromBody] NotificationsDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid user"));
            }
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _notificationsRepository.AddNotification(user.Id, model);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// حذف إشعار.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بحذف إشعار معين باستخدام معرف الإشعار.
        /// </remarks>
        /// <response code="200">تم حذف الإشعار بنجاح.</response>
        /// <response code="400">طلب غير صالح أو الإشعار غير موجود.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpDelete("deleteNotification")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _notificationsRepository.DeleteNotification(notificationId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// تعليم إشعار كمقروء.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بتعليم إشعار معين كمقروء للمستخدم الحالي.
        /// </remarks>
        /// <response code="200">تم تعليم الإشعار كمقروء بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير صحيح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut("markNotificationAsRead")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid user"));
            }
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _notificationsRepository.MarkNotificationAsRead(notificationId, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
