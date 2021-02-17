using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database;
using Database.Models;
using HtmlAgilityPack;
using InitJobOffers.Services;
using JobFinder.Models;
using JobSeeker.Models;
using JobSeeker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IInitJobs _initJob;
        private readonly IJobSeekerService _jobSeekerService;

        public JobsController(ILogger<WeatherForecastController> logger, IInitJobs initJob, IJobSeekerService jobSeekerService)
        {
            _logger = logger;
            _initJob = initJob;
            _jobSeekerService = jobSeekerService;
        }

        [HttpGet("GetSkillsByKey")]
        public async Task<IActionResult> GetSkillsByKey(string key)
        {
            var res = await _jobSeekerService.GetSkillsDropdownlist(key);
            return Ok(new Response<List<SkillUser>>(res));;
        }
        [HttpGet("GetAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var res = await _jobSeekerService.GetAllSkills();
            return Ok(new Response<List<string>>(res)); ;
        }
        [HttpGet("GetAllJobNames")]
        public async Task<IActionResult> GetAllJobNames(string key)
        {
            var res = await _jobSeekerService.GetJobNames(key);
            return Ok(new Response<List<string>>(res));
        }

        [HttpGet("GetRecommendedJobs")]
        public async Task<IActionResult> GetRecommendedJobs(string userId)
        {
            var res = await _jobSeekerService.GetRecommendedJobs(Convert.ToInt32(userId));
            return Ok(new Response<List<RecommendedJobModel>>(res));
        }
        [HttpGet("GetExpendedUserData")]
        public async Task<IActionResult> GetExpendedUserData(int userId)
        {
            var res = await _jobSeekerService.GetExpendedUserData(userId);
            return Ok(new Response<ExpendedUserData>(res));
        }
        [HttpPost("AddUserExpendedData")]
        public async Task<IActionResult> AddUserExpendedData(ExpendedUserData model)
        {
            var res = await _jobSeekerService.UpdateExpendedUserData(model);
            return Ok(new Response<bool>(res));
        }
        [HttpPost("AddKeyRequest")]
        public async Task<IActionResult> AddKeyRequest(NewKey model)
        {
            var res = await _jobSeekerService.AddNewKeyRequest(model);
            return Ok(new Response<bool>(res));
        }
        [HttpPost("AddJobRequest")]
        public async Task<IActionResult> AddJobRequest(NewJob model)
        {
            var res = await _jobSeekerService.AddNewJobRequest(model);
            return Ok(new Response<bool>(res));
        }
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var res = await _jobSeekerService.GetAllRequests();
            return Ok(new Response<RequestList>(res));
        }

        [HttpPut("AddNewKey")]
        public async Task<IActionResult> AddNewKey(int recordId)
        {
            var res = await _jobSeekerService.AddNewKey(recordId);
            return Ok(new Response<bool>(res));
        }

        [HttpPut("RemoveNewKey")]
        public async Task<IActionResult> RemoveNewKey(int recordId)
        {
            var res = await _jobSeekerService.RemoveNewKey(recordId);
            return Ok(new Response<bool>(res));
        }
        [HttpPut("AddNewJobs")]
        public async Task<IActionResult> AddNewJobs(int recordId)
        {
            var res = await _initJob.AddJobsByRequest(recordId);
            return Ok(new Response<bool>(res));
        }
        [HttpGet("UpdateJobSkills")]
        public async Task<IActionResult> UpdateJobSkills()
        {
            var res = await _initJob.UpdateSkillsOnJobs();
            return Ok(new Response<bool>(res));
        }
        [HttpPut("RemoveNewJob")]
        public async Task<IActionResult> RemoveNewJob(int recordId)
        {
            var res = await _jobSeekerService.RemoveNewJob(recordId);
            return Ok(new Response<bool>(res));
        }
        [HttpGet("GetJobsFromCV")]
        public async Task<IActionResult> GetJobsFromCV(int userId)
        {
            var res = await _jobSeekerService.GetJobsFromCV(userId);
            return Ok(new Response<List<ExperienceUser>>(res));
        }

    }
}
