using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Utilities;
using static WebAPI.Utilities.Enum;

namespace WebAPI.Services
{
    public class ComplaintService : IComplaintService
    {
        public readonly ComplaintAgentDBContext _dbContext;
        public ComplaintService(ComplaintAgentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseResult<List<Complaint>> GetComplaintsByUserId(int userId)
        {
            ResponseResult<List<Complaint>> result = new ResponseResult<List<Complaint>>();
            List<Complaint> complaints = _dbContext.Complaints.Where(c => c.UserId == userId).ToList();

            List<Complaint> models = complaints.Select(c => new Complaint
            {
                Id = c.Id,
                Description = c.Description,
                Status = c.Status,
                Type = c.Type,
                UserId = c.UserId,
                StatusStr = System.Enum.GetName(typeof(ComplaintStatus), c.Status),
                TypeStr = System.Enum.GetName(typeof(ComplaintType), c.Type)
            }).ToList();

            result.Data = models;
            result.Errors = null;
            result.Status = StatusType.Success;

            return result;
        }

        public async Task<ActionResult<ResponseResult<List<Complaint>>>> SubmitComplaint(SubmitComplaint dto)
        {
            ResponseResult<List<Complaint>> result = new ResponseResult<List<Complaint>>();
            Complaint complaint = new Complaint();
            try
            {
                if (dto.Id > 0) // Update
                {
                    bool isUpdated = false;
                    complaint = _dbContext.Complaints.Where(c => c.Id == dto.Id).FirstOrDefault();
                    complaint.Type = (ComplaintType)dto.Type;
                    complaint.Description = dto.Description;
                    _dbContext.Update(complaint);
                    isUpdated = await _dbContext.SaveChangesAsync() > 0;
                    if (isUpdated)
                    {
                        result.Data = _dbContext.Complaints.Where(c => c.UserId == dto.UserId).ToList();
                        result.Errors = null;
                        result.Status = StatusType.Success;
                    }
                    else
                    {
                        result.Data = null;
                        result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                        result.Status = StatusType.Failed;
                    }
                }
                else // Save
                {
                    bool isSaved = false;
                    complaint.Type = (ComplaintType)dto.Type;
                    complaint.Description = dto.Description;
                    complaint.Status = ComplaintStatus.Pending;
                    complaint.UserId = dto.UserId;
                    _dbContext.Add(complaint);
                    isSaved = await _dbContext.SaveChangesAsync() > 0;
                    if (isSaved)
                    {
                        result.Data = _dbContext.Complaints.Where(c => c.UserId == dto.UserId).ToList();
                        result.Errors = null;
                        result.Status = StatusType.Success;
                    }
                    else
                    {
                        result.Data = null;
                        result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                        result.Status = StatusType.Failed;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Errors = new List<string> { "Something went wrong, please contact the developer" };
            }

            return result;
        }

        public ActionResult<ResponseResult<List<Complaint>>> GetAll()
        {
            ResponseResult<List<Complaint>> result = new ResponseResult<List<Complaint>>();
            List<Complaint> complaints = _dbContext.Complaints.Include(c => c.user).ToList();

            List<Complaint> models = complaints.Select(c => new Complaint
            {
                Id = c.Id,
                Description = c.Description,
                Status = c.Status,
                Type = c.Type,
                UserId = c.UserId,
                StatusStr = System.Enum.GetName(typeof(ComplaintStatus), c.Status),
                TypeStr = System.Enum.GetName(typeof(ComplaintType), c.Type),
                username = c.user.Name
            }).ToList();

            result.Data = models;
            result.Errors = null;
            result.Status = StatusType.Success;

            return result;
        }

        public async Task<ActionResult<ResponseResult<List<Complaint>>>> UpdateStatus(int id, int status)
        {
            ResponseResult<List<Complaint>> result = new ResponseResult<List<Complaint>>();
            Complaint complaint = new Complaint();
            try
            {
                bool isUpdated = false;
                complaint = _dbContext.Complaints.Where(c => c.Id == id).FirstOrDefault();
                complaint.Status = (ComplaintStatus)status;
                _dbContext.Update(complaint);
                isUpdated = await _dbContext.SaveChangesAsync() > 0;
                if (isUpdated)
                {
                    result.Data = _dbContext.Complaints.ToList();
                    result.Errors = null;
                    result.Status = StatusType.Success;
                }
                else
                {
                    result.Data = null;
                    result.Errors = new List<string> { "Something went wrong, please contact the developer" };
                    result.Status = StatusType.Failed;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Errors = new List<string> { "Something went wrong, please contact the developer" };
            }

            return result;
        }
    }
}
