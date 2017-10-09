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

            var result = sut.Result;
            Assert.True(result.GameComplete);
        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(4, 3, 7)]
        [InlineData(8, 1, 9)]
        public void WhenAllAreNotKnockedDownInAFrameTotalScoreIsTotalNumberOfPins(int tryOnePins, int tryTwoPins, int expectedResult)
        {
            var sut = new BowlingGame();

            sut.Try(tryOnePins);
            sut.Try(tryTwoPins);

            Assert.Equal(expectedResult, sut.Result.GetCurrentScore());
        }
    }
}
