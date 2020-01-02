using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RCW2019.API.Hubs;
using RCW2019.Common;
using RWC2019.BL;

namespace RCW2019.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHubContext<RCWHub> _hubContext;
        public AuthController(IHubContext<RCWHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public AuthUserModel Login([FromBody]string userName)
        {
            return new AuthUserModel(userName, AuthManager.GetToken(new User(userName)));  
        }
    }
}