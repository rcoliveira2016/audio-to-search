using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText.Results;
using Whisper.net;
using Whisper.net.Ggml;
namespace AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;

public class SpeechToTextService : ISpeechToTextService
{
    private const string modelFileName = "./Data/Models/SpeechToText/ggml-base.bin";

    public async Task<SpeechToTextResult> Send(string file)
    {
        var ggmlType = GgmlType.Base;
        if (!File.Exists(modelFileName))
            await DownloadModel(modelFileName, ggmlType);

        return new()
        {
            Itens = GetItens(file),
        };
    }

    private static async IAsyncEnumerable<SpeechToTextItemResult> GetItens(string file)
    {
        using var whisperFactory = WhisperFactory.FromPath(modelFileName);

        var builder = whisperFactory
            .CreateBuilder()
            .WithLanguage("auto")
            .Build();

        using var fileStream = File.OpenRead(file);
        using (builder)
        {
            await foreach (var item in builder.ProcessAsync(fileStream))
            {
                yield return new() { 
                    Text = item.Text,
                    End = item.End,
                    Start = item.Start,
                };
            }
        }
    }

    private static async Task DownloadModel(string fileName, GgmlType ggmlType)
    { 
        var directory = new DirectoryInfo(Path.GetDirectoryName(fileName) ?? string.Empty);
        if(!directory.Exists)
            directory.Create();

        using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(ggmlType);
        using var fileWriter = File.OpenWrite(fileName);
        await modelStream.CopyToAsync(fileWriter);
    }
}
