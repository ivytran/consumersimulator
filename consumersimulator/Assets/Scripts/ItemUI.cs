using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public GameObject itemCanvas;
    public void ActivateCanvas()
    {
        if (itemCanvas)
        {
            itemCanvas.SetActive( true );
        }
    }
    public void DiactivateCanvas()
    {
        if (itemCanvas)
        {
            itemCanvas.SetActive( false );
        }
    }
}
