using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGround.CodingDojo.BowlingGameKata
{
    internal class BowlingGame
    {
        private Player _player;

        public BowlingGame()
        {
            _player = new Player();
            Result.Scores.Add(_player, new List<int>());
        }

        public BowlingResult Result { get; internal set; } = new BowlingResult();

        internal void Try(int result)
        {
            Result.Scores[_player].Add(result);
        }
    }

    internal class BowlingResult
    {
        private readonly Dictionary<Player, List<int>> scores = new Dictionary<Player, List<int>>();

        public Dictionary<Player, List<int>> Scores { get => scores; }
        public bool GameComplete => Scores.All(x => x.Value.Count() == 20);

        internal int GetCurrentScore()
        {
            return scores.First().Value.Sum();
        }
    }
}