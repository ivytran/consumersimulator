using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrayOfFive : MonoBehaviour
{       
    public Button myButton;
    public TMP_Text textField;
    private string[] arrayoffive =
    {"Banana","Pasta","Sliced Bread","Apple","Wine","1","2","3","4","5","6","7",
        "8","9","10","11","12","13","14","15","16","17","18","19","20","21","22",
        "23","24","25"
    };
    void Start()
    {
        RandmeItems();
    }
    private void RandmeItems()
    {
        if (textField)
        {
            List<string> listNumbers = new List<string>();
            string pickItem;
            for (int i = 0; i < 5; i++)
            {
                do
                {
                    pickItem = arrayoffive[new System.Random().Next( 0 , arrayoffive.Length)];
                } while (listNumbers.Contains( pickItem ));
                listNumbers.Add( pickItem );
            }
            if(listNumbers.Count == 5)
            {
                foreach (var item in listNumbers)
                {
                    textField.text += item + "\n";
                }
            }
        }    
    }
    public void TaskOnClick()
    {
        Debug.Log( "You clicked the button!" );
    }

}
