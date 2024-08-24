using AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;

namespace AudioToSearch.Infra.ServiceAgents.ProvaderAI.Test
{
    public class ITextEmbeddingServiceTest
    {
        [Fact]
        public async void TextEmbeddingMicrosoftMLServiceGetEmbedding()
        {
            var service = new TextEmbeddingMicrosoftMLService();
            Assert.NotNull(service);
            var emdedi = await service.GenerateEmbedding("ola varias coisa falados heheeheheh");

            Assert.True(!emdedi.IsEmpty);
        }
    }
}