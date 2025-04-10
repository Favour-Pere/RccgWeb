using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IChurchService
    {
        Task<string?> GetChurchNameAsync(string churchId);
    }
}