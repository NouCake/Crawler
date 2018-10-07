using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    private Rigidbody2D body;

    private int damage;
    private float lifespan;

	void Start () {
        damage = 1;
        body = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        lifespan -= Time.deltaTime;
		if(lifespan <= 0) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.transform.tag == "terrain") {
            Destroy(gameObject);
        } else if(collision.transform.tag == "player"){
            PlayerController.player.dealDamage(damage);
            Destroy(gameObject);
        }
    }
    
    public void shoot(Vector2 direction, float speed) {
        transform.localEulerAngles = Vector3.forward * Mathf.Atan2(direction.y, direction.x);
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        lifespan = 5;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

}
