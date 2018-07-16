using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {

    public GameObject hitbox;
    private Vector2 hitboxOffset;

    [Header("Attack")]
    public float attackPushForward = 5;
    public float attackTime = 0.1f;
    public float attackMoveTimeout = 0.3f;

    [Header("Combo")]
    public float attackComboTime = 0.2f;
    public float attackComboPushForward = 5;
    public float attackComboMoveTimeout = 0.7f;

    public float attackComboResetTime = 0.3f;
    public float offsetMultyplier = 2.5f;
    public int comboAmount = 3;

    private float attackTimer;
    private int comboCounter;
    private float timeSinceLastHit;

    private bool canAttack;

    private PlayerMoveScript moveScript;
    private Rigidbody2D body;
    public ParticleSystem particle;

    private Vector2 particleOffset;
    private Vector2 particleComboOffset;

    void Start () {
        this.body = GetComponent<Rigidbody2D>();
        this.moveScript = GetComponent<PlayerMoveScript>();

        this.hitboxOffset = this.hitbox.transform.localPosition;
        this.particleOffset = this.particle.transform.localPosition;
        this.particleComboOffset = this.particleOffset * offsetMultyplier;

        this.attackTimer = 0;
        this.timeSinceLastHit = 0;
    }
	
	void Update () {

        if (this.attackTimer <= 0) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.attack(Vector2.left);

            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                this.attack(Vector2.right);
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                this.attack(Vector2.up);
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                this.attack(Vector2.down);
            }
        } else {
            this.attackTimer -= Time.deltaTime;
        }
        this.timeSinceLastHit += Time.deltaTime;
        if(this.timeSinceLastHit >= this.attackComboResetTime) {
            this.comboCounter = 0;
        }

    }

    private void attack(Vector2 direction) {
        this.comboCounter++;
        this.timeSinceLastHit = 0;

        //attacking
        float offset = 0;
        if (this.comboCounter < this.comboAmount) { //non-combo attack
            this.body.velocity = direction * this.attackPushForward;
            this.moveScript.setTimout(this.attackMoveTimeout);
            this.attackTimer = this.attackTime;
            offset = this.particleOffset.magnitude;
        } else { //combo attack
            this.body.velocity = direction * this.attackComboPushForward;
            this.moveScript.setTimout(this.attackComboMoveTimeout);
            this.attackTimer = this.attackComboTime;
            this.comboCounter = 0;
            offset = this.particleComboOffset.magnitude;
        }

        //rotating components
        float angle = Mathf.Atan2(direction.y, -direction.x) - Mathf.PI * .5f;
        
        Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);

        //rotate hitbox
        this.hitbox.transform.localPosition = pos * this.hitboxOffset.magnitude;
        this.hitbox.transform.eulerAngles = Vector3.forward * angle * Mathf.Rad2Deg;

        //setting particle system
        this.particle.transform.localPosition = pos * offset;
        this.particle.transform.eulerAngles = new Vector3(angle * Mathf.Rad2Deg, 90,0);
        this.particle.startRotation = angle;

        this.particle.Emit(1);

    }

    public void setCanAttack(bool canAttack) {
        this.canAttack = canAttack;
    }

    public bool isAttacking() {
        return this.attackTimer > 0;
    }

}
