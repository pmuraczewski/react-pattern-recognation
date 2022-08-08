using CognitiveServices.OCR.Service.Configs;
using CognitiveServices.OCR.Service.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CognitiveServices.OCR.Service.Services
{
    public class TextRecogniationService : ITextRecogniationService
    {
        private readonly AzureCognitiveServicesConfig _config;

        public TextRecogniationService(IOptions<AzureCognitiveServicesConfig> options)
        {
            _config = options.Value;
        }

        public async Task<TextRecognationModel> ReadTextFromImageAsync(Stream image)
        {
            var recognizedLines = new List<string> { "Ein", "zwei", "drei", "polizei" };
            var translatedText = "asdada";
            //var recognizedLines = await GetRecognizedLines(image);
            //var translatedText = await GetTranslatedLines(recognizedLines);

            return new TextRecognationModel
            {
                RecognizedLines = recognizedLines,
                TranslatedText = translatedText
            };
        }

        private async Task<List<string>> GetRecognizedLines(Stream image)
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_config.ComputerVisionKey)) { Endpoint = _config.ComputerVisionEndpoint };

            var textHeaders = await client.ReadInStreamAsync(image, "de");

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

        private async Task<string> GetTranslatedLines(List<string> lines)
        {
            var inputText = string.Join(" ", lines);
            string result;

            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(_config.TranslatorEndpoint);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", _config.TranslatorKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", _config.TranslatorRegionKey);

                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<TranslationModel[]>(responseContent)[0].Translations[0].Text;
                }
            }

            return result;
        }
    }
}