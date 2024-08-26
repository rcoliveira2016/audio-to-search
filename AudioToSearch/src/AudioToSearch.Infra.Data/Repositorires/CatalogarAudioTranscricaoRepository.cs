using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
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

    public IEnumerable<CatalogarAudioTranscricaoEntity> GetByEmbedding(float[] embedding)
    {
        return dbSet
            .AsNoTracking()
            .Include(x=> x.CatalogarAudio)
            .Where(x => x.Embedding!.L1Distance(new Vector(embedding)) < 1)
            .OrderBy(x => x.Embedding!.L2Distance(new Vector(embedding)));
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
