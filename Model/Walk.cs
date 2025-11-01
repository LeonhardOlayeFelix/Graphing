using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class Walk<T> : IWalk<T>
    {
        private readonly IList<INode<T>> _nodes = new List<INode<T>>();

        public int Cost
        {
            get
            {
                if (_nodes == null || _nodes.Count <= 1) return 0;

                int total = 0;
                for (int i = 0; i < _nodes.Count - 1; i++)
                {
                    var neighbour = _nodes[i].Neighbours.FirstOrDefault(n => n.Node.Equals(_nodes[i + 1]));
                    if (neighbour == null)
                    {
                        throw new InvalidOperationException($"Walk is invalid: no edge between node {_nodes[i].ID} and node {_nodes[i + 1].ID}.");
                    }
                    total += neighbour.Cost;
                }
                return total;
            }
        }

        public IList<INode<T>> Nodes => _nodes;

        public void Append(INode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            _nodes.Add(node);
        }
        public override string ToString()
        {
            return string.Join(" -> ", Nodes) + $" Cost: {Cost}";
        }
    }
}
