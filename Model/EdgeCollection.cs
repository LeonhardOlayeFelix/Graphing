using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class EdgeCollection<T> : IEdgeCollection<T>
    {
        private readonly IList<IEdge<T>> _edges = new List<IEdge<T>>();

        public IList<IEdge<T>> Edges => _edges;

        public int Cost
        {
            get
            {
                int total = 0;
                foreach (var e in _edges) total += e.Cost;
                return total;
            }
        }

        public void Add(IEdge<T> edge)
        {
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            _edges.Add(edge);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (IEdge<T> edge in Edges)
            {
                ret += edge + "\n";
            }
            return ret;
        }
    }
}
