using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.IRepositories
{
    public interface ICivilAssociationRepository
    {
        Task<ApiResponse> Add(CivilAssociationsDto model);
        Task<ApiResponse> Update(int CivilAssociationId, CivilAssociationsDto model);
        Task<ApiResponse> Delete(int CivilAssociationId);
        Task<ApiResponse> Get(int CivilAssociationId);
        Task<ApiResponse> GetAllCivilAssociations();
    }
}
