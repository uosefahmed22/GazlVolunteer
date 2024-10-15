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
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public VolunteerRepository(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Add(VolunteerModelDto model, string UserId)
        {
            try
            {
                var volunteer = _mapper.Map<VolunteerModel>(model);
                volunteer.UserId = UserId;
                await _dbContext.Volunteers.AddAsync(volunteer);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200,"تم اضافة المتطوع بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400,ex.Message);
            }
        }
        public async Task<ApiResponse> Delete(int VolunteerId, string UserId)
        {
            try
            {
                var volunteer = await _dbContext.Volunteers.FirstOrDefaultAsync(x => x.Id == VolunteerId&&x.UserId==UserId);
                if (volunteer == null)
                    return new ApiResponse(404, "المتطوع غير موجود");
                if (volunteer.UserId != UserId)
                    return new ApiResponse(401, "غير مصرح لك بالحذف");
                _dbContext.Volunteers.Remove(volunteer);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم حذف المتطوع بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllVolunteers()
        {
            try
            {
                var volunteers = await _dbContext
                    .Volunteers
                    .Select(x=> new {
                        x.Id,
                        x.Case,
                        x.CaseName,
                        x.agency,
                        x.IdNumber,
                        x.PhoneNumber,
                        x.Added,
                        x.AppUser.FullName,
                        x.AppUser.Email
                    }).ToListAsync();
                return new ApiResponse(200, volunteers);
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> GetVolunteerById(int VolunteerId)
        {
            try
            {
                var volunteer = await _dbContext
                    .Volunteers
                    .Select(x => new
                    {
                        x.Id,
                        x.Case,
                        x.CaseName,
                        x.agency,
                        x.IdNumber,
                        x.PhoneNumber,
                        x.Added,
                        x.AppUser.FullName,
                        x.AppUser.Email
                    }).FirstOrDefaultAsync(x => x.Id == VolunteerId);
                if (volunteer == null)
                    return new ApiResponse(404, "المتطوع غير موجود");
                return new ApiResponse(200, volunteer);
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
        public async Task<ApiResponse> Update(VolunteerModelDto model, string UserId, int VolunteerId)
        {
            try
            {
                var volunteer = await _dbContext.Volunteers.FirstOrDefaultAsync(x => x.Id == VolunteerId && x.UserId == UserId);

                if (volunteer == null)
                    return new ApiResponse(404, "المتطوع غير موجود");

                if (volunteer.UserId != UserId)
                    return new ApiResponse(401, "غير مصرح لك بالتعديل");

                _mapper.Map(model, volunteer);

                _dbContext.Volunteers.Update(volunteer);
                await _dbContext.SaveChangesAsync();

                return new ApiResponse(200, "تم تعديل المتطوع بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }
    }
}
