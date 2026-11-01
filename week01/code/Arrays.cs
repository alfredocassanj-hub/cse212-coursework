
public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // 1. Create an array with the requested length.
        double[] multiples = new double[length];

        // 2. Loop through each position of the array.
        for (int i = 0; i < length; i++)
        {
            // 3. Calculate the multiple of the starting number.
            // 4. Store the result in the current array position.
            multiples[i] = number * (i + 1);
        }

        // 5. Return the completed array.
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // 1. Calculate the starting index of the elements to move.
        int startIndex = data.Count - amount;

        // 2. Get the last elements from the list.
        List<int> lastElements = data.GetRange(startIndex, amount);

        // 3. Remove the last elements from the original list.
        data.RemoveRange(startIndex, amount);

        // 4. Insert the saved elements at the beginning.
        data.InsertRange(0, lastElements);
    }
}
