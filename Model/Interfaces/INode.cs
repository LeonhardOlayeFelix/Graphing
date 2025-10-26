using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    /// <summary>
    /// A class representing a node in the graph
    /// </summary>
    /// <typeparam name="T">The type of data that each node stores.</typeparam>
    public interface INode<T> : IEquatable<INode<T>>
    {
        public T Data { get; }
        public int ID { get; }
        public IList<INeighbour<T>> Neighbours { get; }
        public void RegisterNeighbour(INode<T> node, int cost);
        public void UnregisterNeighbour(INode<T> node);
        public bool IsNeighbourOf(INode<T> node);
        public void UnregisterAll();
    }

    public interface INeighbour<T>
    {
        public INode<T> Node { get; }
        public int Cost { get; }
    }
}
