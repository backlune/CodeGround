using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGround.CodingDojo.BowlingGameKata
{
    internal class BowlingGame
    {
        private const int FRAMESINALINE = 10;
        private Frame _currentFrame;
        private Frame _previousFrame;

        private List<Frame> CompletedFrames { get; }

        public BowlingGame()
        {

            _currentFrame = new Frame();
            CompletedFrames = new List<Frame>();
        }

        public bool LineCompleted => CompletedFrames.Count == FRAMESINALINE;

        public int Result => CompletedFrames.Select(f => f.Score).Sum();

        internal void Try(int result)
        {
            _currentFrame.Tries++;
            _currentFrame.Score += result;

            if (_previousFrame != null)
            {
                _previousFrame.Score += result;

                if (!_previousFrame.IsStrike || _currentFrame.Tries == 2)
                {
                    CompletedFrames.Add(_previousFrame);
                    _previousFrame = null;
                }
            }

            if (_currentFrame.Score == 10)
            {
                _previousFrame = _currentFrame;
                _previousFrame.IsStrike = _currentFrame.Tries == 1;
                _currentFrame = new Frame();
                return;
            }

            if (_currentFrame.Tries == 2)
            {
                CompletedFrames.Add(_currentFrame);
                _currentFrame = new Frame();
            }
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