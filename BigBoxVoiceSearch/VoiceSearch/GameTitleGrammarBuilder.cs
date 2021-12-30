using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.VoiceSearch
{
    // creates the components of the voice search grammar for each game
    public class GameTitleGrammarBuilder
    {
        private readonly string[] ForwardSlashSplitter = new string[1] { "/" };

        public List<GameTitleGrammar> gameTitleGrammars { get; set; }

        public GameTitleGrammarBuilder(IGame _game)
        {
            gameTitleGrammars = new List<GameTitleGrammar>();
            string[] gameTitles = _game.Title.Split(ForwardSlashSplitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string gameTitle in gameTitles)
            {
                gameTitleGrammars.Add(new GameTitleGrammar(gameTitle.Trim()));
            }
        }
    }
}
