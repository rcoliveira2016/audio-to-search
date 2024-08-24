
using AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using OpenAI.Embeddings;
using System.Diagnostics.CodeAnalysis;

namespace AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;

public class TextEmbeddingMicrosoftMLService() : ITextEmbeddingService
{

    public Task<ReadOnlyMemory<float>> GenerateEmbedding(string text)
    {
        return Task.Run<ReadOnlyMemory<float>>(() => {
            var mlContext = new MLContext();
            IEnumerable<LdaInput> input = [new LdaInput { Text = text }];
            var dataView = mlContext.Data.LoadFromEnumerable(input);
            var pipeline = mlContext.Transforms.Text.NormalizeText("NormalizedText", "Text")
                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
                .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens"))
                .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens"))
                .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: 3));
            var model = pipeline.Fit(dataView);
            var engine = mlContext.Model.CreatePredictionEngine<LdaInput, LdaOutput>(model);
            var resultInput = input.FirstOrDefault();

            return engine.Predict(resultInput!).Features;
        });
    }
}
class LdaInput
{
    public string Text { get; set; }
}

class LdaOutput
{
    public float[] Features { get; set; }
}