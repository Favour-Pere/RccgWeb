using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IChurchService
    {
        Task<Zone> GetZoneByIdAsync(Guid id);

        Task<Area> GetAreaByIdAsync(Guid id);

        Task<Parish> GetParishByIdAsync(Guid id);

        Task<IEnumerable<Zone>> GetAllZonesAsync();

        Task<IEnumerable<Area>> GetAllAreasAsync();

        Task<IEnumerable<Parish>> GetAllParishesAsync();

        Task<bool> ChurchExistsAsync(string churchId);
    }
}