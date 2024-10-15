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
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly UserManager<AppUser> _userManager;

        public VolunteerController(IVolunteerRepository volunteerRepository,UserManager<AppUser> userManager)
        {
            _volunteerRepository = volunteerRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// جلب جميع المتطوعين.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب قائمة بجميع المتطوعين.
        /// </remarks>
        /// <response code="200">تم جلب المتطوعين بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getAllVolunteers")]
        public async Task<IActionResult> GetAllVolunteers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _volunteerRepository.GetAllVolunteers();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        /// <summary>
        /// جلب متطوع معين.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب بيانات متطوع بناءً على رقم الهوية الخاص به.
        /// </remarks>
        /// <param name="VolunteerId">رقم هوية المتطوع.</param>
        /// <response code="200">تم جلب بيانات المتطوع بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getVolunteer")]
        public async Task<IActionResult> GetVolunteer(int VolunteerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _volunteerRepository.GetVolunteerById(VolunteerId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        /// <summary>
        /// إضافة متطوع جديد.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح بإضافة متطوع جديد باستخدام بيانات النموذج المرسل.
        /// </remarks>
        /// <param name="model">بيانات المتطوع الجديد.</param>
        /// <response code="200">تمت إضافة المتطوع بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير موجود.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("addVolunteer")]
        public async Task<IActionResult> AddVolunteer([FromBody] VolunteerModelDto model)
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
            var result = await _volunteerRepository.Add(model, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        /// <summary>
        /// تحديث بيانات المتطوع.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح بتحديث بيانات متطوع موجود بناءً على رقم الهوية.
        /// </remarks>
        /// <param name="model">بيانات المتطوع المحدثة.</param>
        /// <param name="VolunteerId">رقم هوية المتطوع.</param>
        /// <response code="200">تم تحديث بيانات المتطوع بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير موجود.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut("updateVolunteer")]
        public async Task<IActionResult> UpdateVolunteer([FromBody] VolunteerModelDto model, int VolunteerId)
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
            var result = await _volunteerRepository.Update(model, user.Id, VolunteerId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        /// <summary>
        /// حذف متطوع.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح بحذف متطوع بناءً على رقم الهوية الخاص به.
        /// </remarks>
        /// <param name="VolunteerId">رقم هوية المتطوع.</param>
        /// <response code="200">تم حذف المتطوع بنجاح.</response>
        /// <response code="400">طلب غير صالح أو المستخدم غير موجود.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpDelete("deleteVolunteer")]
        public async Task<IActionResult> DeleteVolunteer(int VolunteerId)
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
            var result = await _volunteerRepository.Delete(VolunteerId, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
