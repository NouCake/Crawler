using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollScript : MonoBehaviour {

    public float rollSpeed = 20;
    public float rollDistance = 50;
    public Sprite rollSprite;

    private float rollTimer = 0;
    private bool isRolling;

    private PlayerMoveScript moveScript;
    private PlayerAttackScript attackScript;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;

    private Sprite defaultSprite;


    void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.moveScript = GetComponent<PlayerMoveScript>();
        this.attackScript = GetComponent<PlayerAttackScript>();
        this.controller = GetComponent<PlayerController>();
        this.body = GetComponent<Rigidbody2D>();

        this.defaultSprite = this.spriteRenderer.sprite;
        this.isRolling = false;

    }

    // Update is called once per frame
    void Update() {
        if (!this.controller.isInputBlocked()) {
            if (!this.isRolling && Input.GetKeyDown(KeyCode.Space) && !this.moveScript.isMoveTimeout()) {

                if (this.body.velocity.x == 0 && this.body.velocity.y == 0) {
                    this.body.velocity = this.moveScript.getLastDirection() * this.rollSpeed;
                } else {
                    this.body.velocity = this.body.velocity.normalized * this.rollSpeed;
                }
                this.rollTimer = this.rollDistance / this.rollSpeed;
                this.isRolling = true;
                this.onRollBegin();
            }
        }

        if (this.isRolling) {
            this.rollTimer -= Time.deltaTime;
            if (this.rollTimer < 0) {
                this.isRolling = false;
                this.onRollEnd();
            }
        }
    }

    private void onRollBegin() {
        this.moveScript.setCanMove(false);
        this.attackScript.setCanAttack(false);
        this.spriteRenderer.sprite = rollSprite;
    }

    private void onRollEnd() {
        this.attackScript.setCanAttack(true);
        this.moveScript.setCanMove(true);
        this.spriteRenderer.sprite = defaultSprite;

    }

    public bool getIsRolling() {
        return this.isRolling;
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "enemy") {
            //EnemyController enemy = collision.GetComponent<EnemyController>();
        }
    }

}
