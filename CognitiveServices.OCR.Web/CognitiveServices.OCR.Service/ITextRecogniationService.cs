namespace CognitiveServices.OCR.Service
{
    public interface ITextRecogniationService
    {
        Task<List<string>> ReadTextFromImageAsync(Stream image);
    }
}