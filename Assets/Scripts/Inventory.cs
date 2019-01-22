using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _totalWeight;
    [SerializeField] private int _totalItems;
    [SerializeField] private int _capacity;

    public Text WeightPreview;
    public GameObject[] Parents;
    public GameObject[] Children;

    // private Dictionary<Item, int> _items;

    //public Dictionary<Item, int> Items => _items;
    public List<Item> Items { get; } = new List<Item>();

    private void Start()
    {
        for ( var index = 0; index < Children.Length; index++ )
        {
            var parent = Parents[index];
            var draggableItem = parent.GetComponent<DraggingController>();
            draggableItem.Inventory = this;
            draggableItem.Location = index;
            
            var child = Children[index];
            if ( !child ) continue;
            var itemDb = child.GetComponent<ItemDB>();
            Debug.Log( child.name );
            itemDb.Loc = index;
            itemDb.Inventory = this;
            Items.Add( itemDb.Item );
            _totalItems += 1;
            AddTotalWeight( itemDb.Item.GetWeight() );
//            _totalWeight += itemDb.Item.GetWeight();
        }

        SetTotalWeightText();
    }

    // freeSpot
    // addToSpot
    // removeFromSpot
    
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
        AddTotalWeight( item.GetWeight() );
        //_totalWeight += item.GetWeight();
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
        ReduceTotalWeight( item.GetWeight() );
        //_totalWeight -= item.GetWeight();
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

    private void ReduceTotalWeight( int value )
    {
//        Debug.Log("Reduce: " + value);
        AddTotalWeight( -value );
    }

    private void AddTotalWeight( int value )
    {
//        Debug.Log("TotalWeightChange: " + value);
        _totalWeight += value;
        SetTotalWeightText();
    }

    private void SetTotalWeightText()
    {
        if ( WeightPreview )
        {
            WeightPreview.text = _totalWeight + "/" + _capacity;
            //WeightPreview.GetComponent<Text>().text = _totalWeight + "/" + _capacity;
        }
    }
}