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
    public class complaintRepository : IcomplaintRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public complaintRepository(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Add(complaintModelDto model,string UserId)
        {
            try
            {
                var complaint = _mapper.Map<complaintModel>(model);
                complaint.UserId = UserId;
                await _dbContext.complaints.AddAsync(complaint);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم اضافة الشكوى بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
            
        }
        public async Task<ApiResponse> Delete(int complaintId, string UserId)
        {
            try
            {
                var complaint = await _dbContext.complaints.FirstOrDefaultAsync(x => x.Id == complaintId && x.UserId == UserId);
                if (complaint == null)
                    return new ApiResponse(404, "الشكوى غير موجودة");
                _dbContext.complaints.Remove(complaint);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse(200, "تم حذف الشكوى بنجاح");
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllcomplaints()
        {
            try
            {
                var complaints =await _dbContext
                    .complaints
                    .Select(x => new
                    {
                        x.Id,
                        x.ComplaintDescription,
                        x.ComplaintDate,
                        x.AppUser.FullName,
                        x.AppUser.Email
                    })
                    .ToListAsync();
                return new ApiResponse(200, complaints);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
        public async Task<ApiResponse> GetcomplaintById(int complaintId)
        {
            try
            {
                var complaint = await _dbContext
                    .complaints
                    .Where(x => x.Id == complaintId)
                    .Select(x => new
                    {
                        x.Id,
                        x.ComplaintDescription,
                        x.ComplaintDate,
                        x.AppUser.FullName,
                        x.AppUser.Email
                    })
                    .FirstOrDefaultAsync();
                if (complaint == null)
                    return new ApiResponse(404, "الشكوى غير موجودة");
                return new ApiResponse(200, complaint);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, ex.Message);
            }
        }
    }
}
