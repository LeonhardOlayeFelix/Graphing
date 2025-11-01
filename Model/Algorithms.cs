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
        public static bool IsConnected<T>(Graph<T> graph)
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
        public static bool HasCycle<T>(Graph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Nodes == null || graph.Nodes.Count <= 1) return false;

            var visited = new HashSet<INode<T>>();

            bool Dfs(INode<T> node, INode<T>? parent)
            {
                visited.Add(node);

                foreach (var neigh in node.Neighbours)
                {
                    var next = neigh.Node;
                    if (!visited.Contains(next))
                    {
                        if (Dfs(next, node)) return true;
                    }
                    else if (!next.Equals(parent))
                    {
                        return true;
                    }
                }
                return false;
            }

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    if (Dfs(node, null)) return true;
                }
            }

            return false;
        }
        public static IPath<T> Dijkstras<T>(Graph<T> graph, INode<T> source, INode<T> target)
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
        public static Graph<T> Kruskals<T>(Graph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var mst = new MinimumSpanningTree<T>();

            if (graph.Nodes == null || graph.Nodes.Count == 0) return new Graph<T>();

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

            // build a new Graph<T> that contains only the MST edges (and the same node data)
            var result = new Graph<T>();
            var mapping = new Dictionary<int, INode<T>>();
            foreach (var node in graph.Nodes)
            {
                var newNode = result.AddNode(node.Data);
                mapping[node.ID] = newNode;
            }

            foreach (var edge in mst.Edges)
            {
                result.AddEdge(mapping[edge.Node1.ID], mapping[edge.Node2.ID], edge.Cost);
            }

            return result;
        }
        public static Graph<T> Prims<T>(Graph<T> graph, INode<T>? start = null)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var mst = new MinimumSpanningTree<T>();

            if (graph.Nodes == null || graph.Nodes.Count == 0) return new Graph<T>();

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

            // convert MST edge collection into a Graph<T>
            var result = new Graph<T>();
            var mapping = new Dictionary<int, INode<T>>();
            foreach (var node in graph.Nodes)
            {
                var newNode = result.AddNode(node.Data);
                mapping[node.ID] = newNode;
            }

            foreach (var edge in mst.Edges)
            {
                result.AddEdge(mapping[edge.Node1.ID], mapping[edge.Node2.ID], edge.Cost);
            }

            return result;
        }
        public static IEdgeCollection<T> RouteInspection<T>(Graph<T> graph, INode<T>? start = null, INode<T>? end = null)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Nodes == null || graph.Nodes.Count == 0) return new EdgeCollection<T>();
            if ((start == null) != (end == null)) throw new ArgumentException("Either both start and end must be supplied or neither.");
            if (start != null && !graph.Nodes.Contains(start)) throw new InvalidOperationException("Start node is not part of the graph.");
            if (end != null && !graph.Nodes.Contains(end)) throw new InvalidOperationException("End node is not part of the graph.");

            var oddNodes = GraphUtils.GetOddNodes(graph).ToList();

            if (start != null && end != null)
            {
                if (!oddNodes.Contains(start) || !oddNodes.Contains(end)) throw new InvalidOperationException("When specifying start and end for a trail, both must be odd-degree vertices.");

                oddNodes.Remove(start);
                oddNodes.Remove(end);
            }

            if (!oddNodes.Any()) return new EdgeCollection<T>();

            if (oddNodes.Count % 2 != 0) throw new InvalidOperationException("Odd number of odd-degree vertices after accounting for start/end.");

            var pairings = Partition(oddNodes);

            int bestCost = int.MaxValue;
            List<List<INode<T>>>? bestPairing = null;

            foreach (var pairing in pairings)
            {
                int total = 0;
                bool invalid = false;
                foreach (var pair in pairing)
                {
                    var a = pair[0];
                    var b = pair[1];
                    var shortest = Dijkstras(graph, a, b);
                    if (shortest == null || shortest.Nodes.Count == 0)
                    {
                        invalid = true;
                        break;
                    }
                    total += shortest.Cost;
                }

                if (!invalid && total < bestCost)
                {
                    bestCost = total;
                    bestPairing = pairing;
                }
            }

            if (bestPairing == null) return new EdgeCollection<T>();

            var repeatedEdges = new EdgeCollection<T>();

            foreach (var pair in bestPairing)
            {
                var shortest = Dijkstras(graph, pair[0], pair[1]);
                var nodes = shortest.Nodes;
                for (int i = 0; i < nodes.Count - 1; i++)
                {
                    var u = nodes[i];
                    var v = nodes[i + 1];
                    var neighbour = u.Neighbours.FirstOrDefault(n => n.Node.Equals(v));
                    if (neighbour == null) throw new InvalidOperationException($"No edge between {u.ID} and {v.ID} while reconstructing repeated edges.");
                    repeatedEdges.Add(new Edge<T>(u, v, neighbour.Cost));
                }
            }
    
            return repeatedEdges;
        }
        public static IWalk<T> Bfs<T>(Graph<T> graph, INode<T> start)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (start == null) throw new ArgumentNullException(nameof(start));
            if (graph.Nodes == null || graph.Nodes.Count == 0) return new Walk<T>();
            if (!graph.Nodes.Contains(start)) throw new InvalidOperationException("Start node is not part of the graph.");

            var walk = new Walk<T>();
            var visited = new HashSet<INode<T>>();
            var q = new Queue<INode<T>>();

            visited.Add(start);
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                walk.Append(u);

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

            return walk;
        }
        public static IWalk<T> Dfs<T>(Graph<T> graph, INode<T> start)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (start == null) throw new ArgumentNullException(nameof(start));
            if (graph.Nodes == null || graph.Nodes.Count == 0) return new Walk<T>();
            if (!graph.Nodes.Contains(start)) throw new InvalidOperationException("Start node is not part of the graph.");

            var walk = new Walk<T>();
            var visited = new HashSet<INode<T>>();
            var stack = new Stack<INode<T>>();

            visited.Add(start);
            stack.Push(start);

            while (stack.Count > 0)
            {
                var u = stack.Pop();
                walk.Append(u);

                for (int i = u.Neighbours.Count - 1; i >= 0; i--)
                {
                    var v = u.Neighbours[i].Node;
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        stack.Push(v);
                    }
                }
            }

            return walk;
        }



                        





        private static List<List<List<T>>> Partition<T>(List<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list.Count % 2 == 1) throw new ArgumentException("List length must be even for partitioning into pairs.", nameof(list));
            if (list.Count == 0) return new List<List<List<T>>>();
            if (list.Count == 2) return new List<List<List<T>>>() { new List<List<T>>() { new List<T>() { list[0], list[1] } } };

            var ret = new List<List<List<T>>>();

            for (int i = 1; i < list.Count; i++)
            {
                var pair = new List<T>() { list[0], list[i] };

                var remainder = new List<T>();
                for (int j = 1; j < list.Count; j++)
                {
                    if (j == i) continue;
                    remainder.Add(list[j]);
                }

                var subPartitions = Partition(remainder);
                foreach (var sub in subPartitions)
                {
                    var combined = new List<List<T>>();
                    combined.Add(pair);
                    combined.AddRange(sub);
                    ret.Add(combined);
                }
            }

            return ret;
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
