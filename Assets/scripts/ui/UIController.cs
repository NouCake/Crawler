using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public static UIController ui;

    void Awake() {
        if (ui != null) {
            Debug.Log("Too Many UIController");
        }
        ui = this;
    }

    public HealthbarScript playerHealthbar;
    public HealthbarScript targetHealthbar;
    public InventoryRenderer inventoryRenderer;

    void Start() {
        this.playerHealthbar.setTarget(PlayerController.player);
        inventoryRenderer = GetComponentInChildren<InventoryRenderer>(true);
        if(inventoryRenderer == null) {
            Debug.Log("Could not find InventoryRenderer");
        }
    }

    public void setLastTarget(Controller target) {
        this.targetHealthbar.setTarget(target);
    }


}
