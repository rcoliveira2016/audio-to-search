using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.Common.Repositories;
using System.Linq.Expressions;

namespace AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;

public interface ICatalogarAudioTranscricaoRepository : IRepositoryBase<CatalogarAudioTranscricaoEntity>
{
    IEnumerable<CatalogarAudioTranscricaoEntity> GetByUIdCatalogarAudio(Guid uidCatalogarAudio);
    Task Insert(ICollection<CatalogarAudioTranscricaoEntity> catalogarAudioTranscricaoEntities);
    Task Update(ICollection<CatalogarAudioTranscricaoEntity> catalogarAudioTranscricaoEntities);
    IEnumerable<CatalogarAudioTranscricaoEntity> GetByEmbedding(float[] embedding);
}
