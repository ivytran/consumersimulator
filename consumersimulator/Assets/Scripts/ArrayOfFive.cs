using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrayOfFive : MonoBehaviour
{       
    public Button myButton;
    public TMP_Text textField;
    
    private Timer timer;
    private List<string> arrayoffive;
    public List<string> listNumbers; 
    void Start()
    {
        arrayoffive = new List<string>
            {"Banana","Pasta","SlicedBread","Apple","Wine","Shampoo","Book","Cake","Coffee","Cereals","Sugar","ChocolateBox",
                "Chips","Rice","Milk","Sauce","Pepper","Bleach","Cheese","Pizza","Artichoke","Ham","PetFood","Teabox","Vase","Sushis","Garlic",
                "Oil","Salt","Paintings"
            };//MashPotatoe - Flour
        listNumbers = new List<string>();
        timer = FindObjectOfType<Timer>();
        RandmeItems();
    }
    private void RandmeItems()
    {
        if (textField )
        {
            
            string pickItem;
            //if (!mainItemsUi.activeSelf)
            //{
            //    mainItemsUi.SetActive( true );
            //}
            for (int i = 0; i < 5; i++)
            {
                do
                {
                    pickItem = arrayoffive[new System.Random().Next( 0 , arrayoffive.Count)];
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
        if (timer)
        {
          DeactivateItemMenu();
        }
    }
    private void DeactivateItemMenu()
    {
        StartCoroutine( timer.GameStartDelay() ); 
    }
  
}