using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public GameObject attackHitbox;
    public ParticleSystem particles;

    private Vector2 attackHitboxOffset;
    private Vector2 particleOffset;

    private bool canAttack; //is true when Controller can start a Attack right now

    public float attackTime = 0.5f; //the frequenzy for attacks
    public float attackPush = 5; //the distance controller pushes forward when starting an attack
    public float attackMoveTimout = 0.3f; //the time controller cant move after he finished his attack

    private float attackTimer; //timer for current stuff

    private Controller controller;
    protected Collider2D hitboxCollider;
    protected Vector2 lastDirection;

    void Start() {
        controller = GetComponent<Controller>();

        attackHitboxOffset = attackHitbox.transform.localPosition;
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

    private void attack(Vector2 attackDirection) {
        attackTimer = attackTime; //starting attack

        //Attack Push
        controller.timeoutMovement(attackMoveTimout); //prevents move bevahivour from slowing down the push
        controller.getBody().velocity = attackDirection * attackPush; //inits push

        //calculates rotation
        float angle = Mathf.Atan2(attackDirection.y, -attackDirection.x) - Mathf.PI * .5f;
        Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);

        //rotates hitbox
        attackHitbox.transform.localPosition = pos * attackHitboxOffset.magnitude;
        attackHitbox.transform.eulerAngles = Vector3.forward * angle * Mathf.Rad2Deg;

        //rotating particle System
        particles.transform.localPosition = pos * particleOffset.magnitude;
        particles.transform.eulerAngles = new Vector3(angle * Mathf.Rad2Deg, 90, 0);
        particles.startRotation = angle;

        particles.Emit(1);

    }

    public bool isAttacking() {
        return attackTimer > 0;
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
