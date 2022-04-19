using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //Door will open when the player gets close to it
    public bool isAuto;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            anim.SetBool ("Open", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            anim.SetBool ("Open", false);
        }
    }
}
