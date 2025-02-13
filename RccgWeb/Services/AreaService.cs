using Microsoft.EntityFrameworkCore;
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

        public async Task AddAreaAsync(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
        }

        public async Task<Area> GetAreaByIdAsync(Guid areaId)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.AreaId == areaId);
        }

        public async Task<IEnumerable<Area>> GetAreasAsync()
        {
            return await _context.Areas.ToListAsync();
        }

        public async Task UpdateAreaAsync(Area area)
        {
            _context.Areas.Update(area);

            await _context.SaveChangesAsync();
        }
    }
}