using System.Collections;
using UnityEngine;

public class StaticStatus : MonoBehaviour
{

    private string getCartName;
    private string stringItem = "";
    private int thisLength = 0;
    public void GrabStaticStatSelectStart()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void GrabStaticStatSelectFinish()
    {
        StartCoroutine( ObjectCartCo() );
    }
    public void CartKinematic()
    {
        getCartName = Carts.cart;
        GameObject.Find( getCartName ).GetComponent<Rigidbody>().isKinematic = true;
    }
    public void CartNonKinematic()
    {
        getCartName = Carts.cart;
        GameObject.Find( getCartName ).GetComponent<Rigidbody>().isKinematic = false;
    }
    public void CartParentObject()
    {
        getCartName = Carts.cart;
        Debug.Log( "cartName " + getCartName );
        gameObject.transform.parent = GameObject.Find( getCartName ).transform;
        Carts.isItemCart = true;
        CartItems.ItemName = gameObject.name;
        stringItem = CartItems.ItemName.Split( '_' )[0];
        CartItems.AddItems( stringItem );
        CartItems.TotalItems = CartItems.HoldItems.Count;
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
    private IEnumerator ObjectCartCo()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds( 2f );
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
