using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;

namespace CognitiveServices.OCR.Service
{
    public class TextRecogniationService : ITextRecogniationService
    {
        private readonly AzureCognitiveServicesConfig _config;

        public TextRecogniationService(IOptions<AzureCognitiveServicesConfig> options)
        {
            _config = options.Value;
        }

        public async Task<List<string>> ReadTextFromImageAsync(Stream image)
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_config.SubscriptionKey)) { Endpoint = _config.Endpoint };

            var textHeaders = await client.ReadInStreamAsync(image);

            var operationLocation = textHeaders.OperationLocation;
            var operationId = operationLocation?.Split('/')?.Last();

            if (Guid.TryParse(operationId, out Guid operationIdValue) == false)
            {
                return new List<string>();
            }

            var results = await client.GetReadResultAsync(operationIdValue);

            var textUrlFileResults = results.AnalyzeResult.ReadResults.FirstOrDefault();

            return
                textUrlFileResults == null
                    ? new List<string>()
                    : textUrlFileResults.Lines.Select(t => t.Text).ToList();
        }
    }
}