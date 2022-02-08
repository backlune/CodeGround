using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodeGround.CodingDojo.BowlingGameKata
{

    public class BowlingGameTests
    {

        [Fact]
        public void ALineIsFinishedAfterTenFrames()
        {
            var sut = new BowlingGame();

            for (int i = 0; i < 10; i++)
            {
                sut.Try(1);
                sut.Try(1);
            }

            Assert.True(sut.LineCompleted);
        }

        /// <summary>
        /// 
        /// 
        /// Other testmethod names tried:
        /// WhenAllAreNotKnockedDownInAFrameTotalScoreIsTotalNumberOfPinsWhenNoStrikesOrSpares
        /// </summary>
        /// <param name="tryOnePins"></param>
        /// <param name="tryTwoPins"></param>
        /// <param name="expectedResult"></param>
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 3, 7)]
        [InlineData(8, 1, 9)]
        public void GivenNotAllPinsKnockedDownInAFrame_TotalScoreIsTotalNumberofPins(int tryOnePins, int tryTwoPins, int expectedResult)
        {
            var sut = new BowlingGame();

            sut.Try(tryOnePins);
            sut.Try(tryTwoPins);

            Assert.Equal(expectedResult, sut.Result);
        }

        [Theory]
        [InlineData(5, 5, 5)]
        [InlineData(4, 6, 6)]
        [InlineData(3, 7, 7)]
        [InlineData(1, 9, 1)]
        [InlineData(2, 8, 9)]
        public void GivenAllPinsKnockedDownInAFramAfterTwoTriesAndNextFrameKnocksFewerThanAll_TotalScoreAfterTwoFramesIsTenPlusTheScoreOfNextTry
            (int tryOnePins, int tryTwoPins, int nextTryScore)
        {
            var sut = new BowlingGame();

            sut.Try(tryOnePins);
            sut.Try(tryTwoPins);

            sut.Try(nextTryScore);

            Assert.Equal(10 + nextTryScore, sut.Result);
        }

        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(10, 6, 1)]
        [InlineData(10, 7, 1)]
        [InlineData(10, 1, 5)]
        [InlineData(10, 9, 0)]
        public void GivenAllPinsKnockedDownInFirstTryOfAFramAndNextFrameKnocksFewerThanAll_TotalScoreAfterTwoFramesIsTenPlusDoubleTheScoreOfSecondFrame
            (int tryOnePins, int nextTryScore1, int nextTryScore2)
        {
            var sut = new BowlingGame();

            sut.Try(tryOnePins);

            sut.Try(nextTryScore1);
            sut.Try(nextTryScore2);

            int frame1Score = 10 + nextTryScore1 + nextTryScore2;
            int frame2Score = nextTryScore1 + nextTryScore2;


            Assert.Equal(frame1Score + frame2Score, sut.Result);
        }
    }
}
