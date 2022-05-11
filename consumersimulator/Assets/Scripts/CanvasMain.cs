using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMain : MonoBehaviour
{
    private bool isUnloaded = false;
    private bool isThisLoaded = false;
    Scene sceneOne;
    Scene sceneTwo;
    int thisLoadIndex;
    int thisUnLoadIndex;
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
        Debug.Log( "LoadThisScene " + UnLoadedScene() );
        SceneManager.LoadSceneAsync( UnLoadedScene() );
        //}           
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    //  SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        Debug.Log( "OnSceneLoaded: " + scene.name + " Mode " + mode);
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
}
