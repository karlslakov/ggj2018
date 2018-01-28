using Photon;
using System.Collections;
using UnityEngine;

public class PartialPlayerController : PunBehaviour
{
    public float speed = 1f;
    Vector2 influence;
    Animator animator;

    bool walking;
    int facing;

    int facingParamId = Animator.StringToHash("Facing");
    int walkingParamId = Animator.StringToHash("Walking");
        
    void Start()
    {
        animator = GetComponent<Animator>();
        Camera.main.transform.parent = transform;
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(walking);
            stream.SendNext(facing);
        }
        else
        {
            walking = (bool)stream.ReceiveNext();
            facing = (int)stream.ReceiveNext();
        }
    }

    void Update()
    {
        Vector2 move = Vector2.zero;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        move += new Vector2(horizontal, vertical) * speed * Time.deltaTime;

        if (photonView.isMine)
        {
            move += influence;
            FaceAndWalk(move);
            GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(move.x, move.y));
        }
        else
        {
            photonView.RPC("AcceptMoveInfluence", PhotonTargets.All, move * 0.5f);
        }

        influence = Vector2.zero;
        animator.SetBool(walkingParamId, walking);
        animator.SetInteger(facingParamId, facing);
    }

    private void FaceAndWalk (Vector2 move)
    {
        if (move.x != 0 || move.y != 0)
        {
            walking = true;
            if (move.y != 0)
            {
                if (move.y > 0)
                    facing = 1;
                else
                    facing = 3;
            }
            else
            {
                if (move.x < 0)
                    facing = 2;
                else
                    facing = 0;
            } 
        }
        else
            walking = false;
    }

    [PunRPC]
    public void AcceptMoveInfluence(Vector2 move)
    {
        influence += move;
    }

    public void Die()
    {
        enabled = false;
        animator.SetBool(walkingParamId, false);
        GetComponent<PlayerTakeover>().enabled = false;
        StartCoroutine("delayInvisible");
    }

    private IEnumerator delayInvisible()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().enabled = false;
     
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("mainscene");
        yield break;
    }
}
