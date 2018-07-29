using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : MonoBehaviour {

    public Inventory inventory;
    public Transform listElement;
    private List<GameObject> renderedElements;
    private Transform listContentContainer;

    void Start() {
        inventory.OnItemChangedCallback += updateRenderer;
        this.listContentContainer = this.transform.Find("List").Find("ListContent");
        this.renderedElements = new List<GameObject>();
    }

    void Update() {

    }

    private void updateRenderer() {
        //Clear list
        for(int i = renderedElements.Count-1; i >= 0; i--) {
            if(renderedElements[i] != null) {
                GameObject obj = renderedElements[i];
                renderedElements.Remove(obj);
                Destroy(obj);
            }
        }

        for (int i = 0; i < inventory.slots; i++) {
            if (inventory.getAt(i) != null) {
                Debug.Log("Item " + i);
                this.renderedElements.Add(createListItem(inventory.getAt(i), i));
            }
        }
    }

    private GameObject createListItem(Item i, int slot) {
        GameObject element = Instantiate(this.listElement.gameObject, this.listContentContainer);
        element.transform.Find("Slot").GetComponent<Text>().text = (slot+1) + "";
        element.transform.Find("Item Name").GetComponent<Text>().text = i.name;
        element.transform.Find("Value").GetComponent<Text>().text = i.price + "";
        element.transform.localPosition += Vector3.down * slot * 15;
        return element;
    }

}
