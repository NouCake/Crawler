using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour {

    public float timeInvicibleAfterHit = 0.5f;

    private Controller controller;
    private SpriteRenderer render;
    //time controller is invincible after receiving damage
    private float invincibleTimer;
    private Color colorNormal;
    private Color colorInvince;

    void Start() {
        controller = GetComponent<Controller>();  
        render = GetComponent<SpriteRenderer>();
        colorNormal = render.color;
        colorInvince = new Color(0, 0, 0, 0.5f);
    }

    void Update () {
		if(invincibleTimer > 0) {
            invincibleTimer -= Time.deltaTime;
            render.color = colorInvince;
        } else {
            render.color = colorNormal;
        }
	}

    public void dealDamage(float amount) {
        if (!isInvincible()) {
            int damage = (int)amount - controller.getStats().getDef();
            if (damage < 1) damage = 1;
            controller.getStats().setCurHP(controller.getStats().getCurHP() - damage);
            controller.onDamageReveived();
            invincibleTimer = timeInvicibleAfterHit;
        }
    }

    public void setInvincible(float time) {
        this.invincibleTimer = time;
    }

    public bool isInvincible() {
        return invincibleTimer > 0;
    }

}

/*using System.Collections;
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

    public void heal(int amount) {
        this.health += amount;
        if(this.health >= this.maxHealth) {
            this.health = this.maxHealth;
        }
    }

}*/
