using Photon;
using UnityEngine;

public class PartialPlayerController : PunBehaviour {

	public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	
    void Update () {
        Vector3 move = Vector3.zero;
		move += new Vector3(speed * Time.deltaTime * Input.GetAxisRaw ("Horizontal"), 0.0f, 0.0f);
		move += new Vector3(0.0f, speed * Time.deltaTime * Input.GetAxisRaw ("Vertical"), 0.0f);
        photonView.RPC("Move", PhotonTargets.All, move);
    }

    [PunRPC]
    public void Move(Vector3 move)
    {
        transform.position += move;
    }
}
