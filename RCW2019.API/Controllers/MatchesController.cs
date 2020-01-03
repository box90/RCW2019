using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWC2019.BL;
using RWC2019.Entities;

namespace RCW2019.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Match>> GetMatches()
        {
            return await MatchesManager.GetMatchesAsync();
        }

        [HttpGet("{id}"), Authorize]
        public async Task<MatchDetail> GetMatchDetailAsync(int id)
        {
            return await MatchesManager.GetMatchDetailAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<bool> StartMatch(int id)
        {
            return await MatchesManager.StartMatchAsync(id);
        }
    }
}