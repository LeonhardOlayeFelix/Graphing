
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphing.Model.Interfaces;

namespace Graphing.Model
{
    public class GraphUtils
    {
        public static int Valency<T>(INode<T> node)
        {
            return node.Neighbours.Count;
        }
        public static INode<T>? GetNode<T>(IGraph<T> graph, int id)
        {
            return graph.Nodes.FirstOrDefault(n => n.ID == id);
        }
        public static int TotalValency<T>(IGraph<T> graph)
        {
            int total = 0;
            foreach (INode<T> node in graph.Nodes)
            {
                total += Valency(node);
            }
            return total;
        }
        public static IEnumerable<INode<T>> GetOddNodes<T>(IGraph<T> graph)
        {
            return graph.Nodes.Where((n) => Valency(n) % 2 == 1);
        }
        public static bool IsSemiEulerian<T>(IGraph<T> graph)
        {
            return GetOddNodes(graph).Count() == 2;
        }
        public static bool IsEulerian<T>(IGraph<T> graph)
        {
            return GetOddNodes(graph).Count() == 0;
        }
    }
}
