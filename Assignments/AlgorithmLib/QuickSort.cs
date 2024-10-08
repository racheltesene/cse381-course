/*  CSE 381 - Quick Sort
 *  (c) BYU-Idaho - It is an honor code violation to post this
 *  file completed in a public file sharing site. F4.
 *
 *  Instructions: Implement the Partition and _Sort functions per the instructions
 *  in the comments.  Run all tests in QuickSortTest.cs to verify your code.
 */
namespace AlgorithmLib
{
    public static class QuickSort
    {
        /* Sort the entire list using QuickSort
         *
         *  Inputs:
         *     data - list of values to be sorted
         *  Outputs:
         *     none (the list is sorted in place)
         */
        public static void Sort<T>(List<T> data) where T : IComparable<T>
        {
            // Start the recursion with the entire list: indices 0 to data.Count - 1
            _Sort(data, 0, data.Count - 1);
        }

        /* Recursively sort a sublist defined by first and last
         *  Inputs:
         *     data - list of values to be sorted
         *     first - the index of the first element in the sublist
         *     last - the index of the last element in the sublist
         *  Outputs:
         *     none (the sublist is sorted in place)
         */
        public static void _Sort<T>(List<T> data, int first, int last) where T : IComparable<T>
        {
            // Base case: if the list has one or no elements, it is already sorted
            if (first < last)
            {
                // Partition the list and get the pivot's final position
                int pivotIndex = Partition(data, first, last);

                // Recursively sort the left side of the pivot (from first to pivotIndex - 1)
                _Sort(data, first, pivotIndex - 1);

                // Recursively sort the right side of the pivot (from pivotIndex + 1 to last)
                _Sort(data, pivotIndex + 1, last);
            }
        }

        /* Partition the sublist around a pivot
         *  Inputs:
         *     data - list of values
         *     first - the start of the sublist
         *     last - the end of the sublist (pivot element)
         *  Outputs:
         *     The index where the pivot is finally placed
         *
         *  Algorithm:
         *     1. Select the last element as the pivot.
         *     2. Reorder the sublist such that all elements smaller than or equal to the pivot
         *        are on the left side, and all elements greater than the pivot are on the right.
         *     3. Swap the pivot into its correct position.
         */
        public static int Partition<T>(List<T> data, int first, int last) where T : IComparable<T>
        {
            // The pivot is always the last element of the sublist
            T pivot = data[last];

            // Index of the smaller element; starts before the first element
            int i = first - 1;

            // Loop through the sublist (excluding the pivot) to rearrange elements
            for (int j = first; j < last; j++)
            {
                // If the current element is less than or equal to the pivot, move it to the left
                if (data[j].CompareTo(pivot) <= 0)
                {
                    i++; // Increment the smaller element index
                    Swap(data, i, j); // Swap the current element with the one at i
                }
            }

            // After rearranging, place the pivot in its correct position (i + 1)
            Swap(data, i + 1, last);

            // Return the index of the pivot
            return i + 1;
        }

        /* Swap two elements in the list
         *  Inputs:
         *     data - list of values
         *     i - the index of the first element to swap
         *     j - the index of the second element to swap
         *  Outputs:
         *     none (the elements are swapped in place)
         */
        private static void Swap<T>(List<T> data, int i, int j)
        {
            T temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }
}
