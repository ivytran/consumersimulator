using UnityEditor;
using UnityEngine;

public class StaticStatus : MonoBehaviour
{

    public void GrabStaticStatSelectStart()
    {
        var start = gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log( "select started" + start );
    }
    public void GrabStaticStatSelectFinish()
    {
        var finsigh = gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log( "select finished" + finsigh );
    }
}
