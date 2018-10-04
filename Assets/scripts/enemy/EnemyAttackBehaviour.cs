using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : AttackBehaviour {

    public float waitBeforeAttack = 0.2f; //time controller waits while player is in range before attack starts
    public float attackChargeTime = 0.3f; //when attack start controller charges attack into certain direction and cant move

    private EnemyController enemyController;

    private float moveTimer;
    private float distanceToPlayer;
    private bool charging;

    override protected void init() {
        enemyController = (EnemyController)getController();

        moveTimer = 0;
        charging = false;
        distanceToPlayer = 0;
    }

    protected override bool checkAttackCondition() {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.player.transform.position);

        if (charging) {
            if (moveTimer <= 0) {
                moveTimer = waitBeforeAttack;
                charging = false;
                return true;
            } else {
                moveTimer -= Time.deltaTime;
            }
        } else {
            if (distanceToPlayer <= enemyController.getMoveBehaviour().minDistance) {
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0) {
                    charging = true;
                    moveTimer = attackChargeTime;
                    lastDirection = PlayerController.player.transform.position - transform.position;
                    lastDirection = lastDirection.normalized;
                    enemyController.timeoutMovement(attackChargeTime);
                }
            } else {
                moveTimer = waitBeforeAttack;
            }
        }

        return false;
    }

    protected override bool filterTarget(GameObject other) {
        if (other.tag == "player") {
            return true;
        }
        return false;
    }

}