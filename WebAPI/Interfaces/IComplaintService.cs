using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Utilities;

namespace WebAPI.Interfaces
{
    public interface IComplaintService
    {
        ResponseResult<List<Complaint>> GetComplaintsByUserId(int userId);
        Task<ActionResult<ResponseResult<List<Complaint>>>> SubmitComplaint(SubmitComplaint dto);
        ActionResult<ResponseResult<List<Complaint>>> GetAll();
        Task<ActionResult<ResponseResult<List<Complaint>>>> UpdateStatus(int id, int status);
    }
}
