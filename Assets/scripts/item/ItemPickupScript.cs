using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {

    public Item item;

    private SpriteRenderer spriteRenderer;

	void Start () {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = item.sprite;
        this.name = item.name;
	}

    public void pickup() {
        Destroy(this.gameObject);
    }


}
