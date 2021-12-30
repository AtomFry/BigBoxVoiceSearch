using System;
using System.Collections.Generic;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.VoiceSearch
{
    // creates the components of the voice search grammar for each game
    public class GameTitleGrammarBuilder
    {
        private readonly string[] ForwardSlashSplitter = new string[1] { "/" };

        public List<GameTitleGrammar> GameTitleGrammars { get; set; }

        public GameTitleGrammarBuilder(IGame _game)
        {
            GameTitleGrammars = new List<GameTitleGrammar>();
            string[] gameTitles = _game.Title.Split(ForwardSlashSplitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string gameTitle in gameTitles)
            {
                GameTitleGrammars.Add(new GameTitleGrammar(gameTitle.Trim()));
            }
        }

        public static List<string> GetGameTitleGrammar()
        {
            IGame[] games = PluginHelper.DataManager.GetAllGames();

            List<string> gameTitlePhrases = new List<string>();

            foreach (IGame game in games)
            {
                GameTitleGrammarBuilder gameTitleGrammarBuilder = new GameTitleGrammarBuilder(game);

                foreach (GameTitleGrammar gameTitleGrammar in gameTitleGrammarBuilder.GameTitleGrammars)
                {
                    if (!string.IsNullOrWhiteSpace(gameTitleGrammar.Title))
                    {
                        gameTitlePhrases.Add(gameTitleGrammar.Title);
                    }

                    if (!string.IsNullOrWhiteSpace(gameTitleGrammar.MainTitle))
                    {
                        gameTitlePhrases.Add(gameTitleGrammar.MainTitle);
                    }

                    if (!string.IsNullOrWhiteSpace(gameTitleGrammar.Subtitle))
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

            return gameTitlePhrases;
        }

    }
}
