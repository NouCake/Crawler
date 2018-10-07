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
        base.init();
        enemyController = (EnemyController)getController();

        moveTimer = 0;
        charging = false;
        distanceToPlayer = 0;
    }

    public override bool checkAttackCondition() {
        //Inits a Attack when player is long enough in range

        //Checks if enemy is already charging
        if (charging) {
            //moveTimer = attackChargeTime
            if (moveTimer <= 0) {
                moveTimer = waitBeforeAttack;
                charging = false;
                return true;
            } else {
                moveTimer -= Time.deltaTime;
            }
        } else {
            //checks distance to player
            distanceToPlayer = Vector3.Distance(transform.position, PlayerController.player.transform.position);
            if (distanceToPlayer <= enemyController.getMoveBehaviour().minDistance) { //player is in range
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0) { //moveTimer = waitBeforeAttack
                    //activating charging
                    startCharging();
                }
            } else {
                moveTimer = waitBeforeAttack;
            }
        }
        return false;
    }

    public override void onAttackEnd() {
        getController().getDirectionBehaviour().unlockDirection();
    }

    private void startCharging() {
        charging = true;
        moveTimer = attackChargeTime;
        Vector2 direction = PlayerController.player.transform.position - transform.position;
        setLastDirection(direction.normalized);
        enemyController.timeoutMovement(attackChargeTime);
        DirectionBehaviour dirBehav = getController().getDirectionBehaviour();
        dirBehav.setDirection(direction);
        dirBehav.lockDirection();
        dirBehav.startAttackBlink();
    }

    protected override bool filterTarget(GameObject other) {
        if (other.tag == "player") {
            return true;
        }
        return false;
    }

}