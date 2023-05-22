namespace GrahamAlgorithm;

using System.Collections;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private DoublyLinkedListNode<T>? _head;
    private DoublyLinkedListNode<T>? _tail;
    public DoublyLinkedListNode<T>? FirstNode => _head;
    public DoublyLinkedListNode<T>? LastNode => _tail;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    
    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddLast(T data)
    {
        var node = new DoublyLinkedListNode<T>(data);
        if (IsEmpty)
        {
            _head = node;
        }
        else
        {
            _tail!.Next = node;
            node.Previous = _tail;
        }
        _tail = node;
        Count++;
    }

    public void AddFirst(T data)
    {
        var node = new DoublyLinkedListNode<T>(data);
        if (IsEmpty)
        {
            _tail = node;
        }
        else
        {
            node.Next = _head;
            _head!.Previous = node;
        }

        _head = node;
        Count++;
    }

    public bool Remove(T data)
    {
        if (IsEmpty) return false;
        if (_head!.Data!.Equals(data)) return RemoveFirst();
        if (_tail!.Data!.Equals(data)) return RemoveLast();
        var current = _head.Next;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                current.Previous!.Next = current.Next;
                current.Next!.Previous = current.Previous;
                Count--;
                break;
            }

            current = current.Next;
        }
        return true;
    }

    private bool RemoveFirst()
    {
        if (IsEmpty) return false;
        if (Count == 1)
        {
            Clear();
        }
        else
        {
            _head = _head!.Next;
            _head!.Previous = null;
            Count--;
        }

        return true;
    }

    public bool RemoveLast()
    {
        if (IsEmpty) return false;
        if (Count == 1)
        {
            Clear();
        }
        else
        {
            _tail = _tail!.Previous;
            _tail!.Next = null;
            Count--;
        }

        return true;

    }
    public bool Contains(T data)
    {
        if (IsEmpty) return false;
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public void Clear()
    {
        _head = _tail = null;
        Count = 0;
    }
}

public class DoublyLinkedListNode<T>
{
    public DoublyLinkedListNode<T>? Previous { get; set; }
    public DoublyLinkedListNode<T>? Next { get; set; }
    public T Data { get; }

    public DoublyLinkedListNode(T data)
    {
        Data = data;
    }
        
}