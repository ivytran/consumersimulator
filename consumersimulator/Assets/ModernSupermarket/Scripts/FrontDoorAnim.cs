using System.Collections;
using UnityEngine;

public class FrontDoorAnim : MonoBehaviour
{
    [SerializeField] private Animator frontLeftDoor;
    [SerializeField] private Animator frontRightDoor;

    public void OpenLeftFrontDoor()
    {
        try
        {
            if (frontLeftDoor)
                frontLeftDoor.SetBool( "isLeftOpen" , true );
                frontLeftDoor.SetBool( "isReset" , false );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void CloseLeftFrontDoor()
    {
        try
        {
            if (frontLeftDoor)
                frontLeftDoor.SetBool( "isLeftOpen" , false );
                frontLeftDoor.SetBool( "isReset" , true );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void OpenRightFrontDoor()
    {
        try
        {
            if (frontRightDoor)
                frontRightDoor.SetBool( "isRightOpen" , true );
                frontRightDoor.SetBool( "isReset" , false );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public void CloseRightFrontDoor()
    {
        try
        {
            if (frontRightDoor)
                frontRightDoor.SetBool( "isRightOpen" , false );
                frontRightDoor.SetBool( "isReset" , true );
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "Player" ))
        {
            StartCoroutine( OpenCloseFrontDoorCo() );
        }
    }
    private IEnumerator OpenCloseFrontDoorCo()
    {
        if (frontLeftDoor)
        {
            OpenLeftFrontDoor();
            yield return new WaitForSeconds( 10f );
            CloseLeftFrontDoor();
        }
        if (frontRightDoor)
        {
            OpenRightFrontDoor();
            yield return new WaitForSeconds( 10f );
            CloseRightFrontDoor();
        }
        if (frontLeftDoor && frontRightDoor)
        {
            OpenLeftFrontDoor();
            OpenRightFrontDoor();
            yield return new WaitForSeconds( 10f );
            CloseLeftFrontDoor();
            CloseRightFrontDoor();
        }
    }

}
