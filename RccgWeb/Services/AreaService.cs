using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services.Interfaces;

namespace RccgWeb.Services
{
    public class AreaService : IAreaService
    {
        private readonly ApplicationDbContext _context;

        public AreaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<Area> GetAreaByIdAsync(Guid areadId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Area>> GetAreasAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }
    }
}