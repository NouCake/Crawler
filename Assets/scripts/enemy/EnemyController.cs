using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {

    private HealthScript health;
    private KnockbackScript knockback;

    override protected void init () {
        this.health = GetComponent<HealthScript>();
        this.knockback = GetComponent<KnockbackScript>();
    }

    public void dealDamage(float attackVal, Vector3 causeLocation) {
        if (this.health.dealDamage((int)attackVal)) {
            this.knockback.knockback(causeLocation);
        }

    }

    new public EnemyMoveScript getMoveBehaviour() {
        return (EnemyMoveScript)base.getMoveBehaviour();
    }

}
