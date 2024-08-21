using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AudioToSearch.Infra.Data.Repositorires;

public class CatalogarAudioTranscricaoRepository : RepositoryBase<CatalogarAudioTranscricaoEntity>, ICatalogarAudioTranscricaoRepository
{
    public CatalogarAudioTranscricaoRepository(AudioToSearchContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<CatalogarAudioTranscricaoEntity> GetByUIdCatalogarAudio(Guid uidCatalogarAudio)
    {
        return dbSet.AsNoTracking().Where(x=> x.UIdCatalogarAudio == uidCatalogarAudio).AsEnumerable();
    }

    public async Task Insert(ICollection<CatalogarAudioTranscricaoEntity> catalogarAudioTranscricaoEntities)
    {
        await Context.BulkInsertAsync(catalogarAudioTranscricaoEntities);
    }

    public async Task Update(ICollection<CatalogarAudioTranscricaoEntity> catalogarAudioTranscricaoEntities)
    {
        await Context.BulkUpdateAsync(catalogarAudioTranscricaoEntities);
    }
}
