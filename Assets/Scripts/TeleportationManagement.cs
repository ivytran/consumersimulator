using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TeleportationManagement : MonoBehaviour
{
    [SerializeField] InputActionReference inputAction;
    public UnityEvent startTeleportEvent;
    public UnityEvent cancelTeleportEvent;

    private void OnEnable()
    {
        inputAction.action.Enable();
        //inputAction.action.performed += ctx => Debug.Log( "the action performed" + ctx.ReadValue<Vector2>() );
        inputAction.action.performed += StartTeleportEvent;
        inputAction.action.canceled += EndTeleportEvent;
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
        inputAction.action.Disable();
        inputAction.action.performed -= StartTeleportEvent;
        inputAction.action.canceled -= EndTeleportEvent;
    }


}
