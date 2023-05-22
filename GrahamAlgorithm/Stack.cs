using System.Collections;

namespace GrahamAlgorithm;

public class Stack<T> : IEnumerable<T>
{
    private T?[] _array;
    private const int DefaultArraySize = 4;
    
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _array[i]!;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public Stack()
    {
        _array = new T?[DefaultArraySize];
    }
    public void Push(T? item)
    {
        if (Count == _array.Length)
        {
            ResizeArray();
        }

        _array[Count++] = item;
    }

    public T Pop()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }
        
        var result = _array[Count - 1];
        _array[Count - 1] = default(T);
        Count--;
        return result!;
    }

    public T? Peek()
    {
        return IsEmpty ? default(T) : _array[Count - 1];
    }

    public T? PeekNext()
    {
        return Count < 2 ? default(T) : _array[Count - 2];
    }

    private void ResizeArray()
    {
        T?[] newArray = new T[_array.Length * 2];
        for (int i = 0; i < _array.Length; i++)
        {
            newArray[i] = _array[i];
        }

        _array = newArray;
    }
}