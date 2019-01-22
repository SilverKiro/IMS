using Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private DraggableItem _draggableItem;

    public Inventory Inventory { get; set; }
    public int Location { get; set; }

    public override string ToString()
    {
        return Inventory.name + ": " + Location;
    }

    private void Start()
    {
        _draggableItem = gameObject.GetComponentInChildren<DraggableItem>();
    }


    public void OnBeginDrag( PointerEventData eventData )
    {
        if ( !_draggableItem ) return;
        
        _draggableItem.transform.SetParent( GameObject.Find( "DraggableItemHolder" ).transform );
    }


    public void OnDrag( PointerEventData eventData )
    {
        if ( !_draggableItem ) return;

        if ( eventData.dragging )
        {
            _draggableItem.transform.position = Input.mousePosition;
        }
    }


    public void OnEndDrag( PointerEventData eventData )
    {
        if ( !_draggableItem ) return;

        _draggableItem.transform.position = transform.position;
    }


    public void OnDrop( PointerEventData eventData )
    {
        var item = eventData.pointerDrag.GetComponent<DraggingController>();

        if ( !item || item == this ) return;

        if ( item.Inventory != Inventory )
        {
            if ( _draggableItem != null && _draggableItem.GetComponent<ItemDB>() != null )
            {
                var toBeFreed = 0;
                if ( item._draggableItem != null && item._draggableItem.GetComponent<ItemDB>() != null )
                {
                    toBeFreed = item._draggableItem.GetComponent<ItemDB>().Item.Weight;
                }
                
                if ( item.Inventory.Capacity - item.Inventory.TotalWeight + toBeFreed < _draggableItem.GetComponent<ItemDB>().Item.Weight )
                {
                    return;
                }
            }

            if ( item._draggableItem != null && item._draggableItem.GetComponent<ItemDB>() != null )
            {
                var toBeFreed = 0;
                if ( _draggableItem != null && _draggableItem.GetComponent<ItemDB>() != null )
                {
                    toBeFreed = _draggableItem.GetComponent<ItemDB>().Item.Weight;
                }
                
                if ( Inventory.Capacity - Inventory.TotalWeight + toBeFreed < item._draggableItem.GetComponent<ItemDB>().Item.Weight )
                {
                    return;
                }
            }
        }

        var temp = item._draggableItem;
        item._draggableItem = _draggableItem;
        _draggableItem = temp;

        if ( _draggableItem != null ) item.Inventory.RemoveItem( _draggableItem.GetComponent<ItemDB>().Item );
        if ( item._draggableItem != null ) Inventory.RemoveItem( item._draggableItem.GetComponent<ItemDB>().Item );
        
        if ( _draggableItem != null )
        {
            _draggableItem.transform.SetParent( transform );
            _draggableItem.transform.position = transform.position;
            Inventory.AddItem( _draggableItem.GetComponent<ItemDB>().Item );
        }

        if ( item._draggableItem != null )
        {
            item._draggableItem.transform.SetParent( item.transform );
            item._draggableItem.transform.position = item.transform.position;
            item.Inventory.AddItem( item._draggableItem.GetComponent<ItemDB>().Item );
        }
    }


    private void OnDisable()
    {
        if ( !_draggableItem ) return;

        _draggableItem.transform.position = transform.position;
    }
}
