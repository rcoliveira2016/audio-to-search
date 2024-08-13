namespace AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText.Results;

public class SpeechToTextResult
{
    public required IAsyncEnumerable<SpeechToTextItemResult> Itens { get; init; }
}

public class SpeechToTextItemResult
{
    public required string Text { get; init; }
    public required TimeSpan Start { get; init; }
    public required TimeSpan End { get; init; }
}