using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwtichScenes : MonoBehaviour
{
    public InputAction inputMove;
    private float switchValue;
    public StringData stringData;
    public GameEvents gameEvents;
    string stData;
    private void FixedUpdate()
    {
        SwtichScene();
    }
    public void SwtichScene()
    {
        inputMove.performed += ctx =>
        {
            switchValue = ctx.ReadValue<float>();
            stData = "TransferTestData";
            //PlayerPrefs.SetString( "trData" , stData );
            StartCoroutine( CallToSwitchScenes() );
        };
    }
    private void OnEnable()
    {
        inputMove.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        inputMove.Disable();
    }
    private void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        Debug.Log( "OnSceneLoadedMain: " + scene.name + " Mode " + mode );
    }
    private IEnumerator CallToSwitchScenes()
    {
        if (gameEvents && stringData)
        {
            stringData.RuntimeValue = stData;
            yield return new WaitForSeconds( 3f );
            gameEvents.Raise();
            yield return new WaitForSeconds( 3f );
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 1 );
        }
    }
}
