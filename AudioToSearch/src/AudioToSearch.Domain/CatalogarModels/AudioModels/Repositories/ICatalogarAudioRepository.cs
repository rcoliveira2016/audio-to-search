using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.Common.Repositories;
using System.Linq.Expressions;

namespace AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;

public interface ICatalogarAudioRepository: IRepositoryBase<CatalogarAudioEntity>
{
    Task<int> UpdateTranscricao<TProperty>(
        Expression<Func<CatalogarAudioTranscricaoEntity, bool>> predicate,
        Func<CatalogarAudioTranscricaoEntity, TProperty> propertyExpression,
        Func<CatalogarAudioTranscricaoEntity, TProperty> valueExpression);
}
