public class Entry<K, V>
{
    int hash;
    K key;
    V value;

    public Entry(K key, V value)
    {
        this.key = key;
        this.value = value;
        this.hash = key.GetHashCode();
    }

    public bool Equals(Entry<K, V> other)
    {
        if (hash != other.hash) return false;
        return key.Equals(other.key);
    }

    public override string ToString()
    {
        return $"{key} => {value}";
    }

    public int Hash { get { return hash; } }
    public V Value { get { return value; } set { this.value = value; } }
    public K Key { get { return key; } }
}

public class HashTableSeparateChaining<K, V>
{
    private const int DEFAULT_CAPACITY = 3;
    private const double DEFAULT_LAOD_FACTOR = 0.75;

    private double maxLoadFactor;

    //capacity = size of table
    //threshold = maxLoadFactor * capacity used to determine when to resize
    //size = number of elements in table
    private int capacity, threshold, size = 0;
    private LinkedList<Entry<K, V>>[] table;

    public HashTableSeparateChaining() : this(DEFAULT_CAPACITY, DEFAULT_LAOD_FACTOR)
    {

    }


    public HashTableSeparateChaining(int capacity) : this(capacity, DEFAULT_LAOD_FACTOR)
    {

    }

    public HashTableSeparateChaining(int capacity, double maxLoadFactor)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException("Illegal capacity");
        }
        if (maxLoadFactor <= 0 || Double.IsNaN(maxLoadFactor) || Double.IsInfinity(maxLoadFactor))
        {
            throw new ArgumentOutOfRangeException("Illegal maxLoadFactor");
        }
        this.maxLoadFactor = maxLoadFactor;
        this.capacity = Math.Max(DEFAULT_CAPACITY, capacity);
        threshold = (int)(this.capacity * maxLoadFactor);
        table = new LinkedList<Entry<K, V>>[this.capacity];

    }

    public int Size()
    {
        return size;
    }


    public bool IsEmpty()
    {
        return Size() == 0;
    }

    // Coverts a hash value to an index.
    private int NormalizeHash(int keyHash)
    {
        return (keyHash & 0x7FFFFFFF) % capacity;
    }

    public void Clear()
    {
        Array.Fill(table, null);
        size = 0;
    }

    public bool ContainsKey(K key)
    {
        return HasKey(key);
    }

    public bool HasKey(K key)
    {
        int bucketIndex = NormalizeHash(key.GetHashCode());
        return BucketSeekEntry(key, bucketIndex) != null;
    }

    public V Insert(K key, V value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        Entry<K, V> newEntry = new(key, value);
        int bucketIndex = NormalizeHash(newEntry.Hash);
        return BucketInsertEntry(bucketIndex, newEntry);
    }

    //return default(V) if entry is null
    public V Get(K key)
    {
        if (key == null) throw new ArgumentNullException(nameof
        (key));

        int bucketIndex = NormalizeHash(key.GetHashCode());
        Entry<K, V> entry = BucketSeekEntry(key, bucketIndex);
        if (entry == null) throw new KeyNotFoundException("Key not found");

        return entry.Value;
    }

    public V Remove(K key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        int bucketIndex = NormalizeHash(key.GetHashCode());
        return BucketRemoveEntry(bucketIndex, key);
    }

    //remove an entry from a bucket if it exists
    private V BucketRemoveEntry(int bucketIndex, K key)
    {
        Entry<K, V> entry = BucketSeekEntry(key, bucketIndex);
        if (entry != null)
        {
            LinkedList<Entry<K, V>> links = table[bucketIndex];
            links.Remove(entry);
            --size;
            return entry.Value;
        }
        return default;
    }

    private V BucketInsertEntry(int bucketIndex, Entry<K, V> entry)
    {
        LinkedList<Entry<K, V>> bucket = table[bucketIndex];
        if (bucket == null)
        {
            table[bucketIndex] = bucket = new LinkedList<Entry<K, V>>();
        }

        Entry<K, V> existentEntry = BucketSeekEntry(entry.Key, bucketIndex);
        if (existentEntry == null || existentEntry == default(Entry<K, V>))
        {
            bucket.AddLast(entry);
            if (++size > threshold)
            {
                ResizeTable();
            }
            return default; // indicationg there wasn't a previous entry
        }
        V oldVal = existentEntry.Value;
        existentEntry.Value = entry.Value;
        return oldVal; // we return the old value of the entry
    }
    private Entry<K, V> BucketSeekEntry(K key, int bucketIndex)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        LinkedList<Entry<K, V>> bucket = table[bucketIndex];
        if (bucket == null) return default;
        foreach (Entry<K, V> entry in bucket)
        {
            if (entry.Key.Equals(key))
            {
                return entry;
            }
        }
        return default;
    }

    private void ResizeTable()
    {
        capacity *= 2;
        threshold = (int)(capacity * maxLoadFactor);

        LinkedList<Entry<K, V>>[] newTable = new LinkedList<Entry<K, V>>[capacity];
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i] != null)
            {
                foreach (Entry<K, V> entry in table[i])
                {
                    int bucketIndex = NormalizeHash(entry.Hash);
                    LinkedList<Entry<K, V>> bucket = newTable[bucketIndex];
                    if (bucket == null) newTable[bucketIndex] = bucket = new LinkedList<Entry<K, V>>();
                    bucket.AddLast(entry);
                }

                //Healp the garbage collector
                table[i].Clear();
                table[i] = null;
            }
        }
        table = newTable;
    }

    public List<K> Keys()
    {
        List<K> keys = new List<K>(Size());
        foreach (LinkedList<Entry<K, V>> bucket in table)
        {
            if (bucket != null)
            {
                foreach (Entry<K, V> entry in bucket)
                {
                    keys.Add(entry.Key);
                }
            }
        }

        return keys;
    }

    public List<V> Values()
    {
        List<V> values = new List<V>(Size());
        foreach (LinkedList<Entry<K, V>> bucket in table)
        {
            if (bucket != null)
            {
                foreach (Entry<K, V> entry in bucket)
                {
                    values.Add(entry.Value);
                }
            }
        }

        return values;
    }
}