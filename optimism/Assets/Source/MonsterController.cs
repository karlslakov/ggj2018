using Photon;
using UnityEngine;

public class MonsterController : PunBehaviour {
    
    enum Mode
    {
        CircleLeft,
        CircleRight,
        Charge
    }

    GameObject player;
    Animator animator;
    float timeTillNextStance = 2f;
    Mode mode = Mode.CircleLeft;

    static Random rng = new Random();
    float speed = 0.2f;
    bool dead = false;

    Vector3 direction = Vector2.one;
    Vector3 offset;
    int attackHashID = Animator.StringToHash("attack");

	void Start () {
        animator = GetComponent<Animator>();
        offset = GetComponent<BoxCollider2D>().offset;
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

            direction = (player.transform.position - offset) - transform.position;
            bool kill = direction.magnitude <= 0.8f;
            direction.Normalize();

            if (!kill)
            {
                if (mode == Mode.CircleLeft)
                {
                    direction = new Vector3(-direction.y, direction.x);
                }
                else if (mode == Mode.CircleRight)
                {
                    direction = new Vector3(direction.y, -direction.x);
                }

                timeTillNextStance -= Time.deltaTime;
                if (timeTillNextStance <= 0f)
                {
                    mode = GetNextState(mode);
                    timeTillNextStance = Random.Range(1f, 5f);
                    speed = Random.Range(0.05f, 0.2f);
                }
            }
            else
                speed = 4.1f;

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    Mode GetNextState(Mode mode)
    {
        switch (mode)
        {
            case Mode.CircleRight: return Mode.CircleLeft;
            case Mode.CircleLeft: return Mode.Charge;
            default: return Mode.CircleRight;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth>().Dmg(110f);
            Attack();
        }
    }
    
    public void Attack()
    {
        Vector3 parentPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.localPosition = Vector3.zero;
        transform.position = new Vector3(parentPos.x - 0.05f, parentPos.y);
        animator.SetTrigger(attackHashID);
        dead = true;
        Destroy(gameObject, 0.5f);
        Transform child = transform.Find("DropShadow");
        child.localPosition = new Vector3(-0.03f, -0.154f);
        child.localScale = new Vector3(2, 1, 1);
    }
}
