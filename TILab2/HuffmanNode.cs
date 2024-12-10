using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kod
{
    internal class HuffmanNode
    {
            public int Frequency { get; set; }
            public char Character { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }
     }
}
