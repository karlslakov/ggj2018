using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAutoScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sprite = GetComponentInParent<SpriteRenderer> ();
		transform.localScale = sprite.size;
	}
}
