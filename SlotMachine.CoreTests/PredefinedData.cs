using SlotMachine.Core.Model;
using System.Text;

namespace SlotMachine.Core.Tests
{
    internal class PredefinedData
    {
        internal static SlotSymbol[] symbolTypes = new SlotSymbol[3]
        {
            new SlotSymbol(0, Encoding.ASCII.GetBytes("A"),null),
            new SlotSymbol(1, Encoding.ASCII.GetBytes("B"),null),
            new SlotSymbol(2, Encoding.ASCII.GetBytes("C"),null)
        };

    }
}