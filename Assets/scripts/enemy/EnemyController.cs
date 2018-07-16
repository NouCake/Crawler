using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private HealthScript health;
    private Rigidbody2D body;

    private KnockbackScript knockback;
    private MoveBehaviour moveScript;

    void Start () {
        this.body = GetComponent<Rigidbody2D>();
        this.health = GetComponent<HealthScript>();
        this.knockback = GetComponent<KnockbackScript>();
        this.moveScript = GetComponent<MoveBehaviour>();
    }

	void Update () {

	}

    public void dealDamage(float attackVal, Vector3 causeLocation) {
        if (this.health.dealDamage((int)attackVal)) {
            this.knockback.knockback(causeLocation);
        }

    }

}
