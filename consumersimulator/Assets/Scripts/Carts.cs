public class Carts
{
    private static string nameCart;
    private static bool isInCart;
    public static string cart
    {
        get { return nameCart; }
        set { nameCart = value; }
    }
    public static bool isItemCart
    {
        get { return isInCart; }
        set { isInCart = value; }
    }
}
