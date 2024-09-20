﻿/* CSE 381 - Merge Sort
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
     *     data - list of values
     *  Outputs:
     *     none
     */
    public static void Sort<T>(List<T> data) where T : IComparable<T> 
    {
        // Start the recursive process with the whole list
        _Sort(data, 0, data.Count-1);
    }

    /* Recursively use merge sort to sort a sublist
     * defined by first and last.
     * 
     *  Inputs:
     *     data - list of values
     *     first - the start of the sublist
     *     last - the end of the sublist
     *  Outputs:
     *     None
     */
    public static void _Sort<T>(List<T> data, int first, int last) where T : IComparable<T>
    {
    }
    
    /* Merge two sorted list which are adjacent to each other back into
     * the same list.
     *
     *  Inputs:
     *     data - list of values
     *     first - the start of the first sorted sublist
     *     mid - the end of the first sorted sublist (second sublist starts after)
     *     last - the end of the second sorted sublist
     *  Outputs:
     *     None
     */
    public static void Merge<T>(List<T> data, int first, int mid, int last) where T : IComparable<T>
    {
    }
}

