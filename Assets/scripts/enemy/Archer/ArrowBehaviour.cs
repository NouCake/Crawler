using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    private int damage;

	void Start () {
        damage = 1;
	}
	
	void Update () {
		if(50 <= Vector3.Distance(transform.position, PlayerController.player.transform.position)) {
            Destroy(this);
        }
	}

    private void OnTriggerStay2D(Collider2D collision) {
        Debug.Log("Col");
        if(collision.transform.tag == "terrain") {
            Destroy(gameObject);
        } else if(collision.transform.tag == "player"){
            PlayerController.player.dealDamage(damage);
            Destroy(gameObject);
        }
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

}
