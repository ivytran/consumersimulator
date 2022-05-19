using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvents Event;
    public UnityEvent Response;
    private void OnEnable()
    {
        if (Event)
        {
            Event.RegisterListener( this );
        }
    }

    private void OnDisable()
    { Event.UnregisterListener( this ); }

    public void OnEventRaised()
    {     
        Response.Invoke();
        Event.isRuntimeEventCalled = true;
        Debug.Log( "eventRaised.." ); 
    }
}
