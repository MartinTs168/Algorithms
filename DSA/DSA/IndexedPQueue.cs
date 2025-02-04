using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

class IndexedPQueue<T> where T : IComparable<T>
{

    private int count;
    private readonly int capacity;
    //lookup arrays to track child/parent indexes of each node
    private readonly int[] child, parent;
    //postion map maps key indexes to where the
    //postion of that key is represented in the priority queue
    private readonly int[] pm;
    //im is the inverse of pm
    //im[pm[i]] = pm[im[i]] = i
    private readonly int[] im;
    //indexed with the keys of the elemnts
    private T[] values;

    public IndexedPQueue(int capacity)
    {
        if (capacity <= 0) throw new ArgumentException("Illegal capacity");

        this.capacity = Math.Max(capacity, 3);
        im = new int[this.capacity];
        pm = new int[this.capacity];
        parent = new int[this.capacity];
        child = new int[this.capacity];
        values = new T[this.capacity];
        count = 0;

        for (int i = 0; i < capacity; i++)
        {
            parent[i] = (i - 1) / 2;
            child[i] = i * 2 + 1;
            pm[i] = im[i] = -1;
        }
    }

    public int Size()
    {
        return count;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public bool Contains(int key)
    {
        KeyInBoundsOrThrow(key);
        return pm[key] != -1;
    }

    public int PeekMinKeyIndex()
    {
        IsNotEmptyOrThrow();
        return im[0];
    }

    public int PollMinKeyIndex()
    {
        int minKey = PeekMinKeyIndex();
        Delete(minKey);
        return minKey;
    }

    public T PeekMinValue()
    {
        IsNotEmptyOrThrow();
        return values[im[0]];
    }

    public T PollMinValue()
    {
        T minVal = PeekMinValue();
        Delete(PeekMinKeyIndex());
        return minVal;
    }

    public void Insert(int key, T value)
    {
        if (Contains(key)) throw new ArgumentException("Key already exists");
        ValueNotNullOrThrow(value);
        pm[key] = count;
        im[count] = key;
        values[key] = value;
        Swim(count++);
    }

    public T ValueOf(int key)
    {
        KeyExistsOrThrow(key);
        return values[key];
    }

    public T Delete(int key)
    {
        KeyExistsOrThrow(key);
        int i = pm[key];
        Swap(i, --count);
        Sink(i);
        Swim(i);
        T value = values[key];
        values[key] = default;
        pm[key] = -1;
        im[count] = -1;

        return value;
    }

    public T Update(int key, T value)
    {
        KeyExistsAndValueNoNullOrThrow(key, value);
        int i = pm[key];
        T oldValue = values[key];
        values[key] = value;
        Sink(i);
        Swim(i);
        return oldValue;
    }

    //strictly decreases the value at key
    public void Decrease(int key, T value)
    {
        KeyExistsAndValueNoNullOrThrow(key, value);
        if (Less(value, values[key]))
        {
            values[key] = value;
            Swim(pm[key]);
        }
    }

    //strictly increase the value at key
    public void Increase(int key, T value)
    {
        KeyExistsAndValueNoNullOrThrow(key, value);
        if (Less(values[key], value))
        {
            values[key] = value;
            Sink(pm[key]);
        }
    }


    /* Helper methods */

    private static bool Less(T value1, T value2)
    {
        if (value1.CompareTo(value2) < 0) return true;
        return false;
    }

    private static bool Less(int i, int j)
    {
        if (i < j) return true;
        return false;
    }

    private void Swap(int i, int j)
    {
        pm[im[j]] = i;
        pm[im[i]] = j;

        int tmp = im[i];
        im[i] = im[j];
        im[j] = tmp;
    }

    private void Swim(int index)
    {
        while (Less(index, parent[index]))
        {
            Swap(index, parent[index]);
            index = parent[index];
        }
    }

    private void Sink(int index)
    {
        for (int j = MinChild(index); j != -1;)
        {
            Swap(index, j);
            index = j;
            j = MinChild(index);
        }

    }

    private int MinChild(int i)
    {
        int index = -1, from = child[i], to = Math.Min(count, from + 2);

        for (int j = from; j < to; j++)
        {
            if (Less(j, i))
            {
                index = i = j;
            }
        }
        return index;
    }

    private void IsNotEmptyOrThrow()
    {
        if (IsEmpty()) throw new InvalidOperationException("PQueue is empty");
    }

    private void KeyInBoundsOrThrow(int key)
    {
        if (key < 0 || key >= capacity) throw new IndexOutOfRangeException();
    }

    private void KeyExistsOrThrow(int key)
    {
        KeyInBoundsOrThrow(key);
        if (!Contains(key)) throw new KeyNotFoundException();
    }

    private void ValueNotNullOrThrow(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
    }

    private void KeyExistsAndValueNoNullOrThrow(int key, T value)
    {
        KeyExistsOrThrow(key);
        ValueNotNullOrThrow(value);
    }
}