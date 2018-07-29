using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public int health = 5;
    public int maxHealth = 5;

    public float invincibleTime = 0.25f;

    private float invincibleTimer;
    private bool invincible;
    public bool alive;

    private SpriteRenderer spriteRenderer;

    void Start () {
        this.invincible = false;
        this.alive = true;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update () {
		
        if(this.invincibleTimer > 0) {
            this.invincibleTimer -= Time.deltaTime;
            Color col = spriteRenderer.color;
            this.spriteRenderer.color = new Color(col.r, col.g, col.b, .2f);
        } else {
            this.invincible = false;
            Color col = spriteRenderer.color;
            this.spriteRenderer.color = new Color(col.r, col.g, col.b, 1);
        }

	}

    private void onDeath() {
        Debug.Log(this.name + " is dead");
        Destroy(this.gameObject);
    }

    public bool dealDamage(int amount) {
        if (this.invincible) {
            return false;
        } else {
            this.health -= amount;
            if (this.health <= 0) {
                this.onDeath();
            }
            this.invincible = true;
            this.invincibleTimer = this.invincibleTime;
            return true;
        }
    }

    

}
