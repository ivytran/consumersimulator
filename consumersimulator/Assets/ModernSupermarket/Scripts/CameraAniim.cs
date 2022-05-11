using UnityEngine;

public class CameraAniim : MonoBehaviour
{

    public Animator anim;

    public void ChangeCameraSpeedStart()
    {
        anim.speed = 1f;
    }
    public void ChangeCameraSpeedInitial()
    {
        anim.speed = 1f;
    }
    public void ChangeCameraSpeed()
    {
        anim.speed = 0.5f;
    }
    public void EndCameraAnim()
    {
        anim.enabled = false;
    }
}
