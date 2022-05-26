using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayOfFive : MonoBehaviour
{
    public GameObject[] fiveitems; //will attach other objects in Unity to this one
    public string[] arrayoffive =
    {
        "Mango",
        "Pasta",
        "Sliced Bread",
        "Apple",
        "Wine"

    };
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arrayoffive.Length; i++)
        {
            int rank = i + 1;
            Console.Write(rank + "." + arrayoffive[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
