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
        base.init();
        normalAttackTime = attackTime;
        normalAttackResetTime = attackResetTime;
        normalAttackPush = attackPush;
        normalAttackMoveTimout = attackMoveTimout;

        comboCounter = 0; //max combo = 3, HARDCODED
    }

    public override bool checkAttackCondition() {
        //PlayerController con = (PlayerController)getController();
        if (!((PlayerController)getController()).isInputBlocked()) {
            if (Input.GetButtonDown("Fire1")) {
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                setLastDirection(direction.normalized);
                return true;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                setLastDirection(Vector2.left);
                return true;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                setLastDirection(Vector2.right);
                return true;
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                setLastDirection(Vector2.up);
                return true;
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                setLastDirection(Vector2.down);
                return true;
            }
        }
        return false;
    }

    override public void attackUpdate() {
        timeSinceLastAttack += Time.deltaTime;
    }

    override protected void attack(Vector2 attackDirection) {
        if (true) {
            getController().getRollBehaviour().stopRolling();
        }


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

    new public PlayerController getController() {
        return (PlayerController)base.getController();
    }

}
