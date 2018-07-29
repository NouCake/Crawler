using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour {

    private HealthScript target;

    private RectTransform healthRect;
    private Image healthbarImage;
    private Text label;

    private float startWidth;
    private Color startColor;
    public Color critColor = new Color(255, 0, 0);

    void Start() {
        this.healthRect = this.transform.Find("Bar").GetComponent<RectTransform>();
        this.healthbarImage = this.transform.Find("Bar").GetComponent<Image>();
        this.startWidth = this.healthRect.sizeDelta.x;
        this.startColor = this.healthbarImage.color;
    }

    void Update() {
        if(this.target != null) {
            this.healthRect.sizeDelta = new Vector2(this.startWidth * target.health / target.maxHealth, this.healthRect.sizeDelta.y);
            this.healthbarImage.color = new Color(Mathf.Lerp(this.critColor.r, this.startColor.r, target.health / (float)target.maxHealth),
                Mathf.Lerp(this.critColor.g, this.startColor.g, target.health / (float)target.maxHealth),
                Mathf.Lerp(this.critColor.b, this.startColor.b, target.health / (float)target.maxHealth));
        } else {
            this.gameObject.SetActive(false);
        }
    }

    public void setTarget(HealthScript target) {
        this.target = target;
        this.label = GetComponentInChildren<Text>();
        this.label.text = this.target.transform.name;
        this.gameObject.SetActive(true);
    }

    public void removeTarget() {
        this.target = null;
        this.gameObject.SetActive(false);
    }

}