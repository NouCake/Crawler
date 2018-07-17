using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour {

    public HealthScript HealthToDisplay;

    public GameObject healthbar;

    private SpriteRenderer healthbarRenderer;
    private float startScale;
    private Color startColor;
    public Color critColor = new Color(255, 0, 0);

    void Start () {
        this.healthbarRenderer = healthbar.GetComponentInChildren<SpriteRenderer>();
        this.startScale = this.healthbar.transform.localScale.x;
        this.startColor = this.healthbarRenderer.color;
    }
	
	void Update () {
        this.healthbar.transform.localScale = new Vector3(startScale * HealthToDisplay.health / HealthToDisplay.maxHealth, 1, 1);
        this.healthbarRenderer.color = new Color(Mathf.Lerp(this.critColor.r, this.startColor.r, HealthToDisplay.health / (float)HealthToDisplay.maxHealth),
            Mathf.Lerp(this.critColor.g, this.startColor.g, HealthToDisplay.health / (float)HealthToDisplay.maxHealth),
            Mathf.Lerp(this.critColor.b, this.startColor.b, HealthToDisplay.health / (float)HealthToDisplay.maxHealth));
    }
}