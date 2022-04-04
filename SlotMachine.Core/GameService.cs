using SlotMachine.Core.Model;

namespace SlotMachine.Core
{
    public class GameService
    {
        readonly Reel[] m_Reels;
        readonly SlotSymbol[] m_SlotSymbols;
        readonly Random m_Randomg;
        readonly int m_SymbolsCount;

        public GameService(SlotSymbol[] symbolsTypes, int reelsSymbolsCount = 20)
        {
            m_Reels = new Reel[3];
            m_SlotSymbols = symbolsTypes;
            m_SymbolsCount = 3;
            m_Randomg = new Random();
            for (int r = 0; r < 3; r++)
            {
                var symbols = new List<SlotSymbol>();
                for (int i = 0; i < reelsSymbolsCount; i++)
                    symbols.Add(m_SlotSymbols[m_Randomg.Next(m_SlotSymbols.Count())]);
                m_Reels[r] = new Reel(symbols.ToArray());
            }
        }

        internal List<SlotSymbol[]> RandomizeReelsPosition()
        {
            List<SlotSymbol[]> result = new List<SlotSymbol[]>();
            foreach (var item in m_Reels)
                result.Add(item.GetSymbolsFromPosition(m_Randomg.Next(item.PossitionsAvalable), m_SymbolsCount).ToArray());
            return result;
        }

        internal int GetWinningamount(List<WinLine> _winLines)
        {
            var winAmount = 0;
            foreach (var item in _winLines)
                winAmount += item.Symbol.WinningAmount;
            return winAmount;
        }

        internal List<WinLine> GetWinLines(List<SlotSymbol[]> _currentMatrix)
        {
            var result = new List<WinLine>();
            for (int row = 0; row < _currentMatrix.Count; row++)
            {
                if (_currentMatrix[0][row].SymbolID == _currentMatrix[1][row].SymbolID &&
                    _currentMatrix[1][row].SymbolID == _currentMatrix[2][row].SymbolID)
                    result.Add(new WinLine(new int[,] { { 0, row }, { 1, row }, { 2, row } }, _currentMatrix[row][0]));
            }
            if (_currentMatrix[0][0].SymbolID == _currentMatrix[1][1].SymbolID &&
                _currentMatrix[1][1].SymbolID == _currentMatrix[2][2].SymbolID)
                result.Add(new WinLine(new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 } }, _currentMatrix[0][0]));
            if (_currentMatrix[0][2].SymbolID == _currentMatrix[1][1].SymbolID &&
                _currentMatrix[1][1].SymbolID == _currentMatrix[2][0].SymbolID)
                result.Add(new WinLine(new int[,] { { 0, 2 }, { 1, 1 }, { 2, 0 } }, _currentMatrix[0][2]));
            return result;
        }

        /// <summary>
        /// This method will randomize the slotmachine matrix and calculate the winning line including winning amount
        /// </summary>
        /// <returns>
        /// GameResult which contains the new matrix and win state information
        /// </returns>
        public GameResult Play()
        {
            List<SlotSymbol[]> currentMatrix = RandomizeReelsPosition();
            foreach (var item in m_Reels)
                currentMatrix.Add(item.GetSymbolsFromPosition(m_Randomg.Next(item.PossitionsAvalable), m_SymbolsCount).ToArray());
            var winningLines = GetWinLines(currentMatrix);
            return new GameResult(winningLines, GetWinningamount(winningLines), currentMatrix.ToArray());
        }

    }
}