using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public GameObject attackHitbox;
    public ParticleSystem particles;
    
    private Vector2 particleOffset;

    private bool canAttack; //is true when Controller can start a Attack right now

    public float attackResetTime = 0.2f; //Time until controller can init a attack again
    public float attackTime = 0.1f; //duration of a attack
    public float attackPush = 5; //the distance controller pushes forward when starting an attack
    public float attackMoveTimout = 0.3f; //the time controller cant move after he finished his attack

    protected float attackTimer; //timer for current stuff

    protected Controller controller;
    protected Collider2D hitboxCollider;
    protected Vector2 lastDirection;

    void Start() {
        controller = GetComponent<Controller>();


        canAttack = true;
        attackTimer = 0;

        init();
    }

    virtual protected void initHitParticle() {
        hitboxCollider = attackHitbox.GetComponent<Collider2D>();
        particleOffset = particles.transform.localPosition;
    }

    virtual protected void init() {
        initHitParticle();
    }

    // Update is called once per frame
    void Update() {
        if (canAttack) {
            if (attackTimer <= 0) {
                if (checkAttackCondition()) {
                    attackTimer = attackTime + attackResetTime; //starting attack
                    attack(lastDirection);
                }
            } else {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    //When to init a attack
    //example: player uses keyboard
    virtual protected bool checkAttackCondition() {
        Debug.Log("base checking condition");
        return false;
    }

    virtual protected void attack(Vector2 attackDirection) {
        //Attack Push
        initAttackPush(attackDirection);
        //calculates rotation
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) - Mathf.PI * .5f;
        //rotates hitbox
        rotateHitbox(angle);
        rotateParticleSystem(angle);
        particles.Emit(1);
    }

    protected void initAttackPush(Vector2 attackDirection) {
        controller.timeoutMovement(attackMoveTimout); //prevents move bevahivour from slowing down the push
        controller.getBody().velocity = attackDirection * attackPush; //inits push
    }
    
    protected void rotateHitbox(float angle) {
        attackHitbox.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
    }

    protected void rotateParticleSystem(float angle) {
        Vector3 pos = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);
        particles.transform.localPosition = pos * particleOffset.magnitude;
        particles.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        var main = particles.main;
        main.startRotation = -angle;

    }

    //Returns true when this controller can hit other
    virtual protected bool filterTarget(GameObject other) {
        return false;
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (isAttacking() && filterTarget(collision.gameObject)) {
            Controller other = collision.GetComponent<Controller>();
            other.dealDamage(controller.getStats().getStr());
            other.knockback(transform.position - other.transform.position, 1, .3f, .3f);
        }
    }

    public bool isAttacking() {
        //attacktimer while be set to attackResetTime + attack duration
        return attackTimer > attackResetTime;
    }

    public void setCanAttack(bool canAttack) {
        this.canAttack = canAttack;
    }

    protected Controller getController() {
        return controller;
    }

    protected GameObject getAttackHitbox() {
        return attackHitbox;
    }

}
