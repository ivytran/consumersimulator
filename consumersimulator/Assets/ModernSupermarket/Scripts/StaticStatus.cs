using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaticStatus : MonoBehaviour
{

    private string getCartName;
    private string stringItem = "";
    private int thisLength = 0;
    private GameObject itemTextUI;
    private GameObject itemTextQuantity;
    private ItemUI itemUI;
    public void GrabStaticStatSelectStart()
    {
        itemUI = FindObjectOfType<ItemUI>();
        //GameObject.Find( "ItemCanvas" );
        itemUI.ActivateCanvas();
        itemTextUI = GameObject.Find( "ItemTxt" );
        itemTextQuantity = GameObject.Find( "QuantityTxt" );
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (itemUI && itemTextQuantity)
        {
            itemTextUI.GetComponent<Text>().text = "Item";
            itemTextQuantity.GetComponent<Text>().text = "Quantity";
        }
    }
    public void GrabStaticStatSelectFinish()
    {
        StartCoroutine( ObjectCartCo() );
    }
    public void CartKinematic()
    {
        getCartName = Carts.cart;
        if(getCartName != null)
            GameObject.Find( getCartName ).GetComponent<Rigidbody>().isKinematic = true;
    }
    public void CartNonKinematic()
    {
        getCartName = Carts.cart;
        if(getCartName != null)
            GameObject.Find( getCartName ).GetComponent<Rigidbody>().isKinematic = false;
    }
    public void CartParentObject()
    {
        itemTextUI = GameObject.Find( "ItemTxt" );
        itemTextQuantity = GameObject.Find( "QuantityTxt" );
        getCartName = Carts.cart;
        if (getCartName != null)
        {
            Debug.Log( "cartName " + getCartName );
            gameObject.transform.parent = GameObject.Find( getCartName ).transform;
            Carts.isItemCart = true;
            CartItems.ItemName = gameObject.name;
            stringItem = CartItems.ItemName.Split( '_' )[0];
            CartItems.AddItems( stringItem );
            CartItems.TotalItems = CartItems.HoldItems.Count;
            if (itemTextUI && itemTextQuantity)
            {
                itemTextUI.GetComponent<Text>().text = stringItem;
                itemTextQuantity.GetComponent<Text>().text = "1";
            }
            //items Objects
            if (CartItems.ItemCall.Count == 0)
            {
                thisLength = 1;
            }
            else
            {
                thisLength = CartItems.ItemCall.Count;
            }
            for (int i = 0; i < thisLength; i++)
            {
                CartItems.AddItemsObject( i , stringItem , $"{stringItem} Desc" );
                Debug.Log( "itemsObject " + CartItems.ItemCall[i].Name + CartItems.ItemCall[i].Id + CartItems.ItemCall[i].Description );
            }
        }
        if (itemTextUI && itemTextQuantity)
        {
            itemTextUI.GetComponent<Text>().text = gameObject.name.Split( '_' )[0];
            itemTextQuantity.GetComponent<Text>().text = "1";
        }

        }
    private IEnumerator ObjectCartCo()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds( 2f );
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
