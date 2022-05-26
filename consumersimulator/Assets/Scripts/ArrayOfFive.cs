using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayOfFive : MonoBehaviour
{
    public GameObject[] fiveitems; //will attach other objects in Unity to this one
    public Button myButton;
    //public string[] arrayoffive =
    //{
    //    "Mango",
    //    "Pasta",
    //    "Sliced Bread",
    //    "Apple",
    //    "Wine"
    //};
    // Start is called before the first frame update
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
      
    }
    void TaskOnClick()
    {
        for (int i = 0; i < fiveitems.Length; i++)
        {
            int rank = i + 1;
            Console.Write(rank + "." + fiveitems[i]);
        }
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
