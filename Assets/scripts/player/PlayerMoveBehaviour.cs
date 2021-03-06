﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBehaviour : MoveBehaviour {

    public float accelSpeed = 20;
    public float clampSpeed = 2;

    private Vector2 lastDirection; //last velocity vector that isnt 0, normalized

    override public void init() {
        this.lastDirection = Vector2.up;
    }

    protected override void move() {
        if (!this.getController().isInputBlocked()) {
            if (Input.GetKey(KeyCode.A)) {
                this.newVel.x -= this.accelSpeed * Time.deltaTime;
                if (this.newVel.x > 0) {
                    this.newVel.x = 0;
                }
            }
            if (Input.GetKey(KeyCode.D)) {
                this.newVel.x += this.accelSpeed * Time.deltaTime;
                if (this.newVel.x < 0) {
                    this.newVel.x = 0;
                }
            }

            if (Input.GetKey(KeyCode.W)) {
                this.newVel.y += this.accelSpeed * Time.deltaTime;
                if (this.newVel.y < 0) {
                    this.newVel.y = 0;
                }
            }
            if (Input.GetKey(KeyCode.S)) {
                this.newVel.y -= this.accelSpeed * Time.deltaTime;
                if (this.newVel.y > 0) {
                    this.newVel.y = 0;
                }
            }
        }

        //setting lastDirection
        if ((this.newVel.x == 0 && this.newVel.y == 0) && (this.body.velocity.x != 0 || this.body.velocity.y != 0)) {
            this.lastDirection = new Vector2(this.body.velocity.x, this.body.velocity.y).normalized;
        }

    }

    override protected void slowDown() {
        bool movedX = this.newVel.x != this.body.velocity.x;
        bool movedY = this.newVel.y != this.body.velocity.y;

        //if player hasn't moved, slow down
        if (!movedX) {
            if (this.newVel.x < -this.clampSpeed) {
                this.newVel.x += this.slowdownFactor * this.accelSpeed * Time.deltaTime;
                if (this.newVel.x >= this.clampSpeed) {
                    this.newVel.x = 0;
                }
            } else if (this.newVel.x > this.clampSpeed) {
                this.newVel.x -= this.slowdownFactor * this.accelSpeed * Time.deltaTime;
                if (this.newVel.x <= this.clampSpeed) {
                    this.newVel.x = 0;
                }
            } else {
                this.newVel.x = 0;
            }
        }

        if (!movedY) {
            if (this.newVel.y < -this.clampSpeed) {
                this.newVel.y += this.slowdownFactor * this.accelSpeed * Time.deltaTime;
                if (this.newVel.y >= this.clampSpeed) {
                    this.newVel.y = 0;
                }
            } else if (this.newVel.y > this.clampSpeed) {
                this.newVel.y -= this.slowdownFactor * this.accelSpeed * Time.deltaTime;
                if (this.newVel.y <= this.clampSpeed) {
                    this.newVel.y = 0;
                }
            } else {
                this.newVel.y = 0;
            }
        }
    }

    public Vector2 getLastDirection() {
        return this.lastDirection;
    }

    new public PlayerController getController() {
        return (PlayerController)base.getController();
    }

}
