using Microsoft.EntityFrameworkCore;
using Zoo_Animal_Management_System.Database;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Repository
{
    public class EnclosureRepository : IEnclosureRepository
    {
        private readonly ZooDbContext _context;
        public EnclosureRepository(ZooDbContext context)
        {
            _context = context;
        }
        private async Task<bool> UpdateAndCheckIfAnyRowsAffected()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddEnclosure(Enclosure enclosure)
        {
            _context.Enclosures.Add(enclosure);
            return await UpdateAndCheckIfAnyRowsAffected();
        }
        public async Task<Enclosure> GetEnclosureById(Guid enclosureId)
        {
            Enclosure enclosure = await _context.Enclosures.FirstOrDefaultAsync(e => e.Id == enclosureId);
            return enclosure;
        }
        public async Task<bool> AddEnclosureList(List<Enclosure> enclosures)
        {
            _context.Enclosures.AddRange(enclosures);
            return await UpdateAndCheckIfAnyRowsAffected();
        }
        public async Task<List<Enclosure>> GetAllEnclosures()
        {
            return await _context.Enclosures.Include(enclosure => enclosure.Animals).ToListAsync();
        }

    }
}
