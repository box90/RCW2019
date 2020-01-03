using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RCW2019.API.Hubs;
using RCW2019.Common;
using RWC2019.BL;

namespace RCW2019.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHubContext<RCWHub> hubContext;
        public AuthController(IHubContext<RCWHub> _hubContext, IConfiguration _configuration)
        {
            configuration = _configuration;
            hubContext = _hubContext;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public AuthUserModel Login([FromBody]User u)
        {
            return new AuthUserModel(u.UserName, AuthManager.GetToken(u));  
        }
    }
}