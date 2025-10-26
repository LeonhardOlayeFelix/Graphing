using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class MinimumSpanningTree<T> : IMinimumSpanningTree<T>
    {
        private readonly IList<IEdge<T>> _edges;

        public IList<IEdge<T>> Edges => _edges;

        public MinimumSpanningTree()
        {
            _edges = new List<IEdge<T>>();
        }

        public int Cost
        {
            get
            {
                int total = 0;
                foreach (IEdge<T> edge in Edges)
                {
                    total += edge.Cost;
                }
                return total;
            }
        }
        public void Add(IEdge<T> edge)
        {
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            if (edge.Node1 == null || edge.Node2 == null) throw new ArgumentException("Edge must reference two nodes.", nameof(edge));
            if (edge.Node1.Equals(edge.Node2)) throw new InvalidOperationException($"Cannot add self-loop edge ({edge.Node1.ID} -> {edge.Node2.ID}).");
            if (_edges.Contains(edge)) throw new InvalidOperationException("Edge already exists in the minimum spanning tree.");

            _edges.Add(edge);
        }

        public override string ToString()
        {
            return $"MST: {Edges.Count} edges, Cost={Cost}";
        }
    }
}
