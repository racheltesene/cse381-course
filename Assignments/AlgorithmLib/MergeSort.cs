/* CSE 381 - Merge Sort
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Merge and _Sort functions per the instructions
*  in the comments.  Run all tests in MergeSortTest.cs to verify your code.
*/

namespace AlgorithmLib;

public static class MergeSort
{
    /* Use Merge Sort to sort a list of values in place
     *
     *  Inputs:
     *     data - list of values to be sorted
     *  Outputs:
     *     none (in-place sorting)
     */
    public static void Sort<T>(List<T> data) where T : IComparable<T>
    {
        // Start the recursive process with the entire list (from 0 to data.Count-1)
        _Sort(data, 0, data.Count - 1);
    }

    /* Recursively use merge sort to sort a sublist
     * defined by first and last.
     * 
     *  Inputs:
     *     data  - list of values to be sorted
     *     first - the start of the sublist to sort
     *     last  - the end of the sublist to sort
     *  Outputs:
     *     None (in-place sorting)
     */
    public static void _Sort<T>(List<T> data, int first, int last) where T : IComparable<T>
    {
        // Base case: If the sublist contains only one element, it's already sorted
        if (first < last)
        {
            // Find the middle index to divide the list into two halves
            int mid = (first + last) / 2;

            // Recursively sort the first half of the list
            _Sort(data, first, mid);

            // Recursively sort the second half of the list
            _Sort(data, mid + 1, last);

            // Merge the two sorted halves
            Merge(data, first, mid, last);
        }
    }

    /* Merge two sorted sublists into a single sorted sublist.
     * The two sublists are defined by the indices:
     *   - The first sublist goes from "first" to "mid"
     *   - The second sublist goes from "mid + 1" to "last"
     * 
     *  Inputs:
     *     data  - list of values that contains two adjacent sorted sublists
     *     first - the start of the first sorted sublist
     *     mid   - the end of the first sorted sublist
     *     last  - the end of the second sorted sublist
     *  Outputs:
     *     None (in-place merging)
     */
    public static void Merge<T>(List<T> data, int first, int mid, int last) where T : IComparable<T>
    {
        // Create temporary lists to hold the sorted sublists
        List<T> left = new List<T>(mid - first + 1); // left sublist
        List<T> right = new List<T>(last - mid);     // right sublist

        // Copy the data into the temporary sublists
        for (int i = 0; i < left.Capacity; i++)
        {
            left.Add(data[first + i]);
        }
        for (int i = 0; i < right.Capacity; i++)
        {
            right.Add(data[mid + 1 + i]);
        }

        // Indices to track current position in each list
        int leftIndex = 0, rightIndex = 0, mergedIndex = first;

        // Merge the left and right sublists back into the original data list
        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            // Compare elements from both lists and place the smaller one into the original list
            if (left[leftIndex].CompareTo(right[rightIndex]) <= 0)
            {
                data[mergedIndex] = left[leftIndex];
                leftIndex++;
            }
            else
            {
                data[mergedIndex] = right[rightIndex];
                rightIndex++;
            }
            mergedIndex++;
        }

        // If there are remaining elements in the left sublist, copy them to the original list
        while (leftIndex < left.Count)
        {
            data[mergedIndex] = left[leftIndex];
            leftIndex++;
            mergedIndex++;
        }

        // If there are remaining elements in the right sublist, copy them to the original list
        while (rightIndex < right.Count)
        {
            data[mergedIndex] = right[rightIndex];
            rightIndex++;
            mergedIndex++;
        }
    }
}


