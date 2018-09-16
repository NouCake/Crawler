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

    private float attackTimer; //timer for current stuff

    private Controller charController;
    protected Collider2D hitboxCollider;
    protected Vector2 lastDirection;

    void Start() {
        charController = GetComponent<Controller>();

        hitboxCollider = attackHitbox.GetComponent<Collider2D>();
        particleOffset = particles.transform.localPosition;

        canAttack = true;
        attackTimer = 0;

        init();
    }

    virtual protected void init() {

    }

    // Update is called once per frame
    void Update() {
        if (canAttack) {
            if (attackTimer <= 0) {
                if (checkAttackCondition()) {
                    attack(lastDirection);
                }
            } else {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    virtual protected bool checkAttackCondition() {
        Debug.Log("base checking condition");
        return false;
    }

    virtual protected void attack(Vector2 attackDirection) {
        attackTimer = attackTime + attackResetTime; //starting attack

        //Attack Push
        charController.timeoutMovement(attackMoveTimout); //prevents move bevahivour from slowing down the push
        charController.getBody().velocity = attackDirection * attackPush; //inits push

        //calculates rotation
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) - Mathf.PI * .5f;
        Vector3 pos = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);

        //rotates hitbox
        attackHitbox.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

        //rotating particle System
        particles.transform.localPosition = pos * particleOffset.magnitude;
        particles.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        var main = particles.main;
        main.startRotation = -angle;

        particles.Emit(1);

    }

    public bool isAttacking() {
        return attackTimer > attackResetTime;
    }

    public void setCanAttack(bool canAttack) {
        this.canAttack = canAttack;
    }

    protected Controller getController() {
        return charController;
    }

    protected GameObject getAttackHitbox() {
        return attackHitbox;
    }

}
