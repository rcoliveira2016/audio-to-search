namespace AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText.Results;

public class SpeechToTextResult
{
    public required IAsyncEnumerable<SpeechToTextItemResult> Itens { get; init; }
    //public required List<SpeechToTextItemResult> ItensSync { get; init; }
}

public class SpeechToTextItemResult
{
    public required string Text { get; init; }
}