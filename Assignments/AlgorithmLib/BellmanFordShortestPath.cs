/* CSE 381 - Bellman Ford
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the ShortestPath function per the instructions
*  in the comments.  Run all tests in BellmanFordTest.cs to verify your code.
*/


/*I was able to pass the second test but 
not the first one. 
*/
namespace AlgorithmLib
{
    public static class BellmanFordShortestPath
    {
        public static (List<int>, List<int>) ShortestPath(Graph g, int startVertex)
        {
            int vertices = g.Size();
            List<int> distance = new List<int>(new int[vertices]);   // List to store shortest distances from startVertex
            List<int> predecessor = new List<int>(new int[vertices]); // List to store the previous node in the path

            // Initialize all distances as "infinity" and predecessors as undefined (INF)
            for (int i = 0; i < vertices; i++)
            {
                distance[i] = Graph.INF;
                predecessor[i] = Graph.INF;
            }
            distance[startVertex] = 0; // Distance to the start vertex is zero

            // Relax each edge up to |V| - 1 times (where |V| is the number of vertices)
            for (int i = 0; i < vertices - 1; i++)
            {
                for (int u = 0; u < vertices; u++)
                {
                    // Iterate over edges from vertex u
                    foreach (var edge in g.Edges(u))
                    {
                        int v = edge.DestId;    // Destination vertex of edge
                        int weight = edge.Weight; // Weight of edge

                        // Relax the edge if a shorter path is found to vertex v
                        if (distance[u] != Graph.INF && distance[u] + weight < distance[v])
                        {
                            distance[v] = distance[u] + weight;
                            predecessor[v] = u;
                        }
                    }
                }
            }

            // Check for negative cycles by attempting one more relaxation round
            for (int u = 0; u < vertices; u++)
            {
                foreach (var edge in g.Edges(u))
                {
                    int v = edge.DestId;
                    int weight = edge.Weight;

                    // If we can relax an edge here, a negative cycle exists
                    if (distance[u] != Graph.INF && distance[u] + weight < distance[v])
                    {
                        return (new List<int>(), new List<int>()); // Return empty lists if a negative cycle is detected
                    }
                }
            }

            // Return the lists of shortest distances and predecessors if no negative cycle is found
            return (distance, predecessor);
        }
    }
}

