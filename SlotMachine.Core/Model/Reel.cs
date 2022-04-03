namespace SlotMachine.Core.Model
{
    public class Reel
    {
        private readonly SlotSymbol[] m_Symbol;
        public int PossitionsAvalable { get => m_Symbol.Length - 1; }
        public Reel(SlotSymbol[] _Symbol) => m_Symbol = _Symbol;

        /// <summary>
        /// Get symbols from the Reel starting from the provided position.
        /// Reel symbols will start from the beginning of an array, if the requested count will go over the array boundaries
        /// </summary>
        /// <param name="newPosition">
        /// Starting position for receiving symbols
        /// </param>
        /// <param name="symbolsCountToGet">
        /// Amount of symbols to be received
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Will be thrown if the new position request is out of array boundaries
        /// </exception>
        public IEnumerable<SlotSymbol> GetSymbolsFromPosition(int newPosition, int symbolsCountToGet)
        {
            if (newPosition > m_Symbol.Length - 1) 
                throw new ArgumentOutOfRangeException();
            var result = new List<SlotSymbol>(symbolsCountToGet);
            int Index = newPosition;
            for (var i = 0; i < symbolsCountToGet; i++)
            {
                result.Add(m_Symbol[Index]);
                Index++;
                if (Index >= m_Symbol.Length) Index = 0;
            }
            return result;
        }
    }
}
