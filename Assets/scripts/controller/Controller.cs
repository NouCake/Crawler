using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Rigidbody2D body;
    private MoveBehaviour moveBehaviour;
    private AttackBehaviour attackBehaviour;
    private KnockbackScript knockbackBehaviour;
    private DamageBehaviour damageBehaviour;
    private ControllerStats stats;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        moveBehaviour = GetComponent<MoveBehaviour>();
        attackBehaviour = GetComponent<AttackBehaviour>();
        knockbackBehaviour = GetComponent<KnockbackScript>();
        damageBehaviour = GetComponent<DamageBehaviour>();
        init();
    }

    virtual protected void init() {
        stats = new ControllerStats(this);
    }

    //Should be called to deal damage to Controller
    // damage to controller = amount - this.def (but atleast 1)
    public void dealDamage(float amount) {
        damageBehaviour.dealDamage(amount);
    }

    //Runs when Controller dies
    public virtual void onDeath() {
        Debug.Log(tag + " is Dead");
    }

    //Runs when Controller takes damage
    public virtual void onDamageReveived() {
    }

    public void knockback(Vector2 direction, float distance, float time, float timeKnockedOut) {
        knockbackBehaviour.knockback(direction, distance, time, timeKnockedOut);
    }

    public Rigidbody2D getBody() {
        return body;
    }

    public void timeoutMovement(float time) {
        moveBehaviour.setTimout(time);
    }

    public MoveBehaviour getMoveBehaviour() {
        return moveBehaviour;
    }

    public AttackBehaviour getAttackBehaviour() {
        return attackBehaviour;
    }

    public ControllerStats getStats() {
        return stats;
    }

    public KnockbackScript getKnockbackBehaviour() {
        return knockbackBehaviour;
    }

    public void setStats(ControllerStats stats) {
        this.stats = stats;
    }

}
