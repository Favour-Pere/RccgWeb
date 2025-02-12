using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IZoneService
    {
        Task<IEnumerable<Zone>> GetZonesAsync();

        Task<Zone> GetZoneByIdAsync(Guid zoneId);

        Task AddZoneAsync(Zone zone);

        Task UpdateZoneAsync(Zone zone);

        Task DeleteZoneAsync(Guid zoneId);
    }
}