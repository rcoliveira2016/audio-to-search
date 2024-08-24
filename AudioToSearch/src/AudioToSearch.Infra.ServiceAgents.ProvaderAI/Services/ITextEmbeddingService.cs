namespace AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;

public interface ITextEmbeddingService
{
    Task<ReadOnlyMemory<float>> GenerateEmbedding(string text);
}
