using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine.Core.Model;
using System.Collections.Generic;

namespace SlotMachine.Core.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        GameService game;
        SlotSymbol[] symbolTypes = PredefinedData.symbolTypes;

        [TestInitialize]
        public void Init()
        {
            game = new GameService(symbolTypes);
        }

        [TestMethod]
        public void GetWinningamountTest()
        {
            var data = new List<WinLine>();
            data.Add(new WinLine(new int[,] { }, symbolTypes[0]));
            var result = game.GetWinningamount(data);
            Assert.AreEqual(10, result);

            data.Clear();
            result = game.GetWinningamount(data);
            Assert.AreEqual(0, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[1]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(20, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(30, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(60, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[0]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(70, result);
        }

        [TestMethod]
        public void GetWinningamountWithChangedTest()
        {
            var data = new List<WinLine>();
            symbolTypes[0].ChangeWinningamount(900);
            symbolTypes[1].ChangeWinningamount(5);
            data.Add(new WinLine(new int[,] { }, symbolTypes[0]));
            var result = game.GetWinningamount(data);
            Assert.AreEqual(900, result);

            data.Clear();
            result = game.GetWinningamount(data);
            Assert.AreEqual(0, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[1]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(5, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(30, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(60, result);

            data.Clear();
            data.Add(new WinLine(new int[,] { }, symbolTypes[0]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            data.Add(new WinLine(new int[,] { }, symbolTypes[2]));
            result = game.GetWinningamount(data);
            Assert.AreEqual(960, result);
        }


        [TestMethod]
        public void GetWinLinesTest()
        {
            List<SlotSymbol[]> CurrentMatrix = new List<SlotSymbol[]>();
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[0], symbolTypes[0] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[0], symbolTypes[0] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[0], symbolTypes[0] });
            var result = game.GetWinLines(CurrentMatrix);
            Assert.AreEqual(5, result.Count);
            CurrentMatrix.Clear();

            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[1], symbolTypes[2] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[1], symbolTypes[1], symbolTypes[0] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[1], symbolTypes[0] });
            result = game.GetWinLines(CurrentMatrix);
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEqual(result[0].WinLinePossitions, new int[,] { { 0, 1 }, { 1, 1 }, { 2, 1 } });
            CurrentMatrix.Clear();

            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[1], symbolTypes[1], symbolTypes[2] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[1], symbolTypes[1], symbolTypes[0] });
            CurrentMatrix.Add(new SlotSymbol[] { symbolTypes[0], symbolTypes[1], symbolTypes[1] });
            result = game.GetWinLines(CurrentMatrix);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEqual(result[0].WinLinePossitions, new int[,] { { 0, 1 }, { 1, 1 }, { 2, 1 } });
            CollectionAssert.AreEqual(result[1].WinLinePossitions, new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 } });
        }

    }
}