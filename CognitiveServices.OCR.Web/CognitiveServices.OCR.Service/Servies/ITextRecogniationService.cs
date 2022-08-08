using CognitiveServices.OCR.Service.Models;

namespace CognitiveServices.OCR.Service.Services
{
    public interface ITextRecogniationService
    {
        Task<TextRecognationModel> ReadTextFromImageAsync(Stream image);
    }
}