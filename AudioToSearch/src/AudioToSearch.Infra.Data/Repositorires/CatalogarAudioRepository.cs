using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<int> UpdateTranscricao<TProperty>(
        Expression<Func<CatalogarAudioTranscricaoEntity, bool>> predicate,
        Func<CatalogarAudioTranscricaoEntity, TProperty> propertyExpression,
        Func<CatalogarAudioTranscricaoEntity, TProperty> valueExpression) => 
        await GetDbSet<CatalogarAudioTranscricaoEntity>()
            .Where(predicate)
            .ExecuteUpdateAsync(s => s.SetProperty(propertyExpression, valueExpression));
}
