using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RuntimeSetBase<T> : DescriptionBaseSO
{
    public UnityAction OnItemsChanged;

    [SerializeField]
    private List<T> _items = new List<T>();
    public List<T> Items
    {
        get { return _items; }
    }

    public void Add(T item)
    {
        if (item == null)
        { 
            Debug.LogError("A null value was provided to the " + this.name + " runtime set.");
			return;
        }
        if (!_items.Contains(item))
            _items.Add(item);
    }

    public void Remove(T item)
    {
        if (item == null)
        { 
            Debug.LogError("A null value was provided to the " + this.name + " runtime set.");
			return;
        }
        if (_items.Contains(item))
            _items.Remove(item);
    }

    private void OnDisable()
	{
        _items.Clear();
    }
}