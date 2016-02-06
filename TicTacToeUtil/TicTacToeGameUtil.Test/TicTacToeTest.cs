using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeGameUtil.Test
{
    [TestClass]
    public class TicTacToeTest
    {
        [TestMethod]
        public void TestNextPlayerAs1()
        {
            var g = new TicTacToeGame(12, 5);

            Assert.AreEqual(g.NextPlayer, 1);
        }

        [TestMethod]
        public void TestNextPlayerAs2()
        {
            var g = new TicTacToeGame(123, 5);

            Assert.AreEqual(g.NextPlayer, 2);
        }
        [TestMethod]
        public void TestP1IsWin()
        {
            var g = new TicTacToeGame(1635, 2);

            Assert.IsTrue(g.IsWin);
        }

        [TestMethod]
        public void TestP1IsWinAndNextStateSeq()
        {
            var g = new TicTacToeGame(1635, 2);

            Assert.IsTrue(g.IsWin);

            var t = new List<int>() { 1635, 16, 0 };
            CollectionAssert.AreEqual(t, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

            t = new List<int>() { 16352, 163, 1 };
            CollectionAssert.AreEqual(t, g.GetLastPlayersStateSequence().Select(p => p.NextStateToken).ToList());
        }
        [TestMethod]
        public void TestP1IsWinAndNextStateSeqWithFullStateTokenConstructor()
        {
            var g = new TicTacToeGame(16352);

            Assert.IsTrue(g.IsWin);

            var t = new List<int>() { 1635, 16, 0 };
            CollectionAssert.AreEqual(t, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

            t = new List<int>() { 16352, 163, 1 };
            CollectionAssert.AreEqual(t, g.GetLastPlayersStateSequence().Select(p => p.NextStateToken).ToList());
        }

        [TestMethod]
        public void TestP2IsWin()
        {
            var g = new TicTacToeGame(16359, 4);

            Assert.IsTrue(g.IsWin);
        }

        [TestMethod]
        public void TestIsBlock()
        {
            var g = new TicTacToeGame(163, 2);

            Assert.IsTrue(g.IsBlock);
        }

        [TestMethod]
        public void TestIsSecondBlock()
        {
            var g = new TicTacToeGame(16324, 7);

            Assert.IsTrue(g.IsBlock);
        }
        [TestMethod]
        public void TestIsSecondBlockOtherPlayer()
        {
            var g = new TicTacToeGame(1532, 8);

            Assert.IsTrue(g.IsBlock);
        }
        [TestMethod]
        public void TestIsBlockTest()
        {
            var g = new TicTacToeGame(124, 7);

            Assert.IsTrue(g.IsBlock);
        }
        [TestMethod]
        public void TestIsBlockAndSequenceForReward()
        {
            var g = new TicTacToeGame(124, 7);

            Assert.IsTrue(g.IsBlock);

            var pastStates = new List<int>()
            {
                {124},
                {1}
            };

            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

            pastStates = new List<int>()
            {
                {1247},
                {12}
            };

            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.NextStateToken).ToList());
        }

        [TestMethod]
        public void TestNextPossiblePlays()
        {
            var g = new TicTacToeGame(null, 1);
            var p = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };

            CollectionAssert.AreEqual(g.GetPossiblePlays(), p);

            g = new TicTacToeGame(1, 2);
            p = new List<int>() { 3, 4, 5, 6, 7, 8, 9 };

            CollectionAssert.AreEqual(g.GetPossiblePlays(), p);

            g = new TicTacToeGame(12, 3);
            p = new List<int>() { 4, 5, 6, 7, 8, 9 };

            CollectionAssert.AreEqual(g.GetPossiblePlays(), p);

        }

        [TestMethod]
        public void TestIsTie()
        {
            var g = new TicTacToeGame(16324795, 8);

            Assert.IsTrue(g.IsTie);
        }
        [TestMethod]
        public void TestP2WinnerSequences()
        {
            var g = new TicTacToeGame(16359, 4);

            Assert.IsTrue(g.IsWin);

            var pastStates = new List<int>()
            {
                {16359},
                {163},
                {1}
            };

            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

        }
        [TestMethod]
        public void TestP2WinnerSequences2()
        {
            var g = new TicTacToeGame(1632957, 8);

            Assert.IsTrue(g.IsWin);

            var pastStates = new List<int>()
            {
                {1632957},
                {16329},
                {163},
                {1}
            };

            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

        }
        [TestMethod]
        public void TestP2WinnerSequences3()
        {
            var g = new TicTacToeGame(1632957, 8);

            Assert.IsTrue(g.IsWin);

            var pastStates = new List<int>()
            {
                {1632957},
                {16329},
                {163},
                {1}
            };


            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

        }
        [TestMethod]
        public void TestP2WinnerSequences3WithFullStateTokenConstructor()
        {
            var g = new TicTacToeGame(16329578);

            Assert.IsTrue(g.IsWin);

            var pastStates = new List<int>()
            {
                {1632957},
                {16329},
                {163},
                {1}
            };


            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.StateToken).ToList());

        }
        [TestMethod]
        public void TestP2WinnerNextStatesSeq()
        {
            var g = new TicTacToeGame(1632957, 8);

            Assert.IsTrue(g.IsWin);

            var pastStates = new List<int>()
            {
                {16329578},
                {163295},
                {1632},
                {16}
            };

            CollectionAssert.AreEqual(pastStates, g.GetLastPlayersStateSequence().Select(p => p.NextStateToken).ToList());


        }

        [TestMethod]
        public void TestTieSequence()
        {
            var g = new TicTacToeGame(17325986, 4);

            Assert.IsTrue(g.IsTie);

            var pastStates = new List<int>()
            {
                {173259864},
                {17325986},
                {1732598},
                {173259},
                {17325},
                {1732},
                {173},
                {17},
                {1},
            };

            CollectionAssert.AreEqual(pastStates, g.GetAllStateSequence().Select(p => p.NextStateToken).ToList());

            g = new TicTacToeGame(17325984, 6);

            Assert.IsTrue(g.IsTie);

            pastStates = new List<int>()
            {
                {173259846},
                {17325984},
                {1732598},
                {173259},
                {17325},
                {1732},
                {173},
                {17},
                {1},
            };

            CollectionAssert.AreEqual(pastStates, g.GetAllStateSequence().Select(p => p.NextStateToken).ToList());
        }


    }
}
