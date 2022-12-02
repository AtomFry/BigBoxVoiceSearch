using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.VoiceSearch
{
    // creates the components of the voice search grammar for each game
    public class GameTitleGrammarBuilder
    {
        private readonly string[] ForwardSlashSplitter = new string[1] { "/" };

        public List<GameTitleGrammar> GameTitleGrammars { get; set; }
        
        public GameTitleGrammarBuilder(string _title)
        {
            GameTitleGrammars = new List<GameTitleGrammar>();

            // remove double quotes - they cannot be present in the voice recognition grammar
            string cleanTitle = _title.Replace("\"", " ");

            string[] gameTitles = cleanTitle.Split(ForwardSlashSplitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string gameTitle in gameTitles)
            {
                GameTitleGrammars.Add(new GameTitleGrammar(gameTitle.Trim()));
            }
        }

        public static List<string> GetGrammar(string title)
        {
            List<string> gameTitlePhrases = new List<string>();

            if (string.IsNullOrWhiteSpace(title))
            {
                return gameTitlePhrases;
            }

            GameTitleGrammarBuilder gameTitleGrammarBuilder = new GameTitleGrammarBuilder(title);

            foreach (GameTitleGrammar gameTitleGrammar in gameTitleGrammarBuilder.GameTitleGrammars)
            {
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

            return gameTitlePhrases;
        }

        public static List<string> GetGameTitleGrammar()
        {
            ConcurrentBag<string> gamePhraseBag = new ConcurrentBag<string>();

            IGame[] games = PluginHelper.DataManager.GetAllGames();

            List<string> gameTitlePhrases = new List<string>();

            Parallel.ForEach(games, game =>
            {
                List<string> phrases = GetGrammar(game.Title);

                IAlternateName[] alternateNames = game.GetAllAlternateNames();
                foreach (IAlternateName alternateName in alternateNames)
                {
                    phrases.AddRange(GetGrammar(alternateName.Name));
                }

                foreach (string phrase in phrases)
                {
                    gamePhraseBag.Add(phrase);
                }
            });

            return gamePhraseBag.ToList();
        }

    }
}
