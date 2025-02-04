public class Solution
{
    public static int[] ProductExceptSelf(int[] nums)
    {

        int n = nums.Length;
        int[] result = new int[n];

        int leftProduct = 1;
        int rightProduct = 1;

        for (int i = 0; i < n; i++)
        {
            result[i] = leftProduct;
            leftProduct *= nums[i];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            result[i] *= rightProduct;
            rightProduct *= nums[i];
        }

        return result;
    }

    public static IList<int> SpiralOrder(int[][] matrix)
    {
        int row = 0;
        int col = 0;
        int num_rows = matrix.Length;
        int num_cols = matrix[0].Length;
        int ele_count = num_cols * num_rows;

        IList<int> result = new List<int>();

        Dictionary<string, int[]> directions = new Dictionary<string, int[]>(){
            {"right", new int[] {0, 1}},
            {"down", new int[] {1, 0}},
            {"left", new int[] {0, -1}},
            {"up", new int[] {-1, 0}}
        };

        string curr_direction = "right";

        while (true)
        {
            result.Add(matrix[row][col]);
            matrix[row][col] = 101;
            if (result.Count == ele_count) break;

            int next_row = row + directions[curr_direction][0];
            int next_col = col + directions[curr_direction][1];

            if (next_row < 0 || next_row >= num_rows || next_col < 0 || next_col >= num_cols || matrix[next_row][next_col] == 101)
            {
                if (curr_direction == "right")
                {
                    curr_direction = "down";
                }
                else if (curr_direction == "down")
                {
                    curr_direction = "left";
                }
                else if (curr_direction == "left")
                {
                    curr_direction = "up";
                }
                else
                {
                    curr_direction = "right";
                }
            }

            row += directions[curr_direction][0];
            col += directions[curr_direction][1];

        }

        return result;
    }
}