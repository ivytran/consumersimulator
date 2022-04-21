using UnityEngine;

public class FridgeDoorsAnim : MonoBehaviour
{
    [SerializeField] private Animator leftDoor;
    [SerializeField] private Animator rightDoor;

    public void OpenLeftFridgeDoor()
    {
        try
        {
            if (leftDoor)
                leftDoor.SetBool( "isLeftOpen" , true );
                leftDoor.SetBool( "isReset" , false );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void CloseLeftFridgeDoor()
    {
        try
        {
            if (leftDoor)
                leftDoor.SetBool( "isLeftOpen" , false );
                leftDoor.SetBool( "isReset" , true );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void OpenRightFridgeDoor()
    {
        try
        {
            if (rightDoor)
                rightDoor.SetBool( "isRightOpen" , true );
                rightDoor.SetBool( "isReset" , false );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void CloseRightFridgeDoor()
    {
        try
        {
            if (rightDoor)
                rightDoor.SetBool( "isRightOpen" , false );
                rightDoor.SetBool( "isReset" , true );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
