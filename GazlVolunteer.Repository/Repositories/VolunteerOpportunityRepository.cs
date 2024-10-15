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
    public class VolunteerOpportunityRepository : IVolunteerOpportunityRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public VolunteerOpportunityRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddVolunteerOpportunity(VolunteerOpportunityModelDto model, string UserId)
        {
            try
            {
                var volunteerOpportunity = _mapper.Map<VolunteerOpportunityModel>(model);
                volunteerOpportunity.UserId = UserId;
                await _dbContext.VolunteerOpportunities.AddAsync(volunteerOpportunity);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200,"تم اضافة الفرصة بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllVolunteerOpportunities()
        {
            try
            {
                var volunteerOpportunities = await _dbContext.VolunteerOpportunities.Select(x => new
                {
                    x.name,
                    x.Numbers,
                    x.Time,
                    x.Age,
                    x.Date,

                }).ToListAsync();
                return new ApiResponse(200, "تم جلب البيانات بنجاح", volunteerOpportunities);
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> getVolunteerOpportunity(int VolunteerOpportunityId)
        {
            try
            {
                var volunteerOpportunity = await _dbContext.VolunteerOpportunities.Where(x => x.Id == VolunteerOpportunityId).Select(x => new
                {
                    x.name,
                    x.Numbers,
                    x.Time,
                    x.Age,
                    x.Date,
                }).FirstOrDefaultAsync();
                if (volunteerOpportunity == null)
                {
                    return new ApiResponse(404, "الفرصة غير موجودة");
                }
                return new ApiResponse(200, "تم جلب البيانات بنجاح", volunteerOpportunity);
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateVolunteerOpportunity(VolunteerOpportunityModelDto model, int VolunteerOpportunityId, string UserId)
        {
            try
            {
                var volunteerOpportunity = await _dbContext.VolunteerOpportunities.FirstOrDefaultAsync(x => x.Id == VolunteerOpportunityId&&x.UserId==UserId);
                if (volunteerOpportunity == null)
                {
                    return new ApiResponse(404, "الفرصة غير موجودة");
                }
                _mapper.Map(model, volunteerOpportunity);
                _dbContext.VolunteerOpportunities.Update(volunteerOpportunity);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم تعديل الفرصة بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteVolunteerOpportunity(int VolunteerOpportunityId, string UserId)
        {
            try
            {
                var volunteerOpportunity = await _dbContext.VolunteerOpportunities.FirstOrDefaultAsync(x => x.Id == VolunteerOpportunityId&&x.UserId==UserId);
                if (volunteerOpportunity == null)
                {
                    return new ApiResponse(404, "الفرصة غير موجودة");
                }
                _dbContext.VolunteerOpportunities.Remove(volunteerOpportunity);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم حذف الفرصة بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }

    }
}
