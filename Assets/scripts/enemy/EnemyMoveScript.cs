using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MoveBehaviour {

    public float minDistance = 0.75f;
    public float maxDistance = 4.5f;

    private GameObject player;
    
    override public void init() {
        this.player = GameObject.FindGameObjectWithTag("player");
    }

    protected override void move() {

        float distance = Vector3.Distance(this.transform.position, this.player.transform.position);
        if (distance >= minDistance && distance <= maxDistance) {
            Vector2 tmp = this.player.transform.position - this.transform.position;
            tmp.Normalize();
            tmp *= this.speed;
            this.newVel += tmp;
        }

    }

}
