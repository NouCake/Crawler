using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour {

    public ParticleSystem particles;

    private Controller controller;
    private Rigidbody2D body;
    private Vector2 knockVel;
    
    private float knockbackTimer;
    private float knockedOutTimer;

	void Start () {
        controller = GetComponent<Controller>();
        this.body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(this.knockbackTimer > 0) {
            this.knockbackTimer -= Time.deltaTime;
            this.body.velocity += this.knockVel * Time.deltaTime;
            if(this.body.velocity.magnitude <= 0.2f) {
                this.body.velocity = Vector2.zero;
            }
            if(this.knockbackTimer <= 0) {
                this.body.velocity = Vector2.zero;
            }
        }
        if (knockedOutTimer > 0) {
            knockedOutTimer -= Time.deltaTime;
            if (knockedOutTimer <= 0) {
                onKnockoutOver();
            }
        }
    }

    private void onKnockout() {
        controller.onKnockout();
    }

    private void onKnockoutOver() {
        controller.onKnockoutOver();
    }

    /**
     * Vector2 direction 
     */
    public void knockback(Vector2 direction, float distance, float time, float timeKnockedOut) {
        knockedOutTimer = timeKnockedOut;
        Vector2 knockback = direction;
        knockback = knockback.normalized * 2 * distance / (time * time);
        this.knockVel = knockback;
        this.body.velocity = -knockback * time;
        this.knockbackTimer = time;

        onKnockout();
        emitParticles(direction);
    }
    
    private void emitParticles(Vector2 direction) {
        if(particles != null) {
            float angle = Mathf.Atan2(direction.y, direction.x);
            particles.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
            particles.Play();
        }
    }

}
