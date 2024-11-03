/* CSE 381 - DAG Shortest Path
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Sort and Search functions per the instructions
*  in the comments.  Run all tests in DAGShortestPathTest.cs to verify your code.
*/

using System.Collections.Generic;
using System.Linq;
using System;

namespace AlgorithmLib;

public static class DAGShortestPath
{
    /* Topologically sort all vertices in a graph and return the sorted
     * list of vertex ID's.  Use a Stack object (available in C#).
     *
     *  Inputs:
     *     g - Graph
     *  Outputs:
     *     Return a sorted list of vertex ID's
     */
    public static List<int> Sort(Graph g)
    {
        int size = g.Size(); // Get the number of vertices in the graph
        bool[] visited = new bool[size]; // Track visited vertices
        Stack<int> stack = new Stack<int>(); // Stack to hold the topological order

        // Visit each vertex
        for (int i = 0; i < size; i++)
        {
            if (!visited[i])
            {
                TopologicalSortUtil(g, i, visited, stack); // Call recursive helper
            }
        }

        // Return the vertices in topological order
        return stack.ToList(); // Convert stack to a list for the result
    }

    // A utility function for performing the DFS and filling the stack
    private static void TopologicalSortUtil(Graph g, int vertex, bool[] visited, Stack<int> stack)
    {
        visited[vertex] = true; // Mark the current node as visited

        // Recur for all vertices connected to this vertex
        foreach (var edge in g.Edges(vertex))
        {
            if (!visited[edge.DestId]) // If not visited, recurse on it
            {
                TopologicalSortUtil(g, edge.DestId, visited, stack);
            }
        }

        // Push current vertex to stack which stores the result
        stack.Push(vertex);
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
        int size = g.Size(); // Get the number of vertices in the graph
        List<int> distances = Enumerable.Repeat(Graph.INF, size).ToList(); // Initialize distances to infinity
        List<int> predecessors = Enumerable.Repeat(Graph.INF, size).ToList(); // Initialize predecessors to infinity
        distances[startVertex] = 0; // Distance from start to itself is 0

        // Get the topologically sorted vertices
        List<int> sortedVertices = Sort(g);

        // Process each vertex in topological order
        foreach (var vertex in sortedVertices)
        {
            // If the current vertex is not reachable, skip it
            if (distances[vertex] != Graph.INF)
            {
                // Explore each adjacent vertex
                foreach (var edge in g.Edges(vertex))
                {
                    // Check if the current path is shorter
                    if (distances[vertex] + edge.Weight < distances[edge.DestId])
                    {
                        distances[edge.DestId] = distances[vertex] + edge.Weight; // Update distance
                        predecessors[edge.DestId] = vertex; // Update predecessor
                    }
                }
            }
        }

        // Return the distances and predecessors
        return (distances, predecessors);
    }
}
