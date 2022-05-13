using UnityEngine;

public class MainManuUI : MonoBehaviour
{
    Animator CameraObject;
    public GameObject playMenu;
    public GameObject extrasMenu;
    public GameObject exitMenu;
    public GameObject mainMenu;
    public AudioSource hoverSound;
    void Start()
    {
        CameraObject = transform.GetComponent<Animator>();

    }
    public void ReturnMenu()
    {
        playMenu.SetActive( false );
        if (extrasMenu) extrasMenu.SetActive( false );
        exitMenu.SetActive( false );
        mainMenu.SetActive( true );
    }
    public void Position2()
    {
        DisablePlayCampaign();
        CameraObject.SetFloat( "Animate" , 1 );
    }
    public void Position1()
    {
        CameraObject.SetFloat( "Animate" , 0 );
    }
    public void PlayHover()
    {
        hoverSound.Play();
    }
    public void DisablePlayCampaign()
    {
        playMenu.SetActive( false );
    }

}
