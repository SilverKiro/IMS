using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuController : MonoBehaviour
{
    private void Update()
    {
        if ( Input.GetButtonDown( "Cancel" ) )
        {
            SceneManager.LoadScene( "Menu" );
        }
    }
}
