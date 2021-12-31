using BigBoxVoiceSearch.DataAccess;
using BigBoxVoiceSearch.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Threading.Tasks;

namespace BigBoxVoiceSearch.VoiceSearch
{
    public delegate void RecognitionCompletedDelegate(SpeechRecognizerResult speechRecognizerResult);

    public class VoiceSearcher
    {
        private readonly RecognitionCompletedDelegate recognitionCompletedDelegate;
        private SpeechRecognitionEngine recognizer;
        private SpeechRecognizerResult speechRecognizerResult;

        public bool IsInitialized { get; private set; }

        public VoiceSearcher(RecognitionCompletedDelegate _recognitionCompletedDelegate)
        {
            IsInitialized = false;
            recognitionCompletedDelegate = _recognitionCompletedDelegate ?? throw new ArgumentNullException(nameof(_recognitionCompletedDelegate));
        }

        public async Task<bool> Initialize()
        {
            try
            {
                return await Task.Run(() =>
                {
                    List<string> gameTitlePhrases = GameTitleGrammarBuilder.GetGameTitleGrammar();

                    Choices choices = new Choices();
                    choices.Add(gameTitlePhrases.Distinct().ToArray());                    

                    GrammarBuilder grammarBuilder = new GrammarBuilder();
                    grammarBuilder.Append(choices);

                    Grammar grammar = new Grammar(grammarBuilder) { Name = "Game title elements" };

                    recognizer = new SpeechRecognitionEngine
                    {
                        InitialSilenceTimeout = TimeSpan.FromSeconds(BigBoxVoiceSearchSettingsDataProvider.Instance.BigBoxVoiceSearchSettings.VoiceSearchTimeoutInSeconds)
                    };
                    recognizer.RecognizeCompleted += Recognizer_RecognizeCompleted;
                    recognizer.LoadGrammarAsync(grammar);
                    recognizer.SpeechHypothesized += Recognizer_SpeechHypothesized;
                    recognizer.SetInputToDefaultAudioDevice();
                    recognizer.RecognizeAsyncCancel();

                    IsInitialized = true;

                    return true;
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "Initializing voice searcher");
            }

            return false;
        }

        public void DoVoiceSearch()
        {
            if (!IsInitialized)
            {
                return;
            }

            speechRecognizerResult = new SpeechRecognizerResult();
            recognizer.RecognizeAsync(RecognizeMode.Single);
        }

        public void TryCancelSearch()
        {
            try
            {
                if (recognizer != null)
                {
                    recognizer.RecognizeAsyncCancel();
                }
            }
            finally
            {

            }
        }

        private void Recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            if (GameTitleGrammar.IsNoiseWord(e.Result.Text))
            {
                return;
            }

            speechRecognizerResult.RecognizedPhrases.Add(new RecognizedPhrase()
            {
                Confidence = e.Result.Confidence,
                Phrase = e.Result.Text
            });
        }

        private void Recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e?.Error != null)
            {
                if (recognizer != null)
                {
                    recognizer.RecognizeAsyncCancel();
                }

                speechRecognizerResult.ErrorMessage = e.Error.Message;
            }

            if (e?.InitialSilenceTimeout == true || e?.BabbleTimeout == true)
            {
                if (recognizer != null)
                {
                    recognizer.RecognizeAsyncCancel();
                }

                speechRecognizerResult.ErrorMessage = "Voice recognition did not hear anything";
            }

            recognitionCompletedDelegate(speechRecognizerResult);
        }
    }
}
