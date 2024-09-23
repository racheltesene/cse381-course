/* CSE 381 - BetterLinearSerach
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Search function per the instructions
*  in the comments.  Run all tests in BetterLinearSearchTest.cs to verify your code.
*/

namespace AlgorithmLib
{
    public static class BetterLinearSearch
    {
        /* Search for an item in a list. Ignore duplicates by exiting
        *  as soon as the first match is found.
        *
        *  Inputs:
        *     data - list to search
        *     target - value to search for
        *  Outputs:
        *     Index where target was found
        *
        *  Note: Return -1 if target not found
        */
        public static int Search<T>(List<T> data, T target) where T : IComparable<T>
        {
            // Loop through each element in the list
            for (int i = 0; i < data.Count; i++)
            {
                // Check if the current element matches the target
                if (data[i].Equals(target))
                {
                    // Return the index as soon as a match is found
                    return i;
                }
            }
            // If no match is found, return -1
            return -1;
        }
    }
}
