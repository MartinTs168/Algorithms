using System.Collections;

public class PQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    //number of elements currently in the heap
    private int heapSize = 0;
    //internal capacity of the heap
    private int heapCapacity = 0;

    private List<T> heap = null;

    private Dictionary<T, SortedSet<int>> map = new Dictionary<T, SortedSet<int>>();

    public PQueue()
    {
        heapCapacity = 1;
    }

    public PQueue(int size)
    {
        heap = new List<T>(size);
        heapCapacity = size;
    }

    public PQueue(T[] elems)
    {
        heapSize = heapCapacity = elems.Length;
        heap = new List<T>(elems);

        for (int i = 0; i < heapSize; i++)
        {
            MapAdd(elems[i], i);
        }

        //heapify
        for (int i = Math.Max(0, (heapSize / 2) - 1); i >= 0; i--)
        {
            Sink(i);
        }
    }

    public bool IsEmpty()
    {
        return heapSize == 0;
    }

    public void Clear()
    {
        heap.Clear();
        heapSize = 0;
        map.Clear();
    }

    public int Size()
    {
        return heapSize;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("PQueue is empty");
        }

        return heap[0];
    }

    public T Dequeue()
    {
        return Dequeue(0);
    }

    public bool Contains(T item)
    {
        if (item == null)
        {
            return false;
        }

        return map.ContainsKey(item);
    }

    public void Enqueue(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        if (heapSize < heapCapacity)
        {
            heap[heapSize] = item;
        }
        else
        {
            heap.Add(item);
            heapCapacity++;
        }

        MapAdd(item, heapSize);
        Swim(heapSize);
        heapSize++;

    }

    private bool Less(int i, int j)
    {
        T node1 = heap[i];
        T node2 = heap[j];

        // returns true if element1 is less than or equal to element2 else false
        return node1.CompareTo(node2) <= 0;
    }

    private void Swim(int index)
    {
        int parent = (index - 1) / 2;

        while (index > 0 && Less(index, parent))
        {
            Swap(parent, index);
            index = parent;
            parent = (index - 1) / 2;
        }
    }

    private void Sink(int index)
    {
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            int smallest = left; // assumption left is the smallest node

            //check if right is less than left
            if (right < heapSize && Less(right, left))
            {
                smallest = right;
            }

            //check if out of bounds or curr_el is less than smallest 
            //meaning curr_el is in the right position
            if (left >= heapSize || Less(index, smallest))
            {
                break;
            }

            Swap(smallest, index);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        T i_elem = heap[i];
        T j_elem = heap[j];

        heap[i] = j_elem;
        heap[j] = i_elem;

        MapSwap(i_elem, j_elem, i, j);
    }



    private T Dequeue(int index)
    {
        if (IsEmpty()) throw new InvalidOperationException();

        heapSize--;
        T removed_item = heap[index];
        Swap(index, heapSize);
        MapRemove(removed_item, heapSize);

        //if the very last elemnt is removed meaning
        //the heap is empty we return to avoid swapping attempts
        if (index == heapSize) return removed_item;

        T elem = heap[index];
        Sink(index);

        if (heap[index].Equals(elem))
        {
            Swim(index);
        }

        return removed_item;
    }

    private void MapRemove(T key, int value)
    {
        SortedSet<int> set = map[key];

        set.Remove(value);
        if (set.Count == 0) map.Remove(key);
    }

    private void MapAdd(T key, int value)
    {
        if (map.TryGetValue(key, out SortedSet<int>? set))
        {
            set.Add(value);
        }
        else
        {
            map[key] = new SortedSet<int> { value };
        }
    }

    private void MapSwap(T item1, T item2, int item1Index, int item2Index)
    {
        var set1 = map[item1];
        var set2 = map[item2];

        set1.Remove(item1Index);
        set2.Remove(item2Index);

        set1.Add(item2Index);
        set2.Add(item1Index);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heap.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}