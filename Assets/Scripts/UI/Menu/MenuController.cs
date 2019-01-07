using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Update()
    {
        if ( Input.GetButtonDown( "Submit" ) )
        {
            LoadMainScene();
        }
        else if ( Input.GetButtonDown( "Cancel" ) )
        {
            Quit();
        }
    }


    public void LoadMainScene()
    {
        SceneManager.LoadScene( "Main" );
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}