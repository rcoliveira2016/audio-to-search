using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText.Results;

namespace AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;

public interface ISpeechToTextService
{
    public Task<SpeechToTextResult> Send(string file);
}
