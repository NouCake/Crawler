using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Item/Item")]
public class Item : ScriptableObject {
    
    public int itemID = 0;
    new public string name = "default item";
    public string description = "this is default item description";
    public int price = 100;
    public Sprite sprite;
    public ItemType type = ItemType.MISC;
    public bool stackable = false;

    public void onUse() {
        PlayerController.player.heal(3);
        Debug.Log("Item was used");
    }

}
