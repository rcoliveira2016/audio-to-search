using Whisper.net;
using Whisper.net.Ggml;
using Whisper.net.Logger;

namespace AudioToSearch.Infra.ServiceAgents.SpeechToText;

public static class WhisperProcesso
{
    public static async Task<WhisperProcessorBuilder> Run()
    {
        var ggmlType = GgmlType.Base;
        var modelFileName = "ggml-base.bin";

        if (!File.Exists(modelFileName))
        {
            await DownloadModel(modelFileName, ggmlType);
        }

        LogProvider.Instance.OnLog += (level, message) =>
        {
            Console.Write($"{level}: {message}");
        };

        using var whisperFactory = WhisperFactory.FromPath("ggml-base.bin");

        return whisperFactory
            .CreateBuilder()
            .WithLanguage("auto");
    }

    private static async Task DownloadModel(string fileName, GgmlType ggmlType)
    {
        using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(ggmlType);
        using var fileWriter = File.OpenWrite(fileName);
        await modelStream.CopyToAsync(fileWriter);
    }
}
