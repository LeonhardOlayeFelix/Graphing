using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public interface IEdge<T> : IEquatable<IEdge<T>>
    {
        public INode<T> Node1 { get; set; }
        public INode<T> Node2 { get; set; }
        public int Cost { get; set; }
    }
    public class Edge<T> : IEdge<T>
    {
        private INode<T> _node1;
        private INode<T> _node2;
        private int _cost;

        public INode<T> Node1 { get => _node1; set => _node1 = value; }
        public INode<T> Node2 { get => _node2; set => _node2 = value; }
        public int Cost { get => _cost; set => _cost = value; }

        public Edge(INode<T> node1, INode<T> node2, int cost)
        {
            Node1 = node1;
            Node2 = node2;
            Cost = cost;
        }


        public bool Equals(IEdge<T>? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (Node1.ID == other.Node1.ID && Node2.ID == other.Node2.ID) || (Node1.ID == other.Node2.ID && Node2.ID == other.Node1.ID);
        }
        public override bool Equals(object? obj) => obj is IEdge<T> other && Equals(other);
        public override int GetHashCode()
        {
            int a = Node1.ID;
            int b = Node2.ID;
            if (a > b) (a, b) = (b, a);
            return HashCode.Combine(a, b);
        }
        public static bool operator ==(Edge<T>? left, Edge<T>? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Edge<T>? left, Edge<T>? right) => !(left == right);
        public override string ToString()
        {
            return $"Edge from {Node1} to {Node2} cost {Cost}";
        }
    }
}
