using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBehaviour : MonoBehaviour {

    private SpriteRenderer renderer;

    private float blinkTime = 0.5f;
    private float blinkTimer;

    private Color defaultColor;
    public Color attackColor = new Color(1, 0, 0);
    private bool locked;

	void Start () {
        renderer = GetComponentInChildren<SpriteRenderer>();
        defaultColor = renderer.color;
    }

    void Update() {
        if(blinkTimer > 0) {
            blinkTimer -= Time.deltaTime;
        } else {
            renderer.color = defaultColor;
        }
    }

    public void setDirection(float angle) {
        if (!locked) {
            transform.localEulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        }
    }

    public void startAttackBlink() {
        blinkTimer = blinkTime;
        renderer.color = attackColor;
    }

    public void setDirection(Vector2 direction) {
        setDirection(Mathf.Atan2(direction.y, direction.x) - Mathf.PI * .5f);
    }

    public void lockDirection() {
        locked = true;
    }

    public void unlockDirection() {
        locked = false;
    }

}
