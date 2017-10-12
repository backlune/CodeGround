using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGround.CodingDojo.BowlingGameKata
{
    [TestClass]
    public class BowlingGameMSTestTests
    {

        [TestMethod]
        public void ALineIsFinishedAfterTenFrames()
        {
            var sut = new BowlingGame();

            for (int i = 0; i < 10; i++)
            {
                sut.Try(1);
                sut.Try(1);
            }

            Assert.IsTrue(sut.LineCompleted);
        }

        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(4, 3, 7)]
        [DataRow(8, 1, 9)]
        public void WhenAllAreNotKnockedDownInAFrameTotalScoreIsTotalNumberOfPins(int tryOnePins, int tryTwoPins, int expectedResult)
        {
            var sut = new BowlingGame();

            sut.Try(tryOnePins);
            sut.Try(tryTwoPins);

            Assert.AreEqual(expectedResult, sut.Result);
        }
    }
}
