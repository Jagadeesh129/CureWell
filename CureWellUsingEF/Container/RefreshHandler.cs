using CureWellUsingEF.Repos;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CureWellUsingEF.Container
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly CureWellDBContext context;
        public RefreshHandler(CureWellDBContext context)
        {
            this.context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomNumber = new byte[32];
            using(var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                var existToken = this.context.RefreshTokens.FirstOrDefaultAsync(item => item.UserId == username).Result;
                if (existToken != null)
                {
                    existToken.Refreshtoken1 = refreshToken;
                }
                else
                {
                    await this.context.RefreshTokens.AddAsync(new Repos.Models.RefreshToken
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        Refreshtoken1 = refreshToken   
                    }); 
                }
                await this.context.SaveChangesAsync();
                return refreshToken;
            }
        }
    }
}
