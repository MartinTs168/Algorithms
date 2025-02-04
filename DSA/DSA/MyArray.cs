public class MyArray
{
    private int[] array;
    private int item_count = 0;
    public MyArray(int length)
    {
        array = new int[length];
    }

    public void Insert(int item)
    {
        if (item_count == array.Length)
        {
            int[] new_arr = new int[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                new_arr[i] = array[i];
            }

            array = new_arr;
        }
        array[item_count] = item;
        item_count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= array.Length)
        {
            throw new IndexOutOfRangeException("Index out of range");
        }

        for (int i = index; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }
        item_count--;

    }

    public int IndexOf(int item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void Print()
    {
        for (int i = 0; i < item_count; i++)
        {
            System.Console.WriteLine(array[i]);
        }
    }
}