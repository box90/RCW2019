using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RWC2019.BL;
using RWC2019.Entities;

namespace RCW2019.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {

        private IHubContext<Hubs.RCWHub> _hub;

        public MatchesController(IHubContext<Hubs.RCWHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Match>> GetMatches()
        {
            var matches = await MatchesManager.GetMatchesAsync();
            await _hub.Clients.All.SendAsync("GetMatches",matches);

            return matches;
        }

        [HttpGet("{id}"), Authorize]
        [Authorize]
        public async Task<MatchDetail> GetMatchDetailAsync(int id)
        {
            var match = await MatchesManager.GetMatchDetailAsync(id);
            //await _hub.Clients.All.SendAsync("GetMatchDetailAsync", match);

            return match;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<bool> StartMatch(int id)
        {
            return await MatchesManager.StartMatchAsync(id);
        }
    }
}