using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TeleportationManagement : MonoBehaviour
{
    public InputAction inputAction = null;
    public UnityEvent startTeleportEvent;
    public UnityEvent cancelTeleportEvent;

    private void OnEnable()
    {
        inputAction.Enable();
        //inputAction.action.performed += ctx => Debug.Log( "the action performed" + ctx.ReadValue<Vector2>() );
        inputAction.performed += StartTeleportEvent;
        inputAction.canceled += EndTeleportEvent;
    }

    private void EndTeleportEvent(InputAction.CallbackContext obj)
    {
        Invoke( "CancelTeleport" , 0.1f);
    }
    private void CancelTeleport()
    {
        cancelTeleportEvent.Invoke();
    }
    private void StartTeleportEvent(InputAction.CallbackContext obj)
    {
        startTeleportEvent.Invoke();
    }

    private void OnDisable()
    {
        inputAction.Disable();
        inputAction.performed -= StartTeleportEvent;
        inputAction.canceled -= EndTeleportEvent;
    }


}
