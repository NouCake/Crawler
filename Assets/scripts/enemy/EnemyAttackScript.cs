using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour {

    public float waitBeforeAttack = 0.2f;
    public float attackChargeTime = 0.3f;
    public float attackWaitTime = 0.5f;
    public float attackPushforward = 5;

    public Collider2D attackHitbox;
    public ParticleSystem particle;

    private float timer;
    private bool attacking;
    private int state;
    /* STATES 0:out-of-range  1:wait-before-attack  2:charging 3:attacking */
    private Vector2 attackDir;
    private Vector2 particleOffset;
    private Color col;

    private GameObject player;
    private EnemyMoveScript move;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    void Start () {
        this.body = GetComponent<Rigidbody2D>();
        this.player = GameObject.FindGameObjectWithTag("player");
        this.move = GetComponent<EnemyMoveScript>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.particleOffset = this.particle.transform.localPosition;
        this.attacking = false;
        this.state = 0;
        this.timer = 0;
    }
	
	void Update () {
        float distance = Vector3.Distance(this.transform.position, this.player.transform.position);
        switch (this.state) {
            case 0: //out-of-range
                this.col = Color.red;
                if (distance <= this.move.minDistance) {
                    this.setState(1);
                }
                break;

            case 1: //wait-before-attack
                this.col = Color.blue;
                if (distance > this.move.minDistance) {
                    this.setState(0);
                } else {
                    this.timer += Time.deltaTime;
                    if (this.timer >= this.waitBeforeAttack) {
                        this.setState(2);
                        this.attackDir = this.player.transform.position - this.transform.position;
                        attackHitbox.transform.localEulerAngles = Vector3.forward * Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
                    }
                }
                break;

            case 2: //charging
                this.col = Color.green;
                this.timer += Time.deltaTime;
                if(this.timer >= this.attackChargeTime) {
                    this.setState(3);
                    this.move.setCanMove(false);
                    this.body.velocity = this.attackDir.normalized * this.attackPushforward;
                    this.attacking = true;
                    //this.move.setTimout(this.attackWaitTime);

                    //setting up particle System
                    float angle = Mathf.Atan2(this.attackDir.y, -this.attackDir.x) - Mathf.PI * .5f;
                    Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                    this.particle.transform.localPosition = pos * this.particleOffset.magnitude;
                    this.particle.transform.eulerAngles = new Vector3(angle * Mathf.Rad2Deg, 90, 0);
                    this.particle.startRotation = angle;
                    this.particle.Emit(1);
                }
                break;

            case 3: //attacking
                this.col = Color.yellow;
                this.timer += Time.deltaTime;
                this.body.velocity -= this.body.velocity * this.move.slowdownFactor * Time.deltaTime;
                if (this.timer >= this.attackWaitTime) {
                    this.setState(0);
                    this.move.setCanMove(true);
                    this.attacking = false;
                }
                break;
        }

        this.spriteRenderer.color = this.col;
	}

    private void setState(int state) {
        this.state = state;
        this.timer = 0;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (this.attacking && collision.IsTouching(this.attackHitbox)) {
            player.GetComponent<PlayerController>().doDamage(1, this.gameObject);
        }
    }

}
