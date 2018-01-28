using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : PunBehaviour {

    RectTransform HealthBar;
    float healthPct = 100;
    Vector2 baseSizeDelta;

    void Start () {
        HealthBar = GameObject.Find("HealthFull").GetComponent<RectTransform>();
        baseSizeDelta = HealthBar.sizeDelta;
	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("collide! "+coll.gameObject.name);
		if (coll.gameObject.tag == "Hazard") {
			//Player takes damage or whatever
		}

	}
	
	void Update () {
        if (Input.GetKeyDown("k"))
            photonView.RPC("Dmg", PhotonTargets.AllBufferedViaServer, 10f);
        if (Input.GetKeyDown("p"))
            PhotonNetwork.InstantiateSceneObject("ShadowMonster", Vector3.zero, Quaternion.identity, 0, null);
    }

    [PunRPC]
    public void Dmg(float amount)
    {
        healthPct -= amount;
        HealthBar.sizeDelta = new Vector2(baseSizeDelta.x * healthPct / 100f, baseSizeDelta.y);
        if (isDead())
        {
            GetComponent<PartialPlayerController>().Die();
        }
    }

    public bool isDead()
    {
        return healthPct <= 0;
    }

}
