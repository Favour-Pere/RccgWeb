using Microsoft.AspNetCore.Mvc;
using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IChurchService
    {
        public Task<Church> GetAllChurchesAsync();

        public Task<IActionResult> AddWorker(Workers workers, Church church);
    }
}