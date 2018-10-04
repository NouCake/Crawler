using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour {

    public float knockbackDistance = 5; //The distance the Object should be knocked back
    public float knockbackTime = .5f; // time to travel the Knockback Distance
    public float timeKnockedOut = 0.3f; //the time that is waited after knockout to move again

    private Rigidbody2D body;
    private MoveBehaviour move;
    private Vector2 knockVel;

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

    public void knockback(Vector3 causedBy) {
        Vector2 knockback = causedBy - this.transform.position;
        knockback = knockback.normalized * 2 * this.knockbackDistance / (this.knockbackTime * this.knockbackTime);
        this.knockVel = knockback;
        this.body.velocity = -knockback * this.knockbackTime;
        this.timer = this.knockbackTime;
        this.move.setCanMove(false);
    }

}
