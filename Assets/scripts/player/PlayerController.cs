using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public ParticleSystem moveParticles;

    private PlayerMoveBehaviour move;
    private PlayerRollScript rollBehaviour;
    private Inventory inventory;

    //private Rigidbody2D body;
    //private PlayerAttackBehaviour attack;
    //private PlayerStats stats;

    public bool inputBlocked;
    private int gold;

    override protected void init() {
        this.rollBehaviour = GetComponent<PlayerRollScript>();
        this.inventory = GetComponent<Inventory>();
        this.move = (PlayerMoveBehaviour)getMoveBehaviour();
        //this.body = GetComponent<Rigidbody2D>();
        //this.attack = (PlayerAttackBehaviour)getAttackBehaviour();

        setStats(new PlayerStats(this));
        this.inputBlocked = false;
    }

    private void Update() {
        UIController.ui.updateUI();
        getDirectionBehaviour().setDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        if(moveParticles.isPlaying && getBody().velocity.magnitude < 1) {
            moveParticles.Stop();
        } else if(moveParticles.isStopped && getBody().velocity.magnitude > 1) {
            moveParticles.Play();
        }
    }

    public override void onKnockout() {
        rollBehaviour.stopRolling(); //needs to be before base, because canMove is set to true
        base.onKnockout();
        blockInput();
    }

    public override void onKnockoutOver() {
        base.onKnockoutOver();
        unblockInput();
    }

    public override void onDamageReveived() {
        base.onDamageReveived();
        camController.shake(0.1f, 0.1f);
    }

    public override void onDeath() {
        SceneManager.LoadScene("game over");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "terrain") {
            if (rollBehaviour.isRolling()) {
                camController.shake(0.1f, 0.2f);
                move.setTimout(0.1f);
                rollBehaviour.stopRolling();
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

    public void addXP(int xp) {
        getStats().addXP(xp);
    }

    new public PlayerStats getStats() {
        return (PlayerStats)base.getStats();
    }

    public void addGold(int gold) {
        this.gold += gold;
    }

    public int getGold() {
        return gold;
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

    public PlayerRollScript getRollBehaviour() {
        return rollBehaviour;
    }

    new public PlayerMoveBehaviour getMoveBehaviour() {
        return (PlayerMoveBehaviour)base.getMoveBehaviour();
    }

}