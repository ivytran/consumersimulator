using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public TMP_Text textField;
    public void clearText()
    {
        if (textField)
        {
            textField.text = "";
        }
    }
} 
