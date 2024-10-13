using ModelLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IShelterService
    {
        Task CreateShelter(Shelter shelter);
        Task DeleteShelter(int id);
        IEnumerable<Shelter> GetShelter();
        Task<Shelter> GetShelterByID(int shelterId);
        Task UpdateShelter(Shelter shelter);

        Task<Shelter?> GetShelterByUserIDAsync(int userId);
    }
}