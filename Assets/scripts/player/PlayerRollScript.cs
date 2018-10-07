using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollScript : MonoBehaviour {

    public float rollSpeed = 8;
    public float rollDistance = 2;
    public float rollRecoveryTime = 0.25f;
    public Sprite rollSprite;

    private float rollTimer = 0;
    private float rollRecoveryTimer = 0;
    private bool rolling;
    
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;

    private Sprite defaultSprite;


    void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.controller = GetComponent<PlayerController>();

        this.defaultSprite = this.spriteRenderer.sprite;
        this.rolling = false;

    }
    
    void Update() {
        if (!controller.isInputBlocked()) {
            if (!rolling && rollRecoveryTimer <= 0 && Input.GetKeyDown(KeyCode.Space) && !controller.getMoveBehaviour().isMoveTimeout()) {
                startRolling();
            }
        }

        if (rolling) {
            rollTimer -= Time.deltaTime;
            if (rollTimer < 0) {
                stopRolling();
            }
        }

        if(rollRecoveryTimer > 0) {
            rollRecoveryTimer -= Time.deltaTime;
        }
    }

    private void startRolling() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        controller.getBody().velocity = direction * rollSpeed;
        rollTimer = rollDistance / rollSpeed;
        rolling = true;
        onRollBegin();
    }

    public void stopRolling() {
        rolling = false;
        rollTimer = 0;
        rollRecoveryTimer = rollRecoveryTime;
        onRollEnd();
    }

    private void onRollBegin() {
        controller.getMoveBehaviour().setCanMove(false);
        spriteRenderer.sprite = rollSprite;
    }

    private void onRollEnd() {
        controller.getMoveBehaviour().setCanMove(true);
        spriteRenderer.sprite = defaultSprite;
    }

    public bool isRolling() {
        return rolling;
    }
}
