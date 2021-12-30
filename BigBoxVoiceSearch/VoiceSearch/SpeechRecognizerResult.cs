using System.Collections.Generic;

namespace BigBoxVoiceSearch.VoiceSearch
{
    public class SpeechRecognizerResult
    {
        public List<RecognizedPhrase> RecognizedPhrases { get; set; } = new List<RecognizedPhrase>();
        public string ErrorMessage { get; set; }
    }
}
