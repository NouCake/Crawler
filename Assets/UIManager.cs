using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject DamageNumber;
    private Camera cam;

	void Start () {
        this.cam = Camera.main;
        float height = this.cam.orthographicSize * 2f;
        float width = height * this.cam.aspect;
        this.transform.localPosition = new Vector2(-width * 0.5f, height * 0.5f);
    }

    public void displayDamage(int amount, Vector3 pos, Color col) {
        GameObject go = Instantiate(DamageNumber, pos, this.transform.rotation);
        TMPro.TextMeshProUGUI txt = go.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        txt.text = amount.ToString();
        txt.color = col;
    }

}
