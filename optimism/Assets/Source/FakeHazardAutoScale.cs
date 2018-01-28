using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHazardAutoScale : MonoBehaviour {

	public SpriteRenderer optimistFlowers;

	void Start () {
		SpriteRenderer mySprite = GetComponent<SpriteRenderer> ();
		optimistFlowers.size = mySprite.size;
	}
}
