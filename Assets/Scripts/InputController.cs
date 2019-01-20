using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed;


    private void Update()
    {
        var vertical = Vector3.up * GetAxisMovement( "Vertical" );
        var horizontal = Vector3.right * GetAxisMovement( "Horizontal" );
        var movement = ( vertical + horizontal );
        movement = movement.magnitude > 1 ? movement : ( movement / movement.magnitude );
        movement *= _walkingSpeed * Time.deltaTime;

        if ( movement.magnitude > 0 )
        {
            transform.position += movement;
        }

        if ( Input.GetButtonUp( "Inventory" ) )
        {
            GetComponent<InventoryController>().ShowCanvas();
        }
    }


    private static float SignedSquare( float value )
    {
        return value >= 0 ? Mathf.Pow( value, 8 ) : -Mathf.Pow( value, 8 );
    }


    private static float GetAxisMovement( string axis )
    {
        return ( Input.GetButton( axis ) ? Input.GetAxis( axis ) : SignedSquare( Input.GetAxis( axis ) ) * 0.5f );
    }
}
