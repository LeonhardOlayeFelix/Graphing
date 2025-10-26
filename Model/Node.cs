
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphing.Model.Interfaces;

namespace Graphing.Model
{
    public class Node<T> : INode<T>
    {
        #region Fields
        private T _data;
        private IList<INeighbour<T>> _neighbours;
        private readonly int _id;
        #endregion

        #region Properties
        public T Data => _data;
        public int ID => _id;
        public IList<INeighbour<T>> Neighbours => _neighbours;
        #endregion

        #region Methods
        internal Node(T data, int id)
        {
            _data = data;
            _id = id;
            _neighbours = new List<INeighbour<T>>();
        }
        public void RegisterNeighbour (INeighbour<T> neighbour)
        {
            Neighbours.Remove(neighbour);
            Neighbours.Add(neighbour);
        }
        public void RegisterNeighbour (INode<T> node, int cost)
        {
            RegisterNeighbour(new Neighbour<T>(node, cost));
        }
        public void UnregisterNeighbour(INeighbour<T> neighbour)
        {
            Neighbours.Remove(neighbour);
        }
        public void UnregisterNeighbour(INode<T> node)
        {
            UnregisterNeighbour(new Neighbour<T>(node));
        }
        public void UnregisterAll()
        {
            Neighbours.Clear();
        }
        public bool IsNeighbourOf(INode<T> node)
        {
            return Neighbours.Any(n => n.Node == node);
        }
        public int CostTo(INode<T> node)
        {
            INeighbour<T>? record = Neighbours.FirstOrDefault(n => n.Node == node);

            if (record == null) throw new NeighbourNotRegisteredException(node.ID, ID);

            return record.Cost;
        }
        #endregion

        #region overrides
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
        public override string ToString()
        {
            return ID.ToString();
        }
        #endregion
    }

    public class Neighbour<T> : INeighbour<T>
    {
        #region Fields
        private INode<T> _node;
        private int _cost;
        #endregion

        #region Properties
        public INode<T> Node => _node;
        public int Cost => _cost;
        #endregion

        #region Methods
        public Neighbour(INode<T> node, int cost)
        {
            _node = node;
            _cost = cost;
        }
        internal Neighbour(INode<T> node)
        {
            _node = node;
            _cost = 0;
        }

        public bool Equals(INeighbour<T>? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _node.ID == other.Node.ID;
        }
        public override bool Equals(object? obj) => obj is INeighbour<T> other && Equals(other);
        public override int GetHashCode() => _node.ID.GetHashCode();
        public static bool operator ==(Neighbour<T>? left, Neighbour<T>? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Neighbour<T>? left, Neighbour<T>? right) => !(left == right);

        public override string ToString()
        {
            return $"(Node: {Node}, Cost: {Cost})";
        }
        #endregion
    }

}
