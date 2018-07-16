using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CameraController camController;
    private Rigidbody2D body;
    
    private PlayerMoveScript moveScript;
    private PlayerRollScript rollScript;
    private PlayerAttackScript attackScript;
    private KnockbackScript knockbackScript;
    private HealthScript healthScript;

    void Start () {
        this.body = GetComponent<Rigidbody2D>();
        this.moveScript = GetComponent<PlayerMoveScript>();
        this.rollScript = GetComponent<PlayerRollScript>();
        this.attackScript = GetComponent<PlayerAttackScript>();
        this.knockbackScript = GetComponent<KnockbackScript>();
        this.healthScript = GetComponent<HealthScript>();
    }

    void Update() {

    }

    public void doDamage(float amount, GameObject who) {
        if (this.healthScript.dealDamage((int)amount)) {
            this.knockbackScript.knockback(who.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "terrain") {
            if (this.rollScript.getIsRolling()) {
                camController.shake(0.1f, 0.2f);
                this.moveScript.setTimout(0.5f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //this should be weapon hitbox
        //needs change if multiple trigger collider are added to player
        if (collision.tag == "enemy") {
            if (this.attackScript.isAttacking()) {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                enemy.dealDamage(1, this.transform.position);
            }
        }
    }

}
