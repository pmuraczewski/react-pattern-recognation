using Newtonsoft.Json;

namespace CognitiveServices.OCR.Service.Models
{
    public class TranslationModel
    {
        [JsonProperty("translations")]
        public Translations[] Translations { get; set; }
    }
}
