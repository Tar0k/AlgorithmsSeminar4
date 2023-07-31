using System.Dynamic;

namespace AlgorithmsSeminar4;

public class HashMap<T1, T2>
{
    public static int InitBucketCount = 16;
    private List<Bucket<T1, T2>> _buckets;

    public HashMap()
    {
        _buckets = new List<Bucket<T1, T2>>(InitBucketCount);
        foreach (var _ in Enumerable.Range(0, InitBucketCount))
        {
            _buckets.Add(new Bucket<T1, T2>());
        }
    }
    
    public HashMap(int initCapacity)
    {
        _buckets = new List<Bucket<T1, T2>>(initCapacity);
        foreach (var _ in Enumerable.Range(0, initCapacity))
        {
            _buckets.Add(new Bucket<T1, T2>());
        }
    }


    private class Bucket<T1, T2>
    {
        private Node? Head { get; set; }
        
        private class Node
        {
            public Entity<T1, T2> Value { get; set; }
            public Node? Next { get; set; }
        }

        public object? Add(Entity<T1, T2> entity)
        {
            var node = new Node
            {
                Value = entity
            };

            if (Head == null)
            {
                Head = node;
                return null;
            }

            var currentNode = Head;
            while (true)
            {
                if(currentNode.Value.Key.Equals(entity.Key))
                {
                    var buf = currentNode.Value.Value;
                    currentNode.Value.Value = entity.Value;
                    return buf;
                }

                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                else
                {
                    currentNode.Next = node;
                    return null;
                }
            }

        }

        public object? Get(T1 key)
        {
            var node = Head;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                    return node.Value.Value;
                node = node.Next;
            }
            return null;
        }

        public object? Remove(T1 key)
        {
            if (Head == null)
                return null;
            if (Head.Value.Key.Equals(key))
            {
                var buf = Head.Value.Value;
                Head = Head.Next;
                return buf;
            }
            else
            {
                var node = Head;
                while (node.Next != null)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        var buf = node.Next.Value.Value;
                        node.Next = node.Next.Next;
                        return buf;
                    }
                    node = node.Next;
                }
                return null;
            }
        }
        
        public IEnumerator<(T1, T2)> GetEnumerator()
        {
            if (Head == null)
                yield break;
            var node = Head;
            while (node != null)
            {
                yield return (node.Value.Key, node.Value.Value);
                node = node.Next;
            }
        }

    }


    private class Entity<T1, T2>
    {
        public T1 Key;
        public T2 Value;
    }

    private int CalculateBucketIndex<K>(K key)
    {
        var index =  key.GetHashCode() % _buckets.Capacity;
        index = Math.Abs(index);
        return index;
    }

    public object? Put(T1 key, T2 value)
    {
        var index = CalculateBucketIndex(key);
        if (_buckets[index] == null)
        {
            var bucket = new Bucket<T1, T2>();
            _buckets[index] = bucket;
        }

        var entity = new Entity<T1, T2>
        {
            Key = key,
            Value = value
        };
        return _buckets[index].Add(entity);
        
    }
    
    public object? Get(T1 key)
    {
        var index = CalculateBucketIndex(key);
        if (_buckets[index] == null) return null;
        return _buckets[index].Get(key);
    }

    public object? Remove(T1 key)
    {
        var index = CalculateBucketIndex(key);
        if (_buckets[index] == null) return null;
        return _buckets[index].Remove(key);
    }
    
    public IEnumerator<(int, T1, T2)> GetEnumerator()
    {
        foreach (var item in _buckets.Select((bucket, index) => new { bucket, index }))
        {
            foreach (var value in item.bucket)
            {
                yield return (item.index, value.Item1, value.Item2);
            }
        }
    }
    
    
}