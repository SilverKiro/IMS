using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private DraggableItem _draggableItem;


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

        if ( !item ) return;

        var temp = item._draggableItem;
        item._draggableItem = _draggableItem;
        _draggableItem = temp;

        if ( _draggableItem != null )
        {
            _draggableItem.transform.SetParent( transform );
            _draggableItem.transform.position = transform.position;
        }

        if ( item._draggableItem != null )
        {
            item._draggableItem.transform.SetParent( item.transform );
            item._draggableItem.transform.position = item.transform.position;
        }
    }


    private void OnDisable()
    {
        if ( !_draggableItem ) return;

        _draggableItem.transform.position = transform.position;
    }
}
