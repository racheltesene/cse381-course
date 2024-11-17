/* CSE 381 - Huffman Tree
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Profile, BuildTree, _CreateEncodingMap,
*  Encode, and Decode function per the instructions in the comments.  
*  Run all tests in HuffmanTreeTest.cs to verify your code.
*/

namespace AlgorithmLib;

public static class HuffmanTree
{
    /* Represent the nodes in the Huffman Tree */
    public class Node
    {
        public char Letter { get; set; }  // Letter represented by the node. Can be blank.
        public int Count { get; set; }   // Frequency of letters in the sub-tree.
        public Node? Left;               // Left sub-tree (can be null).
        public Node? Right;              // Right sub-tree (can be null).
    }

    /// <summary>
    /// Create a frequency profile of all letters in the input text.
    /// </summary>
    /// <param name="text">Source text.</param>
    /// <returns>Dictionary with character frequencies.</returns>
    public static Dictionary<char, int> Profile(string text)
    {
        var profile = new Dictionary<char, int>();
        foreach (var ch in text)
        {
            if (!profile.ContainsKey(ch))
                profile[ch] = 0;
            profile[ch]++;
        }
        return profile;
    }

    /// <summary>
    /// Build a Huffman Tree using a priority queue based on the profile.
    /// </summary>
    /// <param name="profile">Character frequency profile.</param>
    /// <returns>Root node of the Huffman Tree.</returns>
    public static Node BuildTree(Dictionary<char, int> profile)
    {
        var priorityQueue = new PriorityQueue<Node, int>();
        
        // Initialize the priority queue with leaf nodes.
        foreach (var kvp in profile)
        {
            priorityQueue.Enqueue(new Node { Letter = kvp.Key, Count = kvp.Value }, kvp.Value);
        }

        // Construct the Huffman Tree.
        while (priorityQueue.Count > 1)
        {
            var left = priorityQueue.Dequeue();
            var right = priorityQueue.Dequeue();

            var parent = new Node
            {
                Letter = '\0', // Internal nodes do not represent a specific letter.
                Count = left.Count + right.Count,
                Left = left,
                Right = right
            };

            priorityQueue.Enqueue(parent, parent.Count);
        }

        return priorityQueue.Dequeue(); // The last remaining node is the root.
    }

    /// <summary>
    /// Create an encoding map from the Huffman Tree.
    /// </summary>
    /// <param name="root">Root of the Huffman Tree.</param>
    /// <returns>Dictionary with character-to-code mappings.</returns>
    public static Dictionary<char, string> CreateEncodingMap(Node root)
    {
        var encodingMap = new Dictionary<char, string>();
        _CreateEncodingMap(root, "", encodingMap);
        return encodingMap;
    }

    /// <summary>
    /// Recursive helper to populate the encoding map.
    /// </summary>
    /// <param name="node">Current node in the tree.</param>
    /// <param name="code">Current Huffman code.</param>
    /// <param name="map">Encoding map being populated.</param>
    public static void _CreateEncodingMap(Node node, string code, Dictionary<char, string> map)
    {
        if (node.Left == null && node.Right == null)
        {
            // Leaf node: map the letter to its Huffman code.
            map[node.Letter] = code;
            return;
        }

        // Traverse left and append '0' to the code.
        if (node.Left != null)
            _CreateEncodingMap(node.Left, code + "0", map);

        // Traverse right and append '1' to the code.
        if (node.Right != null)
            _CreateEncodingMap(node.Right, code + "1", map);
    }

    /// <summary>
    /// Encode a string using the encoding map.
    /// </summary>
    /// <param name="text">Text to encode.</param>
    /// <param name="map">Encoding map.</param>
    /// <returns>Encoded string of 0s and 1s.</returns>
    public static string Encode(string text, Dictionary<char, string> map)
    {
        var encodedText = new StringBuilder();
        foreach (var ch in text)
        {
            encodedText.Append(map[ch]);
        }
        return encodedText.ToString();
    }

    /// <summary>
    /// Decode an encoded string using the Huffman Tree.
    /// </summary>
    /// <param name="text">Encoded string.</param>
    /// <param name="tree">Root node of the Huffman Tree.</param>
    /// <returns>Decoded text.</returns>
    public static string Decode(string text, Node tree)
    {
        var decodedText = new StringBuilder();
        var currentNode = tree;

        foreach (var bit in text)
        {
            // Traverse left for '0', right for '1'.
            currentNode = bit == '0' ? currentNode.Left : currentNode.Right;

            // If we reach a leaf node, append the letter and reset to the root.
            if (currentNode.Left == null && currentNode.Right == null)
            {
                decodedText.Append(currentNode.Letter);
                currentNode = tree;
            }
        }

        return decodedText.ToString();
    }
}
