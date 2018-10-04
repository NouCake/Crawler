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

    //private PlayerAttackBehaviour attack;
    private PlayerMoveScript move;
    private PlayerRollScript rollScript;
    private Inventory inventory;

    public bool inputBlocked;

    override protected void init() {
        //this.body = GetComponent<Rigidbody2D>();
        this.rollScript = GetComponent<PlayerRollScript>();
        this.inventory = GetComponent<Inventory>();

        this.move = (PlayerMoveScript)getMoveBehaviour();
        //this.attack = (PlayerAttackBehaviour)getAttackBehaviour();

        this.inputBlocked = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            getKnockbackBehaviou().knockback(Vector2.zero);
        }
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

    public void heal(int amount) {
        Debug.Log("Player got healed");
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