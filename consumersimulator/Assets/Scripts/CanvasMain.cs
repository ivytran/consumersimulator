using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMain : MonoBehaviour
{
    public StringData stringData;
    public GameEvents gameEvents;
    public TMP_Text changeTestText;
    private bool isUnloaded = false;
    private bool isThisLoaded = false;
    Scene sceneOne;
    Scene sceneTwo;
    int thisLoadIndex;
    int thisUnLoadIndex;
    int playerScore = 0;

    public void StartNewScene()
    {
        sceneOne = CurrentScene( "Consumer_Simulator_Main_Menu_Scene" );
        sceneTwo = CurrentScene( "Supermarket" );
       
        //if (LoadedScene() != -1)
        //{
        //    SceneManager.UnloadSceneAsync( LoadedScene() );
        //    isUnloaded = true;
        //}
        //if (isUnloaded)
        //{
        SceneManager.LoadScene( UnLoadedScene() );
        //}           
    }
    public void GetData()
    {
        if (gameEvents)
        {
            if (gameEvents.isRuntimeEventCalled)
            {
                changeTestText.text = stringData.RuntimeValue;
                gameEvents.isRuntimeEventCalled = false;
            }
        }
        //PlayerPrefs.GetString( "trData" ) 
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    //  SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        Debug.Log( "OnSceneLoadedMenu: " + scene.name + " Mode " + mode);
        isThisLoaded = true;
    }

    private void OnSceneUnloaded(Scene current)
    {
        Debug.Log( "OnSceneUnloaded: " + current );
    }
    private Scene CurrentScene(string thisName)
    {
        return SceneManager.GetSceneByName( thisName);
    }
    private int LoadedScene()
    {
        try
        {
            if (sceneOne.name != null || sceneTwo.name != null)
            {
                if (sceneOne.name == null)
                {

                    if (sceneTwo.isLoaded)
                    {
                        thisLoadIndex = 1;
                        return thisLoadIndex;
                    }
                }
                else if (sceneTwo.name == null)
                {
                    if (sceneOne.isLoaded)
                    {
                        thisLoadIndex = 0;
                        return thisLoadIndex;
                    }
                }
                else
                {
                    thisLoadIndex = -1;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
      
        return thisLoadIndex;     
    }
    private int UnLoadedScene()
    {
        if (sceneOne.name != null || sceneTwo.name != null)
        {
            if (sceneOne.name == null)
            {
                if (sceneTwo.isLoaded)
                {
                    thisUnLoadIndex = 0;
                    return thisUnLoadIndex;
                }
            }
            else if (sceneTwo.name == null)
            {
                if (sceneOne.isLoaded)
                {
                    thisUnLoadIndex = 1;
                    return thisUnLoadIndex;
                }
            }
            else
            {
                thisUnLoadIndex = -1;
            }
        }
        return thisUnLoadIndex;
    }
    private void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
    private void SaveGameData()
    {
        //create player score class
        PlayerPrefs.SetInt( "SavedScore" , 6 );
        PlayerPrefs.Save();
    }
    private void ContinueGame()
    {
        if (PlayerPrefs.HasKey( "SavedScore" ))
        {
           playerScore = PlayerPrefs.GetInt( "SavedScore" );
        }
    }
    private void ChooseRightController()
    {
        //List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);
        //foreach (var item in devices)
        //{
        //    Debug.Log(item.characteristics);
        //}
        CharacterController cc = gameObject.GetComponent<CharacterController>();
        
        cc.enabled = false;
    }
    private void OnDestroy()
    {
        Debug.Log( "MenuSceneDestroyed..." );
    }
}
