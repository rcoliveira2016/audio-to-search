using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NAudio.Wave;
using System.Text;

namespace AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;

public class CatalogarAudioCommandHandler(
    IOptions<PathSettings> pathSettings,
    ISpeechToTextService speechToTextService,
    ILogger<CatalogarAudioCommandHandler> logger
    ) : IRequestHandler<CatalogarAudioCommand>
{
    private const int SampleRate = 16000;

    public async Task Handle(CatalogarAudioCommand request, CancellationToken cancellationToken)
    {
        if(!ObterCaminhoAudioTratado(request, out var caminhoNovo)) return;

        await TranscreverAudio(caminhoNovo);

        return;
    }

    private bool ObterCaminhoAudioTratado(CatalogarAudioCommand request, out string caminhoNovo)
    {
        if (!CheckExtension(request.CaminhoArquivo))
        {
            caminhoNovo = string.Empty;
            logger.LogError("Extenção não é valida");
            return false;
        }

        if (!IsMp3OrWav(request.CaminhoArquivo, out var tipo))
        {
            logger.LogError("Não é formato mp3 e wav");
            caminhoNovo = string.Empty;
            return false;
        }

        caminhoNovo = Path.Combine(
            pathSettings.Value.DiretorioCatalogoAudios,
            $"{Guid.NewGuid().ToString()}.wav");

        if (tipo == eFormatoArquivo.Mp3)
            ConvertMp3ToWav(request.CaminhoArquivo, caminhoNovo, SampleRate);

        else if (tipo == eFormatoArquivo.Wav)
            ConvertWavToWavMono(request.CaminhoArquivo, caminhoNovo, SampleRate);

        File.Delete(request.CaminhoArquivo);

        return true;
    }

    private async Task TranscreverAudio(string caminhoNovo)
    {
        try
        {
            var result = await speechToTextService.Send(caminhoNovo);
            await foreach (var item in result.Itens)
            {
                logger.LogInformation(item.Text);
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private static void ConvertMp3ToWav(string mp3FilePath, string wavFilePath, int sampleRate)
    {
        using (var reader = new Mp3FileReader(mp3FilePath))
            CreateWaveFile(reader, wavFilePath, sampleRate);
    }

    private static void ConvertWavToWavMono(string inputWavFilePath, string outputWavFilePath, int sampleRate)
    {
        using (var reader = new WaveFileReader(inputWavFilePath))
            CreateWaveFile(reader, outputWavFilePath, sampleRate);
    }

    private static void CreateWaveFile(IWaveProvider reader, string outputWavFilePath, int sampleRate)
    {
        var newFormat = new WaveFormat(sampleRate, 1);
        using (var resampler = new MediaFoundationResampler(reader, newFormat))
        {
            resampler.ResamplerQuality = 60;
            WaveFileWriter.CreateWaveFile(outputWavFilePath, resampler);
        }
    }

    private static bool CheckExtension(string filePath)
    {
        var extencao = Path.GetExtension(filePath);

        return extencao == ".wav" || extencao == ".mp3";
    }

    private static bool IsMp3OrWav(string filePath, out eFormatoArquivo eFormatoArquivo)
    {
        byte[] buffer = new byte[12];

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            if (fs.Length < 12)
            {
                eFormatoArquivo = eFormatoArquivo.None;
                return false;
            }

            fs.Read(buffer, 0, buffer.Length);
        }

        // Check for WAV
        if (Encoding.ASCII.GetString(buffer, 0, 4) == "RIFF" &&
            Encoding.ASCII.GetString(buffer, 8, 4) == "WAVE")
        {
            eFormatoArquivo = eFormatoArquivo.Wav;
            return true;
        }

        // Check for MP3
        if (Encoding.ASCII.GetString(buffer, 0, 3) == "ID3")
        {
            eFormatoArquivo = eFormatoArquivo.Mp3;
            return true;
        }

        // Check for MP3 MPEG frame header
        // Check for MPEG audio frame sync (0xFFE or 0xFFFB)
        if ((buffer[0] == 0xFF && (buffer[1] & 0xE0) == 0xE0))
        {
            eFormatoArquivo = eFormatoArquivo.Mp3;
            return true;
        }

        eFormatoArquivo = eFormatoArquivo.None;
        return false;
    }
}

enum eFormatoArquivo { 
    None,
    Mp3,
    Wav
}


