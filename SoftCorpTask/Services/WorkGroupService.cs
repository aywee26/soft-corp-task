using Microsoft.EntityFrameworkCore;
using SoftCorpTask.Contexts;
using SoftCorpTask.Entities;
using SoftCorpTask.Mapping;
using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public class WorkGroupService : IWorkGroupService
{
    private readonly ApplicationDbContext _context;
    private readonly WorkGroupMapper _mapper;

    public WorkGroupService(ApplicationDbContext context)
    {
        _context = context;
        _mapper = new();
    }

    public async Task<IEnumerable<WorkGroupModel>> GetWorkGroupsAsync()
    {
        var entities = await _context.WorkGroups.ToListAsync();
        var models = entities.Select(_mapper.Map);
        return models;
    }

    public async Task<WorkGroupModel?> GetWorkGroupByIdAsync(Guid id)
    {
        var entity = await _context.WorkGroups.FindAsync(id);
        if (entity is null)
            return null;
        
        var model = _mapper.Map(entity);
        return model;
    }

    public async Task<WorkGroupModel> CreateWorkGroupAsync(CreateWorkGroupModel model)
    {
        var entity = new WorkGroup
        {
            Id = Guid.NewGuid(),
            Name = model.Name
        };
        
        _context.WorkGroups.Add(entity);
        await _context.SaveChangesAsync();
        
        var result = _mapper.Map(entity);
        return result;
    }
}