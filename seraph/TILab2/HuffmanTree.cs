using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kod
{
    internal class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, double> Frequencies = new Dictionary<char, double>();

        public void Build(Dictionary<char, double> organ_harvest)
        {
            Frequencies = organ_harvest;

            foreach (KeyValuePair<char, double> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[1],
                        Right = taken[0]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }
        }

        public List<string> ReturnAlphabet()
        {
            List<string> tmp = new List<string>();

            foreach (KeyValuePair<char, double> symbol in Frequencies)
            {
                List<bool> encodedSymbol = this.Root.Traverse(symbol.Key, new List<bool>());
                tmp.Add(new string(encodedSymbol.Select(x => x ? '0' : '1').ToArray()));
            }

            return tmp;
        }
    }
}
