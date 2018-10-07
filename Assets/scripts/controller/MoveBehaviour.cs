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
    private Controller controller;


    void Start() {
        body = GetComponent<Rigidbody2D>();
        canMove = true;
        newVel = body.velocity;
        controller = GetComponent<Controller>();
        this.init();
    }

    virtual public void init() {
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
        if (newVel.magnitude >= 1) {
            newVel -= newVel * slowdownFactor * Time.deltaTime;
        } else {
            newVel = Vector2.zero;
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

    public Controller getController() {
        return controller;
    }

}
