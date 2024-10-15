using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.IRepositories
{
    public interface IVolunteerRepository
    {
        Task<ApiResponse> Add(VolunteerModelDto model, string UserId);
        Task<ApiResponse> Delete(int VolunteerId, string UserId);
        Task<ApiResponse> GetAllVolunteers();
        Task<ApiResponse> GetVolunteerById(int VolunteerId);
        Task<ApiResponse> Update(VolunteerModelDto model, string UserId, int VolunteerId);
    }
}
