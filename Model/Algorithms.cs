using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class Algorithms
    {
        public static bool IsConnected<T>(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Nodes == null) return true;
            if (graph.Nodes.Count <= 1) return true;

            var start = graph.Nodes.First();
            var visited = new HashSet<INode<T>>();
            var q = new Queue<INode<T>>();
            visited.Add(start);
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                foreach (var neigh in u.Neighbours)
                {
                    var v = neigh.Node;
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        q.Enqueue(v);
                    }
                }
            }

            return visited.Count == graph.Nodes.Count;
        }
        public static IPath<T> Dijkstras<T>(IGraph<T> graph, INode<T> source, INode<T> target)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (graph.Nodes == null || graph.Nodes.Count == 0)
                return new Path<T>();

            if (!graph.Nodes.Contains(source)) throw new InvalidOperationException("Source node is not part of the graph.");
            if (!graph.Nodes.Contains(target)) throw new InvalidOperationException("Target node is not part of the graph.");

            if (source.Equals(target))
            {
                var single = new Path<T>();
                single.Append(source);
                return single;
            }

            var dist = new Dictionary<INode<T>, int>();
            var prev = new Dictionary<INode<T>, INode<T>?>();
            var visited = new HashSet<INode<T>>();

            foreach (var node in graph.Nodes)
            {
                dist[node] = int.MaxValue;
                prev[node] = null;
            }

            dist[source] = 0;

            var pq = new PriorityQueue<INode<T>, int>();
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                var u = pq.Dequeue();

                if (visited.Contains(u)) continue;
                visited.Add(u);

                if (u.Equals(target)) break;

                foreach (var neighbour in u.Neighbours)
                {
                    var v = neighbour.Node;
                    int weight = neighbour.Cost;

                    if (weight < 0)
                        throw new InvalidOperationException("Dijkstra's algorithm does not support negative edge weights");

                    if (visited.Contains(v)) continue;

                    long altLong = (long)dist[u] + weight;
                    if (altLong < dist[v])
                    {
                        dist[v] = (int)altLong;
                        prev[v] = u;
                        pq.Enqueue(v, dist[v]);
                    }
                }
            }

            if (dist[target] == int.MaxValue) return new Path<T>();

            var stack = new Stack<INode<T>>();
            INode<T>? cur = target;
            while (cur != null)
            {
                stack.Push(cur);
                cur = prev[cur];
            }

            var path = new Path<T>();
            while (stack.Count > 0)
            {
                path.Append(stack.Pop());
            }

            return path;
        }
    }
}
