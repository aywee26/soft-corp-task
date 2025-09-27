using Riok.Mapperly.Abstractions;
using SoftCorpTask.Entities;
using SoftCorpTask.Models;

namespace SoftCorpTask.Mapping;

[Mapper]
public partial class WorkGroupMapper
{
    [MapperIgnoreSource(nameof(WorkGroup.Users))]
    public partial WorkGroupModel Map(WorkGroup model);
}