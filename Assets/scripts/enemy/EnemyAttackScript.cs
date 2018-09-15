using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : AttackBehaviour {

    public float waitBeforeAttack = 0.2f; //time controller waits while player is in range before attack starts
    public float attackChargeTime = 0.3f; //when attack start controller charges attack into certain direction and cant move

    private EnemyController controller;

    private float moveTimer;
    private float distanceToPlayer;
    private bool charging;

    override protected void init() {
        controller = (EnemyController)getController();

        moveTimer = 0;
        charging = false;
        distanceToPlayer = 0;

        Debug.Log("Hello");
        Debug.Log(controller.getMoveBehaviour());
    }

    protected override bool checkAttackCondition() {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.player.transform.position);

        if (charging) {
            if (moveTimer <= 0) {
                moveTimer = waitBeforeAttack;
                charging = false;
                controller.timeoutMovement(attackChargeTime + waitBeforeAttack);
                lastDirection = PlayerController.player.transform.position - transform.position;
                lastDirection = lastDirection.normalized;
                return true;
            } else {
                moveTimer -= Time.deltaTime;
            }
        } else {
            if (distanceToPlayer <= controller.getMoveBehaviour().minDistance) {
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0) {
                    charging = true;
                    moveTimer = attackChargeTime;
                }
            } else {
                moveTimer = waitBeforeAttack;
            }
        }

        return false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (isAttacking() && collision.IsTouching(hitboxCollider)) {
            PlayerController.player.GetComponent<PlayerController>().receiveDamage(1, this.gameObject);
        }
    }

}