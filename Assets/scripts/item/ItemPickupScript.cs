using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {

    public Item item;

    private SpriteRenderer renderer;
    private BoxCollider collider;

	void Start () {
        this.renderer = GetComponent<SpriteRenderer>();
        this.renderer.sprite = item.sprite;
        this.name = item.name;
	}

    public void pickup() {
        Destroy(this.gameObject);
    }


}
