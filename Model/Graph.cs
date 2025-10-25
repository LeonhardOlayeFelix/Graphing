
using Graphing.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public interface IGraph<T>
    {
        public IList<INode<T>> Nodes { get; }
        public IList<IEdge<T>> Edges { get; }
        public INode<T> AddNode(T data);
        public void RemoveNode(INode<T> node);
        public IEdge<T> AddEdge(INode<T> node1, INode<T> node2, int cost);

    }
    /// <summary>
    /// A class representing a graph data structure
    /// </summary>
    /// <typeparam name="T">The type of the data that the graph stores</typeparam>
    public class Graph<T> : IGraph<T>
    {
        private IList<INode<T>> _nodes;
        private IList<IEdge<T>> _edges;
        private int _nextId;
        public IList<INode<T>> Nodes => _nodes;
        public IList<IEdge<T>> Edges
        {
            get
            {
                IList<IEdge<T>> ret = new List<IEdge<T>>();
                foreach (var node in Nodes)
                {
                    foreach (INeighbour<T> record in node.Neighbours)
                    {
                        if (node.ID < record.Node.ID) ret.Add(new Edge<T>(node, record.Node, record.Cost));
                    }
                }
                return ret;
            }
        }
        public Graph()
        {
            _nodes = new List<INode<T>>();
            _edges = new List<IEdge<T>>();

        }
        public INode<T> AddNode(T data)
        {
            INode<T> toAdd = new Node<T>(data, _nextId++);
            Nodes.Add(toAdd);

            return toAdd;
        }

        public void RemoveNode(INode<T> node)
        {
            INode<T>? nodeToRemove = Nodes.FirstOrDefault((n) => n == node);

            if (nodeToRemove == null) throw new NodeDoesNotExistException(node.ID);

            foreach (INeighbour<T>? record in nodeToRemove.Neighbours)
            {
                record.Node.UnregisterNeighbour(node);
            }

            Nodes.Remove(node);
        }
        public IEdge<T> AddEdge(INode<T> node1, INode<T> node2, int cost)
        {
            if (node1 == node2) throw new InvalidGraphOperationException($"Cannot add edge between Node: {node1} and Node: {node2} because they represent the same node");
            if (!Nodes.Contains(node1)) throw new InvalidGraphOperationException($"Cannot add edge between Node: {node1} and Node: {node2} because Node: {node1} is not in the graph");
            if (!Nodes.Contains(node2)) throw new InvalidGraphOperationException($"Cannot add edge between Node: {node1} and Node: {node2} because Node: {node2} is not in the graph");

            node1.RegisterNeighbour(node2, cost);
            node2.RegisterNeighbour(node1, cost);

            return new Edge<T>(node1, node2, cost);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var node in Nodes)
            {
                ret += $"Node: {node.ID}\n";
                foreach (INeighbour<T> record in node.Neighbours)
                {
                    ret += $"\t Connected to {record.Node} with cost {record.Cost}\n";
                }
            }
            return ret;
        }
    }
}
