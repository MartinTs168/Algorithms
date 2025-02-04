public class UnionFind
{

    //number of elemnts
    private int size;

    //tracking the sizes of each component
    private int[] sz;
    //id[i] points to the parent of i
    private int[] id;

    private int numComponents;

    public UnionFind(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Size must be a postitive integer");
        }

        this.size = numComponents = size;
        sz = new int[size];
        id = new int[size];
        for (int i = 0; i < size; i++)
        {
            id[i] = i;
            sz[i] = 1; // every component starts with size 1
        }
    }

    public int Find(int p)
    {
        int root = p;
        while (root != id[root])
        {
            root = id[root];
        }

        //Path comperession
        while (p != root)
        {
            int next = id[p];
            id[p] = root;
            p = next;
        }

        return root;
    }

    public bool Connected(int p, int q)
    {
        return Find(p) == Find(q);
    }

    public int ComponentSize(int p)
    {
        return sz[Find(p)];
    }

    public int Size()
    {
        return size;
    }

    public int Components()
    {
        return numComponents;
    }

    public void Unify(int p, int q)
    {
        int root1 = Find(p);
        int root2 = Find(q);

        if (root1 == root2) return;

        if (sz[root1] < sz[root2])
        {
            sz[root2] += sz[root1];
            id[root1] = root2;
        }
        else
        {
            sz[root1] += sz[root2];
            id[root2] = root1;
        }

        numComponents--;
        
    }

}
