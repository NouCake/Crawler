using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : AttackBehaviour {

    public float comboChainTime = 0.6f; //Time before combo chain breakes up

    public float comboAttackTime = 0.15f;
    public float comboAttackResetTime = 0.5f;
    public float comboAttackPush = 10;
    public float comboAttackMoveTimout = 0.5f;

    private float normalAttackTime;
    private float normalAttackResetTime;
    private float normalAttackPush;
    private float normalAttackMoveTimout;

    private int comboCounter;
    private float timeSinceLastAttack;

    protected override void init() {
        normalAttackTime = attackTime;
        normalAttackResetTime = attackResetTime;
        normalAttackPush = attackPush;
        normalAttackMoveTimout = attackMoveTimout;

        comboCounter = 0; //max combo = 3, HARDCODED
    }

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

    private void FixedUpdate() {
        //UNCLEAN
        //this line should be in Update, but I dont want it there
        timeSinceLastAttack += Time.deltaTime;
    }

    override protected void attack(Vector2 attackDirection) {
        if(timeSinceLastAttack > comboChainTime) {
            comboCounter = 0;
        }
        if(comboCounter >= 3) {
            comboCounter = 0;
        }
        timeSinceLastAttack = 0;
        comboCounter++;

        if (comboCounter >= 3) {
            attackTime = comboAttackTime;
            attackResetTime = comboAttackResetTime;
            attackPush = comboAttackPush;
            attackMoveTimout = comboAttackMoveTimout;
        } else {

            attackTime = normalAttackTime;
            attackResetTime = normalAttackResetTime;
            attackPush = normalAttackPush;
            attackMoveTimout = normalAttackMoveTimout;
        }

        base.attack(attackDirection);
    }

    protected override bool filterTarget(GameObject other) {
        if (other.tag == "enemy") {
            return true;
        }
        return false;
    }

    new void OnTriggerStay2D(Collider2D collision) {
        base.OnTriggerStay2D(collision);
        if(isAttacking() && filterTarget(collision.gameObject)) {
            UIController.ui.setLastTarget(collision.GetComponent<Controller>());
        }
    }

    public bool isComboAttacking() { //TODO returns if the current attack is a combo attack
        return comboCounter >= 3;
    }

}
