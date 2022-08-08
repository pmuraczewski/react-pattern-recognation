namespace CognitiveServices.OCR.Service.Models
{
    public class TextRecognationModel
    {
        public List<string> RecognizedLines { get; set; }

        public string TranslatedText { get; set; }
    }
}
