using System;
using Items;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Inventory _chest;


    public void MaximizeTotalValueFromChest()
    {
        var items = _chest.Items.ToArray();
        /*
        var items = new Item[_chest.TotalItems];
        var globCounter = 0;
        foreach ( var kvp in _chest.Items )
        {
            var counter = kvp.Value;
            while ( counter-- > 0 )
            {
                items[globCounter++] = kvp.Key;
            }
        }
        */
        var capacity = _inventory.Capacity - _inventory.TotalWeight;
        var knapsack = new double[items.Length + 1, capacity + 1];
        var keep = new int[items.Length + 1, capacity + 1];
        var itemIdx = 0;
        for ( ; itemIdx <= items.Length; itemIdx++ )
        {
            var currentItem = itemIdx == 0 ? null : items[itemIdx - 1];
            for ( var currentCapacity = 0; currentCapacity <= capacity; currentCapacity++ )
            {
                if ( currentItem == null )
                {
                    knapsack[itemIdx, currentCapacity] = 0;
                    keep[itemIdx, currentCapacity] = 0;
                }
                else if ( currentItem.GetWeight() <= currentCapacity )
                {
                    var memorizedValue = currentItem.GetValue() +
                                         knapsack[itemIdx - 1, currentCapacity - currentItem.GetWeight()];
                    knapsack[itemIdx, currentCapacity] = Math.Max(
                        memorizedValue,
                        knapsack[itemIdx - 1, currentCapacity] );
                    if ( Math.Abs( knapsack[itemIdx, currentCapacity] - memorizedValue ) < 0.000000001 )
                    {
                        keep[itemIdx, currentCapacity] = 1;
                    }
                    else
                    {
                        keep[itemIdx, currentCapacity] = 0;
                    }
                }
                else
                {
                    knapsack[itemIdx, currentCapacity] = knapsack[itemIdx - 1, currentCapacity];
                    keep[itemIdx, currentCapacity] = 0;
                }
            }
        }

        itemIdx = items.Length;
        while ( capacity >= 0 && itemIdx > 0 )
        {
            if ( keep[itemIdx, capacity] == 1 )
            {
                var item = items[itemIdx - 1];
                _inventory.AddItem( item );
                _chest.RemoveItem( item );
                capacity -= item.Weight;
            }

            itemIdx--;
        }
    }
}
