using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public interface IWorkGroupService
{
    Task<IEnumerable<WorkGroupModel>> GetWorkGroupsAsync();
    Task<WorkGroupModel?> GetWorkGroupByIdAsync(Guid id);
    Task<WorkGroupModel> CreateWorkGroupAsync(CreateWorkGroupModel model);
}