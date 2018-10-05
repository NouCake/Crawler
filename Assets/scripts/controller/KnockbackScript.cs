using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour {
    
    private Rigidbody2D body;
    private MoveBehaviour move;
    private Vector2 knockVel;

    private float timeKnockedOut;
    private float timer;

	void Start () {
        this.body = GetComponent<Rigidbody2D>();
        this.move = GetComponent<MoveBehaviour>();
    }

    private void Update() {
        if(this.timer > 0) {
            this.timer -= Time.deltaTime;
            this.body.velocity += this.knockVel * Time.deltaTime;
            if(this.body.velocity.magnitude <= 0.2f) {
                this.body.velocity = Vector2.zero;
            }
            if(this.timer <= 0) {
                this.body.velocity = Vector2.zero;
                this.move.setCanMove(true);
                this.move.setTimout(this.timeKnockedOut);
            }
        }
    }

    /**
     * Vector2 direction 
     */
    public void knockback(Vector2 direction, float distance, float time, float timeKnockedOut) {
        this.timeKnockedOut = timeKnockedOut;
        Vector2 knockback = direction;
        knockback = knockback.normalized * 2 * distance / (time * time);
        this.knockVel = knockback;
        this.body.velocity = -knockback * time;
        this.timer = time;
        this.move.setCanMove(false);
    }
    

}
