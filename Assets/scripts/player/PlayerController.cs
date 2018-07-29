using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
    private Rigidbody2D body;

    private PlayerMoveScript moveScript;
    private PlayerRollScript rollScript;
    private PlayerAttackScript attackScript;
    private KnockbackScript knockbackScript;
    private HealthScript healthScript;
    private Inventory inventory;

    public Item i;

    void Start() {
        this.body = GetComponent<Rigidbody2D>();
        this.moveScript = GetComponent<PlayerMoveScript>();
        this.rollScript = GetComponent<PlayerRollScript>();
        this.attackScript = GetComponent<PlayerAttackScript>();
        this.knockbackScript = GetComponent<KnockbackScript>();
        this.healthScript = GetComponent<HealthScript>();
        this.inventory = GetComponent<Inventory>();

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            InventoryRenderer invRen = UIController.ui.inventoryRenderer;
            invRen.gameObject.SetActive(!invRen.gameObject.activeSelf);
            //Debug.Log(invRen);
            //UIController.ui.inventoryRenderer.gameObject.SetActive(!UIController.ui.inventoryRenderer.gameObject.activeSelf);
        }
    }

    public void doDamage(float amount, GameObject who) {
        if (this.healthScript.dealDamage((int)amount)) {
            this.knockbackScript.knockback(who.transform.position);
            camController.shake(0.1f, 0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "terrain") {
            if (this.rollScript.getIsRolling()) {
                camController.shake(0.1f, 0.2f);
                this.moveScript.setTimout(0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("hello");
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
            if (this.attackScript.isAttacking()) {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                enemy.dealDamage(1, this.transform.position);
                camController.shake(0.05f, 0.05f);

                //setting UI Target Healthbar
                UIController.ui.setLastTarget(collision.gameObject);
            }
        }
    }

}
