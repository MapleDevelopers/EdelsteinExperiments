using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Edelstein.Core.Entities;
using Edelstein.Service.WebAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Edelstein.Service.WebAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private WebAPIService Service { get; }

        public AuthController(WebAPIService service)
        {
            Service = service;
        }

        private TokenContract GetToken(Account account)
        {
            var signingKey = Convert.FromBase64String(Service.Config.TokenKey);
            var expiryDuration = Service.Config.TokenExpiry;
            var now = DateTime.UtcNow;
            var expire = now.AddMinutes(expiryDuration);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Service.Config.TokenIssuer,
                Audience = Service.Config.TokenAudience,
                IssuedAt = now,
                NotBefore = now,
                Expires = expire,
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.ID.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);

            return new TokenContract
            {
                Token = token,
                Expire = expire
            };
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginContract contract)
        {
            using var store = Service.DataStore.StartSession();
            var account = store
                .Query<Account>()
                .Where(a => a.Username == contract.Username)
                .FirstOrDefault();

            if (account == null || !BCrypt.Net.BCrypt.Verify(contract.Password, account.Password))
                return Unauthorized("Failed to authenticate");

            return Ok(GetToken(account));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterContract contract)
        {
            using var store = Service.DataStore.StartSession();
            var account = store
                .Query<Account>()
                .Where(a => a.Username == contract.Username)
                .FirstOrDefault();

            if (account != null)
                return Unauthorized("Account already exists");

            account = new Account
            {
                Username = contract.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(contract.Password)
            };

            await store.InsertAsync(account);

            return Ok(GetToken(account));
        }

        [Authorize]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh()
        {
            using var store = Service.DataStore.StartSession();
            var accountID = Convert.ToInt32(
                HttpContext.User.Claims
                    .Single(c => c.Type == ClaimTypes.Name)?.Value
            );
            var account = store
                .Query<Account>()
                .Where(a => a.ID == accountID)
                .First();
            return Ok(GetToken(account));
        }
    }
}