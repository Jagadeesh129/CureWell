namespace CureWellUsingEF.Services
{
    public interface IRefreshHandler
    {
        public Task<string> GenerateToken(string username);
    }
}
