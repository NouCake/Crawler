using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {
    
    private KnockbackScript knockback;

    override protected void init () {

    }

    new public EnemyMoveScript getMoveBehaviour() {
        return (EnemyMoveScript)base.getMoveBehaviour();
    }

}
