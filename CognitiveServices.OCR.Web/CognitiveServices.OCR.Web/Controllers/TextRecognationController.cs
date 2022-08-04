using CognitiveServices.OCR.Service;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveServices.OCR.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextRecognationController : ControllerBase
    {
        private readonly ITextRecogniationService _textRecogniationService;

        public TextRecognationController(ITextRecogniationService textRecogniationService)
        {
            _textRecogniationService = textRecogniationService;
        }

        [HttpGet]
        public async Task<TextRecognationModel> Get()
        {
            var mockedImage = new FileStream("test2.png", FileMode.Open);

            var recognizedLines = await _textRecogniationService.ReadTextFromImageAsync(mockedImage);

            return new TextRecognationModel
            {
                RecognizedLines = recognizedLines,
            };
        }
    }
}