using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreasAsync();

        Task<Area> GetAreaByIdAsync(Guid areadId);

        Task AddAreaAsync(Area area);

        Task UpdateAreaAsync(Area area);
    }
}