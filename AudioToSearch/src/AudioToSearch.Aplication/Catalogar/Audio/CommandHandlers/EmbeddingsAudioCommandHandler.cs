using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;

public class EmbeddingsAudioCommandHandler(
    IOptions<ApiKeysSettings> apiKeysSettingsOptions
    ) : IRequestHandler<EmbeddingsAudioCommand>
{
    public async Task Handle(EmbeddingsAudioCommand request, CancellationToken cancellationToken)
    {
        var aa = apiKeysSettingsOptions.Value.OpenIA;
    }
}
