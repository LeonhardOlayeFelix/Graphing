using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    public interface INode<T> : IEquatable<INode<T>>
    {
        public T Data { get; }
        public int ID { get; }
        public IList<INeighbour<T>> Neighbours { get; }
        public void RegisterNeighbour(INode<T> node, int cost);
        public void UnregisterNeighbour(INode<T> node);
        public bool IsNeighbourOf(INode<T> node);
    }

    public interface INeighbour<T>
    {
        public INode<T> Node { get; }
        public int Cost { get; }
    }
}
