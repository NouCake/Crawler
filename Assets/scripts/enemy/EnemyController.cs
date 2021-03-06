﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {

    public int vit = 5;
    public int str = 10;
    public int def = 10;

    protected override void init() {
        setStats(new ControllerStats(this, vit, str, def));
    }

    private void Update() {
        if (!getAttackBehaviour().isAttacking()) {
            getDirectionBehaviour().setDirection(PlayerController.player.transform.position - transform.position);
        }
    }

    new public EnemyMoveScript getMoveBehaviour() {
        return (EnemyMoveScript)base.getMoveBehaviour();
    }

    override public void onDeath() {
        PlayerController.player.addGold(10);
        PlayerController.player.addXP(1);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "path collider") {
            Vector2 vel = getBody().velocity;
            getBody().velocity = (transform.position - collision.transform.position) * 0.3f;
            getBody().velocity += vel;
        }
    }

}
