using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : MonoBehaviour {

    public Inventory inventory;
    public Transform listElement;
    private List<GameObject> renderedElements;
    private Transform listContentContainer;
    public float listElementSpacing = 15;

    public bool showCursor;
    public GameObject cursor;
    private int cursorPosition; 

    void Start() {
        inventory.OnItemChangedCallback += updateRenderer;
        this.listContentContainer = this.transform.Find("List").Find("ListContent");
        this.renderedElements = new List<GameObject>();
        this.cursorPosition = -1;
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

    public void cursorUp() {
        if(this.inventory.getFilledSlots() <= 0) {
            return;
        }
        this.activateCursor();
        if (this.cursorPosition > 0) {
            this.cursorPosition--;
        }
        this.cursor.transform.localPosition = new Vector2(this.cursor.transform.localPosition.x, -listElementSpacing * this.cursorPosition);
    }

    public void cursorDown() {
        if(this.inventory.getFilledSlots() <= 0) {
            return;
        }
        this.activateCursor();
        if(this.cursorPosition+1 < this.inventory.getFilledSlots()) {
            this.cursorPosition++;
        }
        this.cursor.transform.localPosition = new Vector2(this.cursor.transform.localPosition.x, -listElementSpacing * this.cursorPosition);
    }

    private void activateCursor() {
        if (this.showCursor) {
            this.cursor.SetActive(true);
        }
    }

    private GameObject createListItem(Item i, int slot) {
        GameObject element = Instantiate(this.listElement.gameObject, this.listContentContainer);
        element.transform.Find("Slot").GetComponent<Text>().text = (slot+1) + "";
        element.transform.Find("Item Name").GetComponent<Text>().text = i.name;
        element.transform.Find("Value").GetComponent<Text>().text = i.price + "";
        element.transform.localPosition += Vector3.down * slot * this.listElementSpacing;
        return element;
    }

    public Item getSelectecdItem() {
        Debug.Log(this.cursorPosition);
        if(this.cursorPosition == -1) {
            return null;
        }

        Item item = this.inventory.getAt(this.cursorPosition);
        if(item == null) {
            Debug.Log("Error, Cursor position is wrong");
        }
        return item;
    }

}
