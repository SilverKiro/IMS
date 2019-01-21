using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _totalWeight;
    [SerializeField] private int _totalItems;
    [SerializeField] private int _capacity;

    // private Dictionary<Item, int> _items;

    //public Dictionary<Item, int> Items => _items;
    public List<Item> Items { get; } = new List<Item>();

    public int TotalWeight
    {
        get { return _totalWeight; }
        set { _totalWeight = value; }
    }

    public int TotalItems
    {
        get { return _totalItems; }
        set { _totalItems = value; }
    }

    public int Capacity
    {
        get { return _capacity; }
        set { _capacity = value; }
    }


    public void AddItem( Item item )
    {
        //int defVal;
        if ( item.GetWeight() + _totalWeight > _capacity )
        {
            throw new Exception( "Inventory is full!" );
        }

        Items.Add( item );
        /*
        if ( _items.TryGetValue( item, out defVal ) )
        {
            _items.Remove( item );
            _items.Add( item, defVal + 1 );
            _totalItems += 1;
        }
        else
        {
            _items.Add( item, 1 );
            _totalItems += 1;
        }
        */
        _totalWeight += item.GetWeight();
    }


    public void RemoveItem( Item item )
    {
        //int defVal;

        Items.Remove( item );
        /*
        if ( _items.TryGetValue( item, out defVal ) && defVal.CompareTo( 1 ) >= 0 )
        {
            _items[item] -= 1;
            _totalItems -= 1;
        }

        if ( _items.TryGetValue( item, out defVal ) && defVal.CompareTo( 0 ) <= 0 )
        {
            _items.Remove( item );
        }
        */
        _totalWeight -= item.GetWeight();
    }

    /*
    public override string ToString()
    {
        var toReturn = "[";
        foreach ( var kvp in _items )
        {
            toReturn += "(" + kvp.Key + ", " + kvp.Value + "), ";
        }

        return toReturn + "]";
    }
    */
}