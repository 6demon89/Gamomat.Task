namespace SlotMachine.Core.Model
{
    public class WinLine
    {
        public int[,] WinLinePossitions { get; private set; }
        public SlotSymbol Symbol { get; set; }
        public WinLine(int[,] _position, SlotSymbol _symbol)
        {
            WinLinePossitions = _position;
            Symbol = _symbol;
        }
    }
}
