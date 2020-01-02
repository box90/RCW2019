using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCW2019.Common
{
    public class AuthUserModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }

        public AuthUserModel(string u, string t)
        {
            this.UserName = u;
            this.Token = t;
        }
    }
}
