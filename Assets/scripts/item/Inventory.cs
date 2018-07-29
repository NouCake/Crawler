using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public int slots;
    public Item[] items;
    private int filledSlots;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    void Start() {
        this.items = new Item[slots];
        this.filledSlots = 0;
    }

    public virtual bool add(Item item) {

        if(filledSlots >= slots) {
            //Inventory is full
            return false;
        }

        int slot = getNextFreeSlot();
        this.items[slot] = item;

        this.filledSlots++;
        if (this.OnItemChangedCallback != null) {
            this.OnItemChangedCallback.Invoke();
        }
        return true;
    }
    
    private int getNextFreeSlot() {
        for (int i = 0; i < slots; i++) {
            if(this.items[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public virtual bool remove(Item item) {
        for(int i = 0; i < this.slots; i++) {
            if(this.items[i] == item) {
                this.items[i] = null;
                if (this.OnItemChangedCallback != null) {
                    this.OnItemChangedCallback.Invoke();
                }
                return true;
            }
        }

        //No such Item in Inventory
        return false;
    }

    public int getFilledSlots() {
        return this.filledSlots;
    }

    protected bool setAt(int index, Item item) {
        this.items[index] = item;
        return true;
    }

    public Item getAt(int index) {
        return this.items[index];
    }

}
