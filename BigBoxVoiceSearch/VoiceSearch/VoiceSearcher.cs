using BigBoxVoiceSearch.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.VoiceSearch
{
    public class RecognizedPhrase
    {
        public string Phrase { get; set; }
        public float Confidence { get; set; }
    }

    public class SpeechRecognizerResult
    {
        public List<RecognizedPhrase> RecognizedPhrases { get; set; } = new List<RecognizedPhrase>();
        public string ErrorMessage { get; set; }
    }

    public delegate void RecognitionCompletedDelegate(SpeechRecognizerResult speechRecognizerResult);

    public class VoiceSearcher
    {
        private RecognitionCompletedDelegate recognitionCompletedDelegate;
        private SpeechRecognitionEngine recognizer;
        private SpeechRecognizerResult speechRecognizerResult;
        
        public bool IsInitialized { get; private set; }
        public string RecognitionStatusMessage { get; private set; }

        public VoiceSearcher(RecognitionCompletedDelegate _recognitionCompletedDelegate)
        {
            IsInitialized = false;
            recognitionCompletedDelegate = _recognitionCompletedDelegate;
        }

        public async Task<bool> Initialize()
        {
            try
            {
                return await Task.Run(() =>
                {
                    IGame[] games = PluginHelper.DataManager.GetAllGames();

                    List<string> gameTitlePhrases = new List<string>();
                    
                    foreach(IGame game in games)
                    {
                        GameTitleGrammarBuilder gameTitleGrammarBuilder = new GameTitleGrammarBuilder(game);
                        
                        foreach(GameTitleGrammar gameTitleGrammar in gameTitleGrammarBuilder.gameTitleGrammars)
                        {
                            if(!string.IsNullOrWhiteSpace(gameTitleGrammar.Title))
                            {
                                gameTitlePhrases.Add(gameTitleGrammar.Title);
                            }

                            if(!string.IsNullOrWhiteSpace(gameTitleGrammar.MainTitle))
                            {
                                gameTitlePhrases.Add(gameTitleGrammar.MainTitle);
                            }

                            if(!string.IsNullOrWhiteSpace(gameTitleGrammar.Subtitle))
                            {
                                gameTitlePhrases.Add(gameTitleGrammar.Subtitle);
                            }

                            for (int i = 0; i < gameTitleGrammar.TitleWords.Count; i++)
                            {
                                StringBuilder sb = new StringBuilder();
                                for (int j = i; j < gameTitleGrammar.TitleWords.Count; j++)
                                {
                                    sb.Append($"{gameTitleGrammar.TitleWords[j]} ");
                                    if (!GameTitleGrammar.IsNoiseWord(sb.ToString().Trim()))
                                    {
                                        gameTitlePhrases.Add(sb.ToString().Trim());
                                    }
                                }
                            }
                        }
                    }

                    Choices choices = new Choices();
                    choices.Add(gameTitlePhrases.Distinct().ToArray());

                    GrammarBuilder grammarBuilder = new GrammarBuilder();
                    grammarBuilder.Append(choices);

                    Grammar grammar = new Grammar(grammarBuilder) { Name = "Game title elements" };

                    recognizer = new SpeechRecognitionEngine();

                    // todo: read from the settings file 
                    recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(5.0);
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

        /// <summary>
        /// Performs a voice search
        /// </summary>
        /// <returns>Indicates whether the search was successfully performed</returns>
        public void DoVoiceSearch()
        {
            if (!IsInitialized)
            {
                return;
            }

            speechRecognizerResult = new SpeechRecognizerResult();
            recognizer.RecognizeAsync(RecognizeMode.Single);
        }
    }
}
