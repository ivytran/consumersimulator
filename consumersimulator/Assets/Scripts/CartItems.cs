using System.Collections.Generic;

public class CartItems
{
    private static string itemName;
    private static int matchedCount;
    private static int itemCount;
    private static List<string> allItems = new List<string>();
    private static List<Item> itemsObject = new List<Item>();
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public static string ItemName
    {
        get{ return itemName; }
        set { itemName = value; }
    }
    public static int MatchedCount
    {
        get { return matchedCount; }
        set { matchedCount = value; }
    }
    public static List<Item> ItemCall
    {
        get { return itemsObject; }
    }
    public static List<string> HoldItems{ get { return allItems; }}
    public static void AddItems(string argValue)
    {
        allItems.Add( argValue );
    }
    public static void AddItemsObject(int thisId,string thisName, string thisDesc)
    {
        itemsObject.Add( new Item { Id = thisId , Name = thisName , Description = thisDesc } );
    }
    public static int TotalItems
    {
        get { return itemCount; }
        set { itemCount = value; }
    }
}
