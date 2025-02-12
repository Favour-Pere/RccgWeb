using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IProgramActivityService
    {
        Task<ProgramActivity> AddProgramActivityAsync(ProgramActivity programActivity);

        Task<ProgramActivity> GetProgramActivityByIdAsync(Guid id);

        Task<IEnumerable<ProgramActivity>> GetProgramActivitiesByChurchIdAsync(string churchId);

        Task<bool> DeleteProgramActivityAsync(Guid id);

        Task<IEnumerable<decimal>> GetAllTitheAsync();

        Task<IEnumerable<decimal>> GetAllOfferingAsync();

        Task<decimal> GetSumOfAllOfferingAsync();

        Task<decimal> GetSumOfAllTitheAsync();

        Task<decimal> GetSumOfOfferingByDateAsync(DateTime date);
    }
}