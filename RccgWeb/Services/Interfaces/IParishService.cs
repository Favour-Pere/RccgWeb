using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IParishService
    {
        Task<IEnumerable<Parish>> GetParishesAsync();

        Task<Parish> GetParishByIdAsync(Guid parishId);

        Task AddParishAsync(Parish parish);

        Task UpdateParishAsync(Parish parish);

        Task DeleteParishAsync(Guid parishId);
    }
}