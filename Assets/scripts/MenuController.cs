using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject MenuUI;

    private PlayerController player;
    private bool inMenu;

	void Start () {
        this.player = PlayerController.player;
        this.inMenu = false;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (this.inMenu) {
                closeMenu();
            } else {
                openMenu();
            }
        }

        if (this.inMenu) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                UIController.ui.inventoryRenderer.cursorDown();
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                UIController.ui.inventoryRenderer.cursorUp();
            } else if (Input.GetKeyDown(KeyCode.Return)) {
                UIController.ui.inventoryRenderer.getSelectecdItem().onUse();
            }
        }
	}

    private void openMenu() {
        this.player.blockInput();
        Time.timeScale = 0;
        this.inMenu = true;
        this.MenuUI.SetActive(true);
    }

    private void closeMenu() {
        this.player.unblockInput();
        Time.timeScale = 1;
        this.inMenu = false;
        this.MenuUI.SetActive(false);
    }

}
