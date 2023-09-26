using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace CureWellUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizeController : ControllerBase
    {
        private readonly CureWellDBContext context;
        private readonly JwtSettings jwtSettings;
        private readonly IRefreshHandler refreshHandler;
        private readonly IUserService service;

        public AuthorizeController(CureWellDBContext context, IOptions<JwtSettings> options,IRefreshHandler refreshHandler,IUserService service)
        {
            this.context = context;
            this.jwtSettings = options.Value;
            this.refreshHandler = refreshHandler;
            this.service = service;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Create(User _data)
        {
            var data = await this.service.Create(_data);
            return Ok(data);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GenerateToken([FromBody] UserLogin userlogin)
        {
            APIResponse response = new APIResponse();
            response.ResponseCode = 401;
            response.ErrorMessage = "Unauthorized";
            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(item => item.UserName == userlogin.userName && item.Password == userlogin.password);
                if (user != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
                    var tokendesc = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddSeconds(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                    };
                    var token = tokenhandler.CreateToken(tokendesc);
                    var finaltoken = tokenhandler.WriteToken(token);
                    return Ok(new TokenResponse() { Token = finaltoken, RefreshToken = await this.refreshHandler.GenerateToken(userlogin.userName) });
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return Ok(response);
            }
        }
        [HttpPost("GenerateRefreshToken")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenResponse token)
        {
            APIResponse response = new APIResponse();
            response.ResponseCode = 401;
            response.ErrorMessage = "Unauthorized";
            try
            {
                var refreshToken = await this.context.RefreshTokens.FirstOrDefaultAsync(item => item.Refreshtoken1 == token.RefreshToken);
                if (refreshToken != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
                    SecurityToken securityToken;
                    var principal = tokenhandler.ValidateToken(token.Token, new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    }, out securityToken);

                    var _token = securityToken as JwtSecurityToken;
                    if (_token != null && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                    {
                        string username = principal.Identity.Name;
                        var _existdata = await this.context.RefreshTokens.FirstOrDefaultAsync(item => item.UserId == username
                        && item.Refreshtoken1 == token.RefreshToken);
                        if (_existdata != null)
                        {
                            var _newtok = new JwtSecurityToken(
                                claims: principal.Claims.ToArray(),
                                expires: DateTime.Now.AddSeconds(30),
                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.securitykey)),
                                SecurityAlgorithms.HmacSha256)
                                );
                            var _finalToken = tokenhandler.WriteToken(_newtok);
                            return Ok(new TokenResponse() { Token = _finalToken, RefreshToken = await this.refreshHandler.GenerateToken(username) });
                        }
                        else
                        {
                            return Ok(response);
                        }
                    }
                    else
                    {
                        return Ok(response);
                    }
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return Ok(response);
            }
        }
    }
}
