using System.Collections.Generic;
using System.Linq;

namespace CodeGround.CodingDojo.BowlingGameKata
{
    internal class BowlingGame
    {
        private const int FRAMESINALINE = 10;
        private Frame currentFrame;
        private Frame previousFrame;

        private List<Frame> CompletedFrames { get; }

        public BowlingGame()
        {
            currentFrame = new Frame();
            CompletedFrames = new List<Frame>();
        }

        public bool LineCompleted => CompletedFrames.Count == FRAMESINALINE;

        public int Result => CompletedFrames.Select(f => f.Score).Sum();

        internal void Try(int result)
        {
            currentFrame.Tries++;
            currentFrame.Score += result;

            if (previousFrame != null)
            {
                previousFrame.Score += result;

                if (!previousFrame.IsStrike || currentFrame.Tries == 2)
                {
                    CompletedFrames.Add(previousFrame);
                    previousFrame = null;
                }
            }

            if (currentFrame.Score == 10)
            {
                previousFrame = currentFrame;
                previousFrame.IsStrike = currentFrame.Tries == 1;
                currentFrame = new Frame();
                return;
            }

            if (currentFrame.Tries == 2)
            {
                CompletedFrames.Add(currentFrame);
                currentFrame = new Frame();
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