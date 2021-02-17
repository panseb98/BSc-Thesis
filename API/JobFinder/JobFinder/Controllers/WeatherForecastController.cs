using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Authorization.Database;
using Database;
using Database.Models;
using HtmlAgilityPack;
using InitJobOffers.Services;
using JobFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext dataContext;
        private readonly AuthorizationDbContext _dataContext;

        private readonly IInitJobs _initJob;

        class TokenModel
        {
            public string Access_token { get; set; }
            public string Expires_in { get; set; }
            public string Token_type { get; set; }
        }
        class Type
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        class Dat
        {
            public List<Data> Data { get; set; }
        }
        class Data
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public Type Type { get; set; }
        }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IInitJobs initJob, DataContext data, AuthorizationDbContext dataContex)
        {
            _logger = logger;
            _initJob = initJob;
            _dataContext = dataContex;
            dataContext = data;
        }

        [HttpGet]
        public async Task GetAsync()
        {


        }
    }
}
