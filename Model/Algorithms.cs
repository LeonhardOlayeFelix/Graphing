using Graphing.Model.Interfaces;
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
        public static IMinimumSpanningTree<T> Kruskals<T>(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var mst = new MinimumSpanningTree<T>();

            if (graph.Nodes == null || graph.Nodes.Count == 0) return mst;

            var ds = new DisjointSet();
            foreach (var node in graph.Nodes)
                ds.MakeSet(node.ID);

            var sorted = graph.Edges.OrderBy(e => e.Cost).ToList();

            foreach (var edge in sorted)
            {
                int a = edge.Node1.ID;
                int b = edge.Node2.ID;

                if (ds.Find(a) != ds.Find(b))
                {
                    mst.Add(edge);
                    ds.Union(a, b);
                    if (mst.Edges.Count == graph.Nodes.Count - 1) break;
                }
            }

            return mst;
        }
        public static IMinimumSpanningTree<T> Prims<T>(IGraph<T> graph, INode<T>? start = null)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var mst = new MinimumSpanningTree<T>();

            if (graph.Nodes == null || graph.Nodes.Count == 0) return mst;

            if (start == null) start = graph.Nodes.First();
            if (!graph.Nodes.Contains(start)) throw new InvalidOperationException("Start node is not part of the graph.");

            var visited = new HashSet<INode<T>>();
            var pq = new PriorityQueue<IEdge<T>, int>();

            visited.Add(start);

            foreach (var neigh in start.Neighbours)
            {
                pq.Enqueue(new Edge<T>(start, neigh.Node, neigh.Cost), neigh.Cost);
            }

            while (pq.Count > 0 && mst.Edges.Count < graph.Nodes.Count - 1)
            {
                var edge = pq.Dequeue();
                var u = edge.Node1;
                var v = edge.Node2;

                if (visited.Contains(u) && visited.Contains(v)) continue;

                INode<T> newNode = visited.Contains(u) ? v : u;

                mst.Add(edge);
                visited.Add(newNode);

                foreach (var neigh in newNode.Neighbours)
                {
                    if (!visited.Contains(neigh.Node))
                    {
                        pq.Enqueue(new Edge<T>(newNode, neigh.Node, neigh.Cost), neigh.Cost);
                    }
                }
            }

            return mst;
        }
        private class DisjointSet
        {
            private readonly Dictionary<int, int> _parent = new();
            private readonly Dictionary<int, int> _rank = new();

            public void MakeSet(int x)
            {
                _parent[x] = x;
                _rank[x] = 0;
            }

            public int Find(int x)
            {
                if (!_parent.ContainsKey(x)) throw new InvalidOperationException($"Element {x} not found in disjoint set.");
                if (_parent[x] != x) _parent[x] = Find(_parent[x]);
                return _parent[x];
            }

            public void Union(int x, int y)
            {
                int rx = Find(x);
                int ry = Find(y);
                if (rx == ry) return;
                if (_rank[rx] < _rank[ry])
                {
                    _parent[rx] = ry;
                }
                else if (_rank[ry] < _rank[rx])
                {
                    _parent[ry] = rx;
                }
                else
                {
                    _parent[ry] = rx;
                    _rank[rx]++;
                }
            }
        }
    }
}
