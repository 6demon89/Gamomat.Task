namespace SlotMachine.Core.Model
{
    public class SlotSymbol : IEquatable<SlotSymbol>
    {
        readonly byte[] m_SymbolName;
        readonly uint m_ID;

        public uint SymbolID { get => m_ID; }

        public byte[] Name { get => m_SymbolName; }

        public int WinningAmount { get; private set; }

        public SlotSymbol(uint _id, byte[] _SymbolName, int? winAmount)
        {
            m_ID = _id;
            m_SymbolName = _SymbolName;
            if (winAmount.HasValue)
                WinningAmount = winAmount.Value;
            else
                WinningAmount = (int)(_id + 1) * 10;
        }

        public void ChangeWinningamount(int newWinAmount) => WinningAmount = newWinAmount;

        public bool Equals(SlotSymbol? other)
        {
            if (other is null)
                throw new NullReferenceException();
            if (this.SymbolID != other.SymbolID)
                return false;
            return true;
        }
    }
}
