using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private BoxCollider[] myColliders;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           myColliders  = gameObject.GetComponents<BoxCollider>();
           StartCoroutine( ExitDoorCo() );
        }
    }
    private IEnumerator ExitDoorCo()
    {
        foreach (BoxCollider bc in myColliders) bc.enabled = !bc.enabled;
        yield return new WaitForSeconds( 2f );
        foreach (BoxCollider bc in myColliders) bc.enabled = !bc.enabled;
    }
}
