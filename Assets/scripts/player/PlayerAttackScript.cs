using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : AttackBehaviour {

    protected override bool checkAttackCondition() {
        //PlayerController con = (PlayerController)getController();
        if (!((PlayerController)getController()).isInputBlocked()) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                lastDirection = Vector2.left;
                return true;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                lastDirection = Vector2.right;
                return true;
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                lastDirection = Vector2.up;
                return true;
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                lastDirection = Vector2.down;
                return true;
            }
        }
        return false;
    }

    public bool isComboAttacking() { //TODO returns if the current attack is a combo attack
        return false;
    }

}
