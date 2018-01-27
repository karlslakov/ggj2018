using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;

public class PlayerCollider : PunBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("collide! "+coll.gameObject.name);
		if (coll.gameObject.tag == "Hazard") {
			
		}
	}
}
