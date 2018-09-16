using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Rigidbody2D body;
    private MoveBehaviour moveBehaviour;
    private AttackBehaviour attackBehaviour;
    private HealthScript health;
    private KnockbackScript knockbackBehaviour;

    void Start () {
        body = GetComponent<Rigidbody2D>();
        moveBehaviour = GetComponent<MoveBehaviour>();
        attackBehaviour = GetComponent<AttackBehaviour>();
        knockbackBehaviour = GetComponent<KnockbackScript>();
        health = GetComponent<HealthScript>();
        init();
	}

    virtual protected void init() {

    }

    public void dealDamage(float amount, Vector2 sourcePosition) {
        health.dealDamage((int)amount);
        knockbackBehaviour.knockback(sourcePosition);
    }
	

    public Rigidbody2D getBody() {
        return body;
    }

    public void timeoutMovement(float time) {
        moveBehaviour.setTimout(time);
    }

    public MoveBehaviour getMoveBehaviour() {
        return moveBehaviour;
    }

    public AttackBehaviour getAttackBehaviour() {
        return attackBehaviour;
    }

}
