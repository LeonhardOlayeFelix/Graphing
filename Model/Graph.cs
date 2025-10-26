
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphing.Model.Interfaces;

namespace Graphing.Model
{
    
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

        public INode<T> RemoveNode(INode<T> node)
        {
            INode<T>? nodeToRemove = Nodes.FirstOrDefault((n) => n == node);

            if (nodeToRemove == null) throw new NodeDoesNotExistException(node.ID);

            foreach (INeighbour<T>? record in nodeToRemove.Neighbours)
            {
                record.Node.UnregisterNeighbour(node);
            }

            node.UnregisterAll();

            Nodes.Remove(node);

            return node;
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
        public IEdge<T> RemoveEdge(INode<T> node1, INode<T> node2, int cost = 0)
        {
            if (!Nodes.Contains(node1)) throw new InvalidGraphOperationException($"Cannot remove edge between Node: {node1} and Node: {node2} because Node: {node1} is not in the graph");
            if (!Nodes.Contains(node2)) throw new InvalidGraphOperationException($"Cannot remove edge between Node: {node1} and Node: {node2} because Node: {node2} is not in the graph");
            if (!EdgeExists(node1, node2)) throw new InvalidGraphOperationException($"Cannot remove edge between Node: {node1} and Node: {node2} because there is no edge");

            node1.UnregisterNeighbour(node2);
            node2.UnregisterNeighbour(node1);

            return new Edge<T>(node1, node2, cost);
        }
        public IEdge<T> RemoveEdge(IEdge<T> edge)
        {
            RemoveEdge(edge.Node1, edge.Node2, edge.Cost);
            return edge;
        }
        private bool EdgeExists(INode<T> node1, INode<T> node2)
        {
            return node1.IsNeighbourOf(node2) && node2.IsNeighbourOf(node1);
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
