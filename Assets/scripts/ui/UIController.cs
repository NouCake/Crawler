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

    private CharacterInfoController charInf;

    void Start() {
        this.playerHealthbar.setTarget(PlayerController.player);
        this.charInf = GetComponentInChildren<CharacterInfoController>();
    }

    public void setLastTarget(Controller target) {
        this.targetHealthbar.setTarget(target);
    }

    public void updateUI() {
        charInf.updateStats();
    }


}
