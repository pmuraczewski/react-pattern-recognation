using Newtonsoft.Json;

namespace CognitiveServices.OCR.Service.Models
{
    public class Translations
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
