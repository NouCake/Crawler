using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject follow;
    public float followSpeed = 10;
    public float followDistance = 3;

    private float shakeTimer;
    private float shakeIntensity;
    private Vector3 followOffset;

	void Start () {
        this.shakeTimer = 0;
        followOffset = this.transform.position - follow.transform.position;
	}
	
	void LateUpdate () {
        if(this.follow != null) {
            this.followUpdate();
        }
        this.shakeUpdate();
        this.checkZAxis();
	}

    private void checkZAxis() {
        if(this.transform.position.z != -10) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }
    }

    private void followUpdate() {
        this.transform.position = follow.transform.position + followOffset;
    }

    private void shakeUpdate() {
        
        if(this.shakeTimer > 0) {
            this.shakeTimer -= Time.deltaTime;

            this.transform.position += Random.insideUnitSphere * this.shakeIntensity;
        } else {
            this.shakeTimer = 0;
        }

    }

    public void shake(float time, float intensity) {
        this.shakeTimer = time;
        this.shakeIntensity = intensity;
    }

}
