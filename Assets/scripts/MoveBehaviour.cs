using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour {

    public float slowdownFactor = 5;
    public float maxSpeed = 2;
    public float speed = 2;

    private bool canMove;
    private float timoutTimer = 0;

    protected Vector2 newVel;

    protected Rigidbody2D body;

    void Start() {
        this.init();
    }

    protected void init() {
        this.body = GetComponent<Rigidbody2D>();
        this.canMove = true;
        this.newVel = this.body.velocity;
    }
	
	void Update () {
        if (this.canMove) {

            this.newVel = this.body.velocity;
            if (this.timoutTimer > 0) {
                //moving is timed out?
                this.timoutTimer -= Time.deltaTime;
            } else {
                this.move();
                //setting maxSpeed
                if (this.newVel.magnitude > this.maxSpeed) {
                    this.newVel = this.newVel.normalized * this.maxSpeed;
                }
            }


            if (!this.hasMoved()) {
                this.slowDown();
            }
            this.body.velocity = newVel;
        }
    }

    private bool hasMoved() {
        bool movedX = this.body.velocity.x != newVel.x;
        bool movedY = this.body.velocity.y != newVel.y;
        return movedX || movedY;
    }

    protected virtual void move() {

    }

    protected virtual void slowDown() {
        if (this.body.velocity.magnitude >= 1) {
            this.body.velocity -= this.body.velocity * slowdownFactor * Time.deltaTime;
        } else {
            this.body.velocity = Vector2.zero;
        }
    }

    public void setTimout(float time) {
        this.timoutTimer = time;
    }

    public bool isMoveTimeout() {
        return this.timoutTimer > 0;
    }

    public void setCanMove(bool canMove) {
        this.canMove = canMove;
    }

}
