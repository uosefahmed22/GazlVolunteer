using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using GazlVolunteer.Core.IRepositories;
using GazlVolunteer.Core.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GazlVolunteer.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class complaintController : ControllerBase
    {
        private readonly IcomplaintRepository _complaintRepository;
        private readonly UserManager<AppUser> _userManager;

        public complaintController(IcomplaintRepository complaintRepository, UserManager<AppUser> userManager)
        {
            _complaintRepository = complaintRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// استرجاع جميع الشكاوى.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب قائمة بجميع الشكاوى المتاحة في النظام.
        /// </remarks>
        /// <response code="200">تم جلب الشكاوى بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getAllcomplaints")]
        public async Task<IActionResult> GetAllcomplaints()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _complaintRepository.GetAllcomplaints();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// استرجاع شكوى بواسطة رقم الهوية.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب تفاصيل الشكوى بناءً على رقم الهوية المقدم.
        /// </remarks>
        /// <param name="complaintId">رقم هوية الشكوى.</param>
        /// <response code="200">تم جلب الشكوى بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getcomplaintById")]
        public async Task<IActionResult> GetcomplaintById(int complaintId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _complaintRepository.GetcomplaintById(complaintId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// إضافة شكوى جديدة.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح للمستخدم بتقديم شكوى جديدة.
        /// </remarks>
        /// <param name="model">بيانات الشكوى.</param>
        /// <response code="200">تمت إضافة الشكوى بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير موجود.</response>
        [HttpPost("addcomplaint")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> Addcomplaint([FromBody] complaintModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "المستخدم غير صالح"));
            }

            var user = await _userManager.FindByEmailAsync(email);
            var result = await _complaintRepository.Add(model, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// حذف شكوى.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح للمستخدم بحذف شكوى بناءً على رقم الهوية.
        /// </remarks>
        /// <param name="complaintId">رقم هوية الشكوى.</param>
        /// <response code="200">تم حذف الشكوى بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير موجود.</response>
        [HttpDelete("deletecomplaint")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> Deletecomplaint(int complaintId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "المستخدم غير صالح"));
            }

            var user = await _userManager.FindByEmailAsync(email);
            var result = await _complaintRepository.Delete(complaintId, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }

}
