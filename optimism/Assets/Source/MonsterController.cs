using Photon;
using UnityEngine;

public class MonsterController : PunBehaviour {
    const float moveperpchance = 0.5f;

    GameObject player;
    Animator animator;
    float timeTillSwap = 2f;
    bool dashing = true;
    static Random rng = new Random();
    float speed = 0f;
    bool dead = false;

    Vector3 direction = Vector2.one;

    int attackHashID = Animator.StringToHash("attack");

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine && !dead) 
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player == null) return;
            }


            if (!dashing)
            {
                timeTillSwap -= Time.deltaTime;
                if (timeTillSwap <= 0f)
                {
                    direction = player.transform.position - transform.position;
                    direction.Normalize();
                    dashing = true;
                    timeTillSwap = Random.Range(0.1f, 1.1f);
                    speed = Random.Range(1.8f, 4.1f);
                }
            }
            else
            {
                timeTillSwap -= Time.deltaTime;
                if (timeTillSwap <= 0f)
                {
                    direction = player.transform.position - transform.position;
                    direction.Normalize();
                    timeTillSwap = Random.Range(1f, 4f);
                    if (Random.Range(0f, 1f) > 0.5f)
                    {
                        direction = new Vector3(-direction.y, direction.x, 0);
                    }
                    else
                    {
                        direction = new Vector3(direction.y, -direction.x, 0);
                    }
                    dashing = false;
                    speed = Random.Range(0.05f, 0.2f);
                }
            }
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetPhotonView().RPC("Dmg", PhotonTargets.All, 110f);
            animator.SetTrigger(attackHashID);
            dead = true;
            Destroy(gameObject, 1f);
        }

    }
}
