using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class Path<T> : IPath<T>
    {
        private IList<INode<T>> _nodes;

        public IList<INode<T>> Nodes => _nodes;
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
                        throw new InvalidOperationException($"Path is invalid: no edge between node {_nodes[i].ID} and node {_nodes[i + 1].ID}.");
                    }
                    total += neighbour.Cost;
                }
                return total;
            }
        }
        public Path()
        {
            _nodes = new List<INode<T>>();
        }
        public void Append(INode<T> node)
        {
            if (node == null) throw new InvalidGraphOperationException($"Cannot append a null node to the path.");
            if (_nodes.Contains(node)) throw new InvalidGraphOperationException($"Cannot append node {node.ID} to path. It is already a member");
            if (Nodes.Count != 0 && !_nodes.Last().IsNeighbourOf(node)) throw new InvalidGraphOperationException($"Cannot append node {node.ID} to the path. There is no connection from node {_nodes.Last()}.");

            _nodes.Add(node);
        }
        public override string ToString()
        {
            return string.Join(" -> ", Nodes) + $" Cost: {Cost}";
        }
    }
}
