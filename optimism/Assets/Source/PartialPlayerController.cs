using Photon;
using UnityEngine;

public class PartialPlayerController : PunBehaviour
{

    public float speed = 1f;

    Vector2 influence;
    


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            
        }
        else
        {
            
        }
    }

    void Update()
    {
        Vector2 move = Vector2.zero;
        move += new Vector2(speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0.0f);
        move += new Vector2(0.0f, speed * Time.deltaTime * Input.GetAxisRaw("Vertical"));

        if (!photonView.isMine)
        {
            if (Input.GetKeyDown("j"))
            {
                photonView.RequestOwnership();
            }
        }

        if (photonView.isMine)
        {
            move += influence;
            transform.position += new Vector3(move.x, move.y);
        }
        else
        {
            photonView.RPC("AcceptMoveInfluence", PhotonTargets.All, move);
        }
        influence = Vector2.zero;
    }

    [PunRPC]
    public void AcceptMoveInfluence(Vector2 move)
    {
        influence += move;
    }
}
