
using AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;
using Microsoft.Extensions.Options;
using OpenAI.Embeddings;
using System.Diagnostics.CodeAnalysis;

namespace AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;

public class TextEmbeddingOpenIAService(
    [NotNull] IOptions<ApiKeysSettings> apiKeysSettingsOptions
    ) : ITextEmbeddingService
{

    public async Task<ReadOnlyMemory<float>> GenerateEmbedding(string text)
    {

        EmbeddingClient client = new(
            model: "text-embedding-3-small", 
            apiKeysSettingsOptions.Value.OpenIA);
        var data = await client.GenerateEmbeddingAsync(text);


        return data.Value.Vector;
    }
}
