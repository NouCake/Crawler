using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    #region Singleton
    public static PlayerController player;

    void Awake() {
        if (player != null) {
            Debug.Log("Too Many Players!");
        }
        player = this;
    }
    #endregion

    public CameraController camController;
    //private Rigidbody2D body;

    private PlayerAttackScript attack;
    private PlayerMoveScript move;
    private PlayerRollScript rollScript;
    private KnockbackScript knockbackScript;
    private HealthScript healthScript;
    private Inventory inventory;

    public bool inputBlocked;

    override protected void init() {
        //this.body = GetComponent<Rigidbody2D>();
        this.rollScript = GetComponent<PlayerRollScript>();
        this.knockbackScript = GetComponent<KnockbackScript>();
        this.healthScript = GetComponent<HealthScript>();
        this.inventory = GetComponent<Inventory>();

        this.move = (PlayerMoveScript)getMoveBehaviour();
        this.attack = (PlayerAttackScript)getAttackBehaviour();

        this.inputBlocked = false;
    }

    public void receiveDamage(float amount, GameObject who) {
        if (this.healthScript.dealDamage((int)amount)) {
            this.knockbackScript.knockback(who.transform.position);
            camController.shake(0.1f, 0.2f);
        }
    }

    public void heal(float amount) {
        this.healthScript.heal(3);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "terrain") {
            if (this.rollScript.getIsRolling()) {
                camController.shake(0.1f, 0.2f);
                move.setTimout(0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "pickup") {
            ItemPickupScript pickup = collision.gameObject.GetComponent<ItemPickupScript>();
            this.inventory.add(pickup.item);
            pickup.pickup();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //this should be weapon hitbox
        //needs change if multiple trigger collider are added to player
        if (collision.tag == "enemy") {
            if (attack.isAttacking()) {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                enemy.dealDamage(1, this.transform.position);

                if (attack.isComboAttacking()) {
                    camController.shake(0.1f, 0.1f);
                }
                //setting UI Target Healthbar
                UIController.ui.setLastTarget(collision.gameObject);
            }
        }
    }

    public void blockInput() {
        this.inputBlocked = true;
    }

    public void unblockInput() {
        this.inputBlocked = false;
    }

    public bool isInputBlocked() {
        return this.inputBlocked;
    }

}
