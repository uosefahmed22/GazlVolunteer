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
            var result = await _volunteerOpportunityRepository.AddVolunteerOpportunity(model,user.Id);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
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
