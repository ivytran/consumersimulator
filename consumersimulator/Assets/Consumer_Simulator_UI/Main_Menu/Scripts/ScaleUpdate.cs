using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpdate : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.localScale = new Vector3(2,2,2);
    }
}
