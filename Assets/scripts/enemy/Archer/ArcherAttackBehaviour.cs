using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackBehaviour : AttackBehaviour {

    public GameObject arrow;
    public int arrowSpeed;
    private EnemyController enemyController;

    override protected void init() {
        enemyController = (EnemyController)getController();
    }

    override protected bool checkAttackCondition() {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.player.transform.position);
        if (distanceToPlayer <= enemyController.getMoveBehaviour().minDistance) {
            lastDirection = PlayerController.player.transform.position - transform.position;
            lastDirection = lastDirection.normalized;
            return true;
        }

        return false;
    }

    override protected void attack(Vector2 attackDirection) {
        GameObject arw = Instantiate(arrow);
        arw.transform.position = transform.position;
        arw.GetComponent<Rigidbody2D>().velocity = attackDirection * arrowSpeed;
        initAttackPush(attackDirection);
    }

    new public void OnTriggerStay2D(Collider2D collision) {

    }
    
}
