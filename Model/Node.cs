using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// A class representing a node in the graph
    /// </summary>
    /// <typeparam name="T">The type of data that each node stores.</typeparam>
    public interface INode<T> : IEquatable<INode<T>>
    {
        public T Data { get; }
        IList<INeighbour<T>> Neighbours { get; }
        public int ID { get; }
    }
    public interface INeighbour<T>
    {
        public INode<T> Node { get; }
        public int Cost { get; }
    }

    public class Node<T> : INode<T>
    {
        private T _data;
        private IList<INeighbour<T>> _neighbours;
        private readonly int _id;

        public T Data => _data;
        public int ID => _id;
        public IList<INeighbour<T>> Neighbours => _neighbours;
        internal Node(T data, int id)
        {
            _data = data;
            _id = id;
            _neighbours = new List<INeighbour<T>>();
        }
        public bool Equals(INode<T>? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other.ID;
        }
        public override bool Equals(object? obj) => obj is Node<T> other && Equals(other);
        public override int GetHashCode() => _id.GetHashCode();
        public static bool operator ==(Node<T>? left, Node<T>? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Node<T>? left, Node<T>? right) => !(left == right);

    }

    public class Neighbour<T> : INeighbour<T>
    {
        private INode<T> _node;
        private int _cost;

        public INode<T> Node => _node;
        public int Cost => _cost;

        public Neighbour(INode<T> node, int cost)
        {
            _node = node;
            _cost = cost;
        }
    }

}
