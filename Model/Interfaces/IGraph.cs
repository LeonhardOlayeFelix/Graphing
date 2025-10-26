using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    public interface IGraph<T>
    {
        public IList<INode<T>> Nodes { get; }
        public IList<IEdge<T>> Edges { get; }
        public INode<T> AddNode(T data);
        public INode<T> RemoveNode(INode<T> node);
        public IEdge<T> AddEdge(INode<T> node1, INode<T> node2, int cost);
        public IEdge<T> RemoveEdge(IEdge<T> edge);

    }
}
