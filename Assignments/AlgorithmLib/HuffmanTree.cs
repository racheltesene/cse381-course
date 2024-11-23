﻿/* CSE 381 - Huffman Tree
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Profile, BuildTree, _CreateEncodingMap,
*  Encode, and Decode function per the instructions in the comments.  
*  Run all tests in HuffmanTreeTest.cs to verify your code.
*/
using System.Text;

namespace AlgorithmLib
{
    public static class HuffmanTree
    {
        // A Node represents a character and its count, along with its left and right children in the Huffman Tree.
        public class Node
        {
            public char Letter { get; set; } = '\0'; // The character associated with this node.
            public int Count { get; set; } // The frequency count of the character in the input string.
            public Node? Left { get; set; } // Left child node.
            public Node? Right { get; set; } // Right child node.
        }

        /// <summary>
        /// Generates a frequency profile for the given text.
        /// </summary>
        /// <param name="text">The input text for which the frequency profile will be generated.</param>
        /// <returns>A dictionary where the key is the character and the value is the frequency of that character in the text.</returns>
        public static Dictionary<char, int> Profile(string text)
        {
            var profile = new Dictionary<char, int>();
            // Loop through each character in the text to count its occurrences
            foreach (char c in text)
            {
                if (profile.ContainsKey(c)) // If the character is already in the profile, increment its count
                    profile[c]++;
                else // If it's not in the profile, add it with an initial count of 1
                    profile[c] = 1;
            }
            return profile; // Return the frequency profile as a dictionary
        }

        /// <summary>
        /// Builds a Huffman Tree based on the character frequency profile.
        /// </summary>
        /// <param name="profile">The frequency profile (character to frequency mapping).</param>
        /// <returns>The root node of the Huffman Tree.</returns>
        public static Node BuildTree(Dictionary<char, int> profile)
        {
            // Check if the profile is null or empty (invalid input)
            if (profile == null || profile.Count == 0)
                throw new ArgumentException("Profile cannot be null or empty");

            var priorityQueue = new PriorityQueue<Node>(); // Create a priority queue for building the Huffman Tree
            
            // Initialize the priority queue with nodes for each character based on the frequency profile
            foreach (var kvp in profile)
            {
                var node = new Node
                {
                    Letter = kvp.Key, // Character
                    Count = kvp.Value // Frequency
                };
                priorityQueue.Enqueue(node, node.Count); // Enqueue node with its frequency as the priority
            }

            // Build the tree by combining the two smallest nodes at each step
            while (priorityQueue.Size() > 1)
            {
                var left = priorityQueue.Dequeue(); // Dequeue the node with the smallest frequency
                var right = priorityQueue.Dequeue(); // Dequeue the second smallest node

                // Create a new parent node with the sum of their counts as its frequency
                var parent = new Node
                {
                    Count = left.Count + right.Count,
                    Left = left,
                    Right = right
                };

                priorityQueue.Enqueue(parent, parent.Count); // Enqueue the new parent node
            }

            return priorityQueue.Dequeue(); // The remaining node in the queue is the root of the Huffman Tree
        }

        /// <summary>
        /// Creates a map of characters to their binary Huffman encoding.
        /// </summary>
        /// <param name="root">The root node of the Huffman Tree.</param>
        /// <returns>A dictionary where the key is the character and the value is its Huffman binary encoding.</returns>
        public static Dictionary<char, string> CreateEncodingMap(Node root)
        {
            var map = new Dictionary<char, string>();
            _CreateEncodingMap(root, "", map); // Start the recursive encoding creation with an empty code string
            return map;
        }

        /// <summary>
        /// Helper method for recursively creating the Huffman encoding map.
        /// </summary>
        /// <param name="node">The current node being processed.</param>
        /// <param name="code">The current Huffman binary code being built.</param>
        /// <param name="map">The dictionary to store the encoding map.</param>
        private static void _CreateEncodingMap(Node node, string code, Dictionary<char, string> map)
        {
            if (node.Left == null && node.Right == null) // Leaf node, meaning it contains a character
            {
                map[node.Letter] = code; // Assign the current code to the character in the map
                return;
            }

            // Recur for the left child with '0' appended to the current code
            if (node.Left != null)
                _CreateEncodingMap(node.Left, code + "0", map);

            // Recur for the right child with '1' appended to the current code
            if (node.Right != null)
                _CreateEncodingMap(node.Right, code + "1", map);
        }

        /// <summary>
        /// Encodes the given text using the provided Huffman encoding map.
        /// </summary>
        /// <param name="text">The input text to be encoded.</param>
        /// <param name="map">The Huffman encoding map that maps characters to their binary encoding.</param>
        /// <returns>The encoded binary string.</returns>
        public static string Encode(string text, Dictionary<char, string> map)
        {
            // Use LINQ to convert each character in the text to its corresponding binary code and concatenate them
            return string.Concat(text.Select(c => map[c]));
        }

        /// <summary>
        /// Decodes the binary string back into text using the Huffman Tree.
        /// </summary>
        /// <param name="text">The binary string to be decoded.</param>
        /// <param name="root">The root node of the Huffman Tree.</param>
        /// <returns>The decoded text.</returns>
        public static string Decode(string text, Node root)
        {
            // If the input text is empty, return an empty string
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var decoded = new StringBuilder(); // Use StringBuilder for efficient string concatenation
            var current = root; // Start from the root of the tree

            // Loop through each bit in the binary string
            foreach (char bit in text)
            {
                if (bit != '0' && bit != '1') // Check for invalid characters in the binary string
                    throw new ArgumentException("Invalid binary string");

                // Traverse left if the bit is '0', or right if it's '1'
                current = bit == '0' ? current.Left : current.Right;

                // If we reach a leaf node, add the character to the decoded string
                if (current == null)
                    throw new ArgumentException("Invalid Huffman encoding"); // In case we encounter an invalid encoding

                if (current.Left == null && current.Right == null) // Leaf node, which holds a character
                {
                    decoded.Append(current.Letter); // Append the character to the result
                    current = root; // Reset to the root node for the next character
                }
            }

            return decoded.ToString(); // Return the final decoded string
        }
    }
}

