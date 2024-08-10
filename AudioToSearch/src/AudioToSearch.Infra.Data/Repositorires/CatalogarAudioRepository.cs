using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;

namespace AudioToSearch.Infra.Data.Repositorires;

public class CatalogarAudioRepository : RepositoryBase<CatalogarAudioEntity>, ICatalogarAudioRepository
{
    public CatalogarAudioRepository(AudioToSearchContext dbContext) : base(dbContext)
    {
    }
}
