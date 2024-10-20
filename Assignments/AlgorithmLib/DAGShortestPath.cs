/* CSE 381 - DAG Shortest Path
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Sort and Search functions per the instructions
*  in the comments.  Run all tests in DAGShortestPathTest.cs to verify your code.
*/

namespace AlgorithmLib;

public static class DAGShortestPath
{
    // Helper function for topological sorting using Depth First Search (DFS)
    private static void TopoSortUtil(Graph g, int v, bool[] visited, Stack<int> stack)
    {
        // Mark the current node as visited
        visited[v] = true;

        // Recur for all the vertices adjacent to this vertex
        foreach (var edge in g.GetEdges(v))  // Use the actual method that retrieves edges for vertex `v`
        {
            int neighbor = edge.Item1;  // Assuming GetEdges returns a list of tuples (neighbor, weight)
            if (!visited[neighbor])
            {
                TopoSortUtil(g, neighbor, visited, stack);
            }
        }

        // Push current vertex to stack which stores the result
        stack.Push(v);
    }

    /* Topologically sort all vertices in a graph and return the sorted
     * list of vertex IDs.  Use a Stack object (available in C#).
     *
     *  Inputs:
     *     g - Graph
     *  Outputs:
     *     Return a sorted list of vertex IDs
     */
    public static List<int> Sort(Graph g)
    {
        int numVertices = g.VertexCount();  // Use the actual method for vertex count
        Stack<int> stack = new Stack<int>();

        // Mark all the vertices as not visited
        bool[] visited = new bool[numVertices];

        // Call the helper function to store Topological Sort
        for (int i = 0; i < numVertices; i++)
        {
            if (!visited[i])
            {
                TopoSortUtil(g, i, visited, stack);
            }
        }

        // Return the sorted list by popping all elements from the stack
        List<int> sortedList = new List<int>();
        while (stack.Count > 0)
        {
            sortedList.Add(stack.Pop());
        }

        return sortedList;
    }

    /* Find the shortest path from a starting vertex to all
     * vertices in a DAG.  This function will need to
     * call the Sort function to obtain the topologically
     * sorted list of vertices from the graph.
     *
     *  Inputs:
     *     g - Directed Acyclic Graph
     *     startVertex - Starting vertex ID
     *  Outputs:
     *     (distance list, predecessor list)
     *     NOTE: The above two output lists should contain Graph.INF as needed
     */
    public static (List<int>, List<int>) ShortestPath(Graph g, int startVertex)
    {
        int numVertices = g.VertexCount();  // Use the actual method for vertex count
        List<int> dist = Enumerable.Repeat(Graph.INF, numVertices).ToList();
        List<int> pred = Enumerable.Repeat(Graph.INF, numVertices).ToList();

        // Distance to the start vertex is 0
        dist[startVertex] = 0;

        // Get the topologically sorted vertices
        List<int> topoSorted = Sort(g);

        // Process vertices in topological order
        foreach (int u in topoSorted)
        {
            // If the distance to this vertex is not INF, process its neighbors
            if (dist[u] != Graph.INF)
            {
                foreach (var edge in g.GetEdges(u))  // Use the actual method that retrieves edges for vertex `u`
                {
                    int v = edge.Item1;     // Assuming GetEdges returns a tuple (neighbor, weight)
                    int weight = edge.Item2;

                    // Relax the edge
                    if (dist[u] + weight < dist[v])
                    {
                        dist[v] = dist[u] + weight;
                        pred[v] = u;
                    }
                }
            }
        }

        return (dist, pred);
    }
}
