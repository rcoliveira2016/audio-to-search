using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AudioToSearch.Infra.Data.Repositorires;

public class CatalogarAudioRepository : RepositoryBase<CatalogarAudioEntity>, ICatalogarAudioRepository
{
    public CatalogarAudioRepository(AudioToSearchContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<CatalogarAudioEntity>> GetAllAsync()
    {
        return await dbSet.Include(x=> x.Transcricaoes).ToListAsync();
    }
}
