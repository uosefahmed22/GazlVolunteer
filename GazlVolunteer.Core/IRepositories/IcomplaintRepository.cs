using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.IRepositories
{
    public interface IcomplaintRepository
    {
        Task<ApiResponse> Add(complaintModelDto model, string UserId);
        Task<ApiResponse> Delete(int complaintId, string UserId);
        Task<ApiResponse> GetAllcomplaints();
        Task<ApiResponse> GetcomplaintById(int complaintId);
    }
}
