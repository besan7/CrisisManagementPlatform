using Microsoft.EntityFrameworkCore;
using CrisisManagementPlatform.Data;
using CrisisManagementPlatform.Models;

namespace CrisisManagementPlatform.Services
{
    public class CrisisService
    {
        private readonly AppDbContext _context;

        public CrisisService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Get all crises with optional search and filter
        public async Task<List<Crisis>> GetCrisesAsync(string? searchTerm, CrisisStatus? statusFilter)
        {
            var query = _context.Crises
                .Include(c => c.CrisisCategory)
                .Include(c => c.CrisisAffectedAreas)
                .ThenInclude(ca => ca.AffectedArea)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm) || (c.CrisisCategory != null && c.CrisisCategory.Name.Contains(searchTerm)));
            }

            if (statusFilter.HasValue)
            {
                query = query.Where(c => c.Status == statusFilter.Value);
            }

            return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        // 2. Statistics counts from Database
        public async Task<int> GetTotalCrisesCountAsync() => await _context.Crises.CountAsync();

        public async Task<int> GetActiveCrisesCountAsync() => await _context.Crises.CountAsync(c => c.Status == CrisisStatus.Active);

        public async Task<int> GetRegisteredCrisesCountAsync() => await _context.Crises.CountAsync(c => c.Status == CrisisStatus.Registered);

        // 3. Get crisis by ID
        public async Task<Crisis?> GetCrisisByIdAsync(int id)
        {
            return await _context.Crises
                .Include(c => c.CrisisCategory)
                .Include(c => c.CrisisAffectedAreas)
                .ThenInclude(ca => ca.AffectedArea)
                .FirstOrDefaultAsync(c => c.CrisisId == id);
        }

        // 4. Create crisis
        public async Task CreateCrisisAsync(Crisis crisis)
        {
            crisis.Status = CrisisStatus.Registered;
            crisis.CreatedAt = DateTime.Now;
            _context.Crises.Add(crisis);
            await _context.SaveChangesAsync();
        }

        // 5. Activate crisis
        public async Task<bool> ActivateCrisisAsync(int id)
        {
            var crisis = await _context.Crises.FindAsync(id);
            if (crisis == null || crisis.Status == CrisisStatus.Active)
            {
                return false;
            }

            crisis.Status = CrisisStatus.Active;
            _context.Crises.Update(crisis);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CrisisCategory>> GetCategoriesAsync() => await _context.CrisisCategories.ToListAsync();
        public async Task<List<AffectedArea>> GetAffectedAreasAsync() => await _context.AffectedAreas.ToListAsync();
    }
}