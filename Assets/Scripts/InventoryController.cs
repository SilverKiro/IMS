using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _imsCanvasObj;
    [SerializeField] private GameObject _chestCanvasObj;


    private void Start()
    {
        ShowChest( false );
    }


    public void ShowCanvas()
    {
        _imsCanvasObj.SetActive( !_imsCanvasObj.activeSelf );
    }


    private void OnTriggerEnter2D( Collider2D other )
    {
        ShowChest( true );
    }


    private void OnTriggerExit2D( Collider2D other )
    {
        ShowChest( false );
    }


    private void ShowChest( bool show )
    {
        _imsCanvasObj.SetActive( show );
        _chestCanvasObj.SetActive( show );
    }
}
