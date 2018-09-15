using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Rigidbody2D body;
    private MoveBehaviour move;
    private AttackBehaviour attack;

	void Start () {
        body = GetComponent<Rigidbody2D>();
        move = GetComponent<MoveBehaviour>();
        attack = GetComponent<AttackBehaviour>();
        init();
	}

    virtual protected void init() {

    }
	

    public Rigidbody2D getBody() {
        return body;
    }

    public void timeoutMovement(float time) {
        move.setTimout(time);
    }

    public MoveBehaviour getMoveBehaviour() {
        return move;
    }

    public AttackBehaviour getAttackBehaviour() {
        return attack;
    }

}
