﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Area> GetAreaByIdAsync(Guid areaId)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.AreaId == areaId);
        }

        public async Task<IEnumerable<Area>> GetAreasAsync()
        {
            return await _context.Areas.ToListAsync();
        }

        public Task UpdateAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }
    }
}