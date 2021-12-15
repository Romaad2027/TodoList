using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TODOListAPI.Models.Requests;

namespace TODOListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private TODOListEFContext db = new TODOListEFContext();

        private string GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return encodedJwt;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            User user = db.Users.FirstOrDefault(u => u.Name == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return BadRequest(new { errorText = "Invalid username or password" });
            }

            var identity = GetIdentity(user);
            var jwt = GetToken(identity);

            var response = new
            {
                access_token = jwt,
                username = identity.Name
            };

            return Json(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.Name == register.Username && u.Password == register.Password);

                if (user == null)
                {
                    user = new User { Name = register.Username, Password = register.Password };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    var identity = GetIdentity(user);
                    var jwt = GetToken(identity);

                    var response = new
                    {
                        access_token = jwt,
                        username = identity.Name
                    };

                    return Json(response);
                }
                return BadRequest(new { errorText = "This user already exists" });
            }
            return BadRequest(ModelState);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
