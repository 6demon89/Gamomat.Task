namespace SlotMachine.Core.Model
{
    public class GameResult
    {
        public List<WinLine> WiningLines { get;private set; }
        public int WinningAmount { get; private set; }
        public SlotSymbol[][] CurrentMatrix { get; private set; }

        public GameResult(List<WinLine> winLines,int winningAmount,SlotSymbol[][] currentMatrix)
        {
            WiningLines = winLines;
            WinningAmount = winningAmount;
            CurrentMatrix = currentMatrix;
        }
    }
}
