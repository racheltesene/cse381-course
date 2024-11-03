/* CSE 381 - Dijkstra Shortest Path
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the ShortestPath function per the instructions
*  in the comments.  Run all tests in DijkstraShortestPathTest.cs to verify your code.
*/


namespace AlgorithmLib;

public static class DijkstraShortestPath
{
    /* Find the shortest path from a starting vertex to all
     * vertices in a graph using Dijkstra's algorithm. This implementation
     * uses a priority queue to efficiently select the minimum-distance vertex.
     *
     * Inputs:
     *     g - Graph: A weighted graph represented with an adjacency list.
     *     startVertex - Starting vertex ID.
     *
     * Outputs:
     *     (distance list, predecessor list)
     *     - distance: List of shortest distances from startVertex to each vertex.
     *     - predecessor: List indicating the previous vertex for the shortest path.
     *     - Unreachable vertices will have distances set to Graph.INF.
     *     - Predecessors of vertices with no path will be set to Graph.INF.
     */
    public static (List<int>, List<int>) ShortestPath(Graph g, int startVertex)
    {
        int n = g.Size();  // Get the number of vertices in the graph

        // Initialize distances to all vertices as infinity, except for the start vertex
        List<int> distance = Enumerable.Repeat(Graph.INF, n).ToList();
        distance[startVertex] = 0;

        // Initialize predecessors to track the shortest path
        List<int> predecessor = Enumerable.Repeat(Graph.INF, n).ToList();

        // Priority queue to process vertices by their minimum distance (use tuples)
        var priorityQueue = new SortedSet<(int, int)>();
        priorityQueue.Add((0, startVertex));

        while (priorityQueue.Count > 0)
        {
            // Get vertex u with the smallest distance
            var (distU, u) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            // Traverse through neighbors of u
            foreach (Edge edge in g.Edges(u))
            {
                int v = edge.DestId;
                int weightUV = edge.Weight;

                // Calculate potential new path distance to v
                int newDist = distU + weightUV;

                // If found shorter path to v
                if (newDist < distance[v])
                {
                    // Remove old distance if it exists
                    priorityQueue.Remove((distance[v], v));

                    // Update with new shortest distance and predecessor
                    distance[v] = newDist;
                    predecessor[v] = u;

                    // Add updated distance to priority queue
                    priorityQueue.Add((distance[v], v));
                }
            }
        }

        return (distance, predecessor);
    }
}
