using Riok.Mapperly.Abstractions;
using SoftCorpTask.Entities;
using SoftCorpTask.Models.Candidates;

namespace SoftCorpTask.Mapping;

[Mapper]
public partial class CandidateMapper
{
    public partial CandidateModel Map(Candidate entity);
}