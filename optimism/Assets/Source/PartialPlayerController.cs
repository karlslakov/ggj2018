using Photon;
using UnityEngine;

public class PartialPlayerController : PunBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	/*
	void Update () {
        float speed = 1f;
        
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);

        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
    }
    */
    void Update () {
        float speed = 1f;
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
            move += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.A))
            move -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
            move += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);

        if (Input.GetKey(KeyCode.S))
            move -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);

        photonView.RPC("Move", PhotonTargets.All, move);
    }

    [PunRPC]
    public void Move(Vector3 move)
    {
        transform.position += move;
    }
}
