/* CSE 381 - Binary Search
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the _Search function per the instructions
*  in the comments.  Run all tests in BinarySearchTest.cs to verify your code.
*/

namespace AlgorithmLib;

public static class BinarySearch
{
    /* Use Binary Search to search for an item in a list.
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
        // Start the recursion by calling _Search with the full range of the list
        return _Search(data, target, 0, data.Count - 1);
    }

    /* Use Binary Search to recursively search for an item in a sublist.
    *
    *  Inputs:
    *     data - list to search
    *     target - value to search for
    *     first - starting index of sublist of data
    *     last - ending index of sublist of data
    *  Outputs:
    *     Index where target was found
    *
    *  Note: Return -1 if target not found
    */
    public static int _Search<T>(List<T> data, T target, int first, int last) where T : IComparable<T>
    {
        // Step 1: Base Case - If the sublist is empty, the target is not found
        // This happens when the first index becomes greater than the last index
        if (first > last)
        {
            return -1;  // Target not found, return -1
        }

        // Step 2: Calculate the middle index of the current sublist
        int mid = first + (last - first) / 2;

        // Step 3: Compare the target value to the middle element of the sublist
        int comparisonResult = target.CompareTo(data[mid]);

        // Step 4: If the target is equal to the middle element, return the middle index
        if (comparisonResult == 0)
        {
            return mid;  // Target found, return the index
        }
        // Step 5: If the target is less than the middle element, search the left half
        else if (comparisonResult < 0)
        {
            return _Search(data, target, first, mid - 1);  // Search the left sublist
        }
        // Step 6: If the target is greater than the middle element, search the right half
        else
        {
            return _Search(data, target, mid + 1, last);  // Search the right sublist
        }
    }
}
