using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApis.Token
{
    public class TokenJWT
    {

        // Aqui faz o Token JWT
        private JwtSecurityToken token;

        internal TokenJWT(JwtSecurityToken token) 
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
