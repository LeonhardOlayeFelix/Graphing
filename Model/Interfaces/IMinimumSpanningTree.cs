using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    public interface IMinimumSpanningTree<T> : IEdgeCollection<T>
    {
        public int Cost { get; }
        public void Add(IEdge<T> edge);
    }
}
