using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayOfFive : MonoBehaviour
{
    //public GameObject[] fiveitems; //will attach other objects in Unity to this one
    public Button myButton;
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
          
    
    void TaskOnClick()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
     
        Debug.Log("You clicked the button!");
    }
    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void buttonClick(Button myButton)
    //{
    //    btn.onClick.AddListener(buttonClick);

    //}
}
