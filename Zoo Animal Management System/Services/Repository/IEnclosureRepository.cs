using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Repository
{
    public interface IEnclosureRepository
    {
        Task<bool> AddEnclosure(Enclosure enclosure);
        Task<bool> AddEnclosureList(List<Enclosure> enclosures);
        Task<Enclosure> GetEnclosureById(Guid enclosureId);
        Task<List<Enclosure>> GetAllEnclosures();
    }
}