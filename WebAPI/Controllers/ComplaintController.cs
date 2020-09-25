using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Utilities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : Controller
    {
        public readonly IComplaintService _repo;
        public ComplaintController(IComplaintService repo)
        {
            _repo = repo;
        }

        [HttpGet("GetComplaintsByUserId/{userId}")]
        public ActionResult<ResponseResult<List<Complaint>>> GetComplaintsByUserId(int userId)
        {
            return Ok(_repo.GetComplaintsByUserId(userId));
        }

        [HttpPost("SubmitComplaint")]
        public async Task<ActionResult<ResponseResult<List<Complaint>>>> SubmitComplaint([FromBody] SubmitComplaint dto)
        {
            return await _repo.SubmitComplaint(dto);
        }

        [HttpGet("GetAll")]
        public ActionResult<ResponseResult<List<Complaint>>> GetAll()
        {
            return _repo.GetAll();
        }

        [HttpGet("UpdateStatus/{id}/{status}")]
        public async Task<ActionResult<ResponseResult<List<Complaint>>>> UpdateStatus(int id, int status)
        {
            return await _repo.UpdateStatus(id, status);
        }
    }
}