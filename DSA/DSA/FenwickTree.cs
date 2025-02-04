public class FenwickTree
{
    private long[] tree;

    public FenwickTree(int n)
    {
        tree = new long[n + 1];
    }

    //values arr must not use index 0
    //must be one based
    public FenwickTree(long[] values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        this.tree = (long[])values.Clone();

        for (int i = 1; i < tree.Length; i++)
        {
            int j = i + (i & -i);
            if (j < tree.Length)
            {
                tree[j] = tree[j] + tree[i];
            }
        }
    }

    // returns the value of the least significant bit
    //lsb(108) = lsb(0b1101100) =    0b100 = 4
    //lsb(104) = lsb(0b1101000) =   0b1000 = 8
    private int Lsb(int i)
    {
        return i & -i;
    }

    //computes the prefix sum from [1, i]
    public long PrefixSum(int i)
    {
        long sum = 0L;
        while (i != 0)
        {
            sum += tree[i];
            i &= ~Lsb(i); //equivalent to i -= Lsb(i);
        }

        return sum;

    }

    private long Sum(int i, int j)
    {
        if (j < i) throw new ArgumentException("j must be >= i");
        return PrefixSum(j) - PrefixSum(i - 1);
    }

    //add 'k' to index 'i' one based
    public void Add(int i, long k)
    {
        //we loop to update the value for every
        //responsibility
        while (i < tree.Length)
        {
            tree[i] += k;
            i += Lsb(i);
        }
    }


    //set index i to be equal to k
    public void Set(int i, long k)
    {
        long value = Sum(i, i);
        Add(i, k - value);
    }
}