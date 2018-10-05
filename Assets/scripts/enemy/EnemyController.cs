using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {
   
    new public EnemyMoveScript getMoveBehaviour() {
        return (EnemyMoveScript)base.getMoveBehaviour();
    }

    override public void onDeath() {
        PlayerController.player.addGold(10);
        PlayerController.player.addXP(1);
        Destroy(gameObject);
    }

}
