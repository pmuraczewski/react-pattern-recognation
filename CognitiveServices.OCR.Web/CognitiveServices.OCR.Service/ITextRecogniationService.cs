namespace CognitiveServices.OCR.Service
{
    public interface ITextRecogniationService
    {
        string[] ReadTextFromImage(byte[] image);
    }
}