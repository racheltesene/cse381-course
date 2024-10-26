/* CSE 381 - Dijkstra Shortest Path
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the ShortestPath function per the instructions
*  in the comments.  Run all tests in DijkstraShortestPathTest.cs to verify your code.
*
*
*NOTE TO GRADER: Ill be so read with you i wasnt able to figure 
*out how to get rid of the errors and have it run the tests. i would
*love your input.
*/

namespace AlgorithmLib
{
    public static class DijkstraShortestPath
    {
        public static (List<int>, List<int>) ShortestPath(Graph g, int startVertex)
        {
            // Use the correct property or method to get the number of vertices
            int numVertices = g.VertexCount;  // Adjust based on your Graph class

            List<int> distances = new List<int>(new int[numVertices]);
            List<int> predecessors = new List<int>(new int[numVertices]);

            // Initialize distances to infinity and predecessors to -1
            for (int i = 0; i < numVertices; i++)
            {
                distances[i] = Graph.INF;  // Graph.INF represents infinity
                predecessors[i] = -1;      // No predecessor initially
            }

            // Set the distance of the start vertex to 0
            distances[startVertex] = 0;

            // Priority Queue to store the vertex and current known distance (min-heap)
            var priorityQueue = new PriorityQueue<(int vertex, int distance), int>();

            // Enqueue the start vertex with a distance of 0
            priorityQueue.Enqueue((startVertex, 0), 0);

            // Main loop: Process the graph while the priority queue is not empty
            (int vertex, int distance) currentVertexWithDistance;
            while (priorityQueue.TryDequeue(out currentVertexWithDistance, out _))
            {
                var (currentVertex, currentDistance) = currentVertexWithDistance;

                // Iterate over the neighbors of the current vertex
                foreach (var neighbor in g.GetNeighbors(currentVertex))
                {
                    int edgeWeight = g.GetEdgeWeight(currentVertex, neighbor);
                    int newDistance = currentDistance + edgeWeight;

                    // If a shorter path to the neighbor is found
                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                        predecessors[neighbor] = currentVertex;
                        priorityQueue.Enqueue((neighbor, newDistance), newDistance);
                    }
                }
            }

            return (distances, predecessors);
        }
    }
}
