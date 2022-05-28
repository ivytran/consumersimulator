using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour
{
    Text textField;
    //string empty;
    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("arrayListTxt").GetComponent<Text>();
    }

    public void clearText()
    {
        textField.text = "";
    }
}
