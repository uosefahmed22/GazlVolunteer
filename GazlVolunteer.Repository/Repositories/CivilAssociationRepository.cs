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
    public class CivilAssociationRepository : ICivilAssociationRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CivilAssociationRepository(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Add(CivilAssociationsDto model)
        {
            try
            {
                if (model == null)
                {
                    return new ApiResponse(400, "البيانات المدخلة غير صحيحة");
                }
                var CivilAssociation = _mapper.Map<CivilAssociations>(model);
                await _dbContext.CivilAssociations.AddAsync(CivilAssociation);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> Delete(int CivilAssociationId)
        {
            try
            {
                var CivilAssociation = await _dbContext.CivilAssociations.FindAsync(CivilAssociationId);
                if (CivilAssociation == null)
                {
                    return new ApiResponse(404, "الجمعية الاهليه غير موجودة");
                }
                _dbContext.CivilAssociations.Remove(CivilAssociation);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم الحذف بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> Get(int CivilAssociationId)
        {
            try
            {
                var CivilAssociation = await _dbContext.CivilAssociations.FindAsync(CivilAssociationId);
                if (CivilAssociation == null)
                {
                    return new ApiResponse(404, "الجمعية الاهليه غير موجودة");
                }
                var result = await _dbContext.CivilAssociations.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Contacts,
                    x.Latitude,
                    x.Longitude
                }).FirstOrDefaultAsync();
                return new ApiResponse(200, result);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllCivilAssociations()
        {
            try
            {
                var CivilAssociations = await _dbContext.CivilAssociations.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Contacts,
                    x.Latitude,
                    x.Longitude
                }).ToListAsync();
                return new ApiResponse(200, CivilAssociations);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> Update(int CivilAssociationId, CivilAssociationsDto model)
        {
            try
            {
                var CivilAssociation = await _dbContext.CivilAssociations.FindAsync(CivilAssociationId);
                if (CivilAssociation == null)
                {
                    return new ApiResponse(404, "الجمعية الاهليه غير موجودة");
                }
                _mapper.Map(model, CivilAssociation);
                _dbContext.CivilAssociations.Update(CivilAssociation);
                await _dbContext.SaveChangesAsync();

                return new ApiResponse(200, "تم التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }

    }
}
