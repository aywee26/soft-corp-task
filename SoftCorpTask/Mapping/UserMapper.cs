using Riok.Mapperly.Abstractions;
using SoftCorpTask.Entities;
using SoftCorpTask.Models;

namespace SoftCorpTask.Mapping;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    [MapperIgnoreSource(nameof(User.PasswordSalt))]
    public partial UserModel MapEntityToModel(User entity);
}