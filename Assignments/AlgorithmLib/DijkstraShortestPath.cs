/* CSE 381 - Dijkstra Shortest Path
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the ShortestPath function per the instructions
*  in the comments.  Run all tests in DijkstraShortestPathTest.cs to verify your code.
*/

namespace AlgorithmLib
{
    public static class DijkstraShortestPath
    {
        /* 
         * Dijkstra's Shortest Path Algorithm:
         * The function calculates the shortest path from a given starting vertex 
         * to all other vertices in a weighted graph.
         * 
         * Algorithm Overview:
         * 1. Initialize a distance list where the distance to each vertex is Graph.INF 
         *    (indicating infinity) except for the start vertex, which is set to 0.
         * 2. Initialize a predecessor list to store the previous vertex on the shortest path.
         * 3. Use a priority queue to always expand the vertex with the smallest known distance.
         * 4. For each neighbor of the current vertex, calculate the tentative distance and 
         *    update if a shorter path is found.
         * 5. Continue until all vertices are processed.
         *
         * Inputs:
         *   g - Graph object that contains information on vertices, edges, and weights.
         *   startVertex - The vertex from which to calculate shortest paths.
         * 
         * Outputs:
         *   A tuple containing:
         *   1. A list of minimum distances from the start vertex to each vertex.
         *   2. A list of predecessors for each vertex (used to reconstruct paths).
         */
        public static (List<int>, List<int>) ShortestPath(Graph g, int startVertex)
        {
            // Number of vertices in the graph
            int vertexCount = g.VertexCount;
            
            // Initialize the distance list with 'infinity' (Graph.INF) for all vertices
            List<int> distance = Enumerable.Repeat(Graph.INF, vertexCount).ToList();
            
            // Set the distance to the start vertex to 0 (shortest path to itself is zero)
            distance[startVertex] = 0;

            // Initialize the predecessor list with -1 for all vertices (indicating no predecessor)
            List<int> predecessor = Enumerable.Repeat(-1, vertexCount).ToList();

            // Create a priority queue to process the vertex with the smallest known distance
            PriorityQueue<(int vertex, int dist), int> pq = new PriorityQueue<(int vertex, int dist), int>();

            // Add the start vertex to the queue with a distance of 0
            pq.Enqueue((startVertex, 0), 0);

            // Set to keep track of processed vertices (to avoid reprocessing)
            HashSet<int> processed = new HashSet<int>();

            // Main loop: Process vertices from the queue until empty
            while (pq.Count > 0)
            {
                // Get the vertex with the smallest distance
                var (currentVertex, currentDistance) = pq.Dequeue();

                // If the vertex has already been processed, skip it
                if (processed.Contains(currentVertex))
                {
                    continue;
                }

                // Mark the current vertex as processed
                processed.Add(currentVertex);

                // Iterate over all neighbors of the current vertex
                foreach (var neighbor in g.GetNeighbors(currentVertex))
                {
                    int neighborVertex = neighbor.vertex;  // Neighbor's vertex ID
                    int edgeWeight = neighbor.weight;      // Weight of the edge to the neighbor

                    // Calculate the new tentative distance for the neighbor
                    int newDistance = currentDistance + edgeWeight;

                    // If the new distance is shorter than the previously known distance
                    if (newDistance < distance[neighborVertex])
                    {
                        // Update the distance and predecessor lists
                        distance[neighborVertex] = newDistance;
                        predecessor[neighborVertex] = currentVertex;

                        // Add the neighbor to the priority queue for further exploration
                        pq.Enqueue((neighborVertex, newDistance), newDistance);
                    }
                }
            }

            // Return the distance list and predecessor list
            return (distance, predecessor);
        }
    }
}
