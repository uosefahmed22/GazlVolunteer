using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.IRepositories
{
    public interface IVolunteerOpportunityRepository
    {
        Task<ApiResponse> AddVolunteerOpportunity(VolunteerOpportunityModelDto model, string UserId);
        Task<ApiResponse> DeleteVolunteerOpportunity(int VolunteerOpportunityId, string UserId);
        Task<ApiResponse> GetAllVolunteerOpportunities();
        Task<ApiResponse> getVolunteerOpportunity(int VolunteerOpportunityId);
        Task<ApiResponse> UpdateVolunteerOpportunity(VolunteerOpportunityModelDto model, int VolunteerOpportunityId, string UserId);
    }
}
