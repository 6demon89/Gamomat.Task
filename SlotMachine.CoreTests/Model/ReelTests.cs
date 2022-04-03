using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine.Core.Model.Tests
{
    [TestClass()]
    public class ReelTests
    {
        SlotSymbol[] symbolTypes = PredefinedData.symbolTypes;
        Random random = new Random();


        [TestMethod()]
        public void GetSymbolsFromPositionTest()
        {
            var symbols = new List<SlotSymbol>();
            uint symbolsPerReel = 20;
            for (int i = 0; i < symbolsPerReel; i++)
                symbols.Add(symbolTypes[random.Next(symbolTypes.Count())]);
            var model = new Reel(symbols.ToArray());

            int RequestPosition = 0;
            var ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(symbols[i], ReelSymbols.ElementAt(i));
            RequestPosition = 1;
            ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);

            for (int i = (int)RequestPosition; i < (RequestPosition+3); i++)
                Assert.AreEqual(symbols[i], ReelSymbols.ElementAt((int)(i-RequestPosition)));

            RequestPosition = 17;
            ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);
            for (int i = (int)RequestPosition; i < (RequestPosition + 3); i++)
                Assert.AreEqual(symbols[i], ReelSymbols.ElementAt((int)(i - RequestPosition)));

            RequestPosition = 18;
            ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);
            Assert.AreEqual(symbols[18], ReelSymbols.ElementAt(0));
            Assert.AreEqual(symbols[19], ReelSymbols.ElementAt(1));
            Assert.AreEqual(symbols[0], ReelSymbols.ElementAt(2));

            RequestPosition = 19;
            ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);
            Assert.AreEqual(symbols[19], ReelSymbols.ElementAt(0));
            Assert.AreEqual(symbols[0], ReelSymbols.ElementAt(1));
            Assert.AreEqual(symbols[1], ReelSymbols.ElementAt(2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSymbolFromPositionExceptionTest()
        {
            var symbols = new List<SlotSymbol>();
            uint symbolsPerReel = 20;
            for (int i = 0; i < symbolsPerReel; i++)
                symbols.Add(symbolTypes[random.Next(symbolTypes.Count())]);
            var model = new Reel(symbols.ToArray());

            int RequestPosition = 20;
            var ReelSymbols = model.GetSymbolsFromPosition(RequestPosition, 3);

        }
    }
}