using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : MonoBehaviour {

    public Inventory inventory;
    public GameObject listElement;
    public int listElementOffset = 16; //space between list elements

    private Transform itemContainer;
    private List<GameObject> items;


    void Start () {
        itemContainer = transform.Find("Content");

        items = new List<GameObject>();

        inventory.OnItemChangedCallback += updateInventory;
    }
	
	void Update () {
		
	}

    private void updateInventory() {
        Debug.Log("updateInventory");
        //Clearing List
        for (int i = items.Count - 1; i >= 0; i--) {
            if (items[i] != null) {
                GameObject obj = items[i];
                items.Remove(obj);
                Destroy(obj);
            }
        }

        //Adding List
        for(int i = 0; i < inventory.slots; i++) {
            if(inventory.getAt(i) != null) {
                GameObject element = createListElement(inventory.getAt(i), i);
                items.Add(element);
            }
        }
    }

    private GameObject createListElement(Item item, int slot) {
        GameObject element = Instantiate(listElement, itemContainer);
        element.transform.Find("Item Name").GetComponent<Text>().text = item.name;
        element.transform.localPosition += Vector3.down * slot * listElementOffset;
        return element;
    }

}
