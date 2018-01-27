using Photon;
using UnityEngine;

public class PlayerHealth : PunBehaviour {

    RectTransform HealthBar;
    float healthPct = 100;
    Vector2 baseSizeDelta;

    void Start () {
        HealthBar = GameObject.Find("HealthFull").GetComponent<RectTransform>();
        baseSizeDelta = HealthBar.sizeDelta;
	}
	
	void Update () {
        if (Input.GetKeyDown("k"))
            photonView.RPC("Dmg", PhotonTargets.AllBufferedViaServer, 10f);
	}

    [PunRPC]
    public void Dmg(float amount)
    {
        healthPct -= amount;
        HealthBar.sizeDelta = new Vector2(baseSizeDelta.x * healthPct / 100f, baseSizeDelta.y);
    }

}
