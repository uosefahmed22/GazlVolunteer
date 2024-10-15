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
    public class VolunteerOpportunityController : BaseApiController
    {
        private readonly IVolunteerOpportunityRepository _volunteerOpportunityRepository;
        private readonly UserManager<AppUser> _userManager;

        public VolunteerOpportunityController(IVolunteerOpportunityRepository volunteerOpportunityRepository,UserManager<AppUser> userManager)
        {
            _volunteerOpportunityRepository = volunteerOpportunityRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// استرجاع جميع الفرص التطوعية.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب جميع الفرص التطوعية المتاحة.
        /// </remarks>
        /// <response code="200">تم استرجاع الفرص التطوعية بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getAllVolunteerOpportunities")]
        public async Task<IActionResult> GetAllVolunteerOpportunities()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _volunteerOpportunityRepository.GetAllVolunteerOpportunities();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// استرجاع فرصة تطوعية معينة.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب تفاصيل فرصة تطوعية معينة باستخدام معرف الفرصة التطوعية.
        /// </remarks>
        /// <response code="200">تم استرجاع الفرصة التطوعية بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getVolunteerOpportunity")]
        public async Task<IActionResult> GetVolunteerOpportunity(int VolunteerOpportunityId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _volunteerOpportunityRepository.getVolunteerOpportunity(VolunteerOpportunityId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// إضافة فرصة تطوعية جديدة.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح للمستخدم بإضافة فرصة تطوعية جديدة.
        /// </remarks>
        /// <response code="200">تم إضافة الفرصة التطوعية بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("addVolunteerOpportunity")]
        public async Task<IActionResult> AddVolunteerOpportunity([FromBody] VolunteerOpportunityModelDto model)
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
            var result = await _volunteerOpportunityRepository.AddVolunteerOpportunity(model, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// تحديث فرصة تطوعية.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح للمستخدم بتحديث فرصة تطوعية معينة.
        /// </remarks>
        /// <response code="200">تم تحديث الفرصة التطوعية بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut("updateVolunteerOpportunity")]
        public async Task<IActionResult> UpdateVolunteerOpportunity([FromBody] VolunteerOpportunityModelDto model, int VolunteerOpportunityId)
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
            var result = await _volunteerOpportunityRepository.UpdateVolunteerOpportunity(model, VolunteerOpportunityId, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// حذف فرصة تطوعية.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يسمح للمستخدم بحذف فرصة تطوعية معينة.
        /// </remarks>
        /// <response code="200">تم حذف الفرصة التطوعية بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpDelete("deleteVolunteerOpportunity")]
        public async Task<IActionResult> DeleteVolunteerOpportunity(int VolunteerOpportunityId)
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
            var result = await _volunteerOpportunityRepository.DeleteVolunteerOpportunity(VolunteerOpportunityId, user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
