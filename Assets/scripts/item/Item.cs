using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Item/Item")]
public class Item : ScriptableObject {
    
    public int itemID = 0;
    new public string name = "default item";
    public int price = 100;
    public Sprite sprite;
    public ItemType type = ItemType.MISC;

}
