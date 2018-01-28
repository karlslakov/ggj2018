using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Sprite neg, pos;
    Image goText;
    Animator animator;
    GameObject player;

	void Start () {
        animator = GetComponent<Animator>();
        goText = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (LocalState.PlayerType)
        {
            case PlayerType.Optimist: goText.sprite = pos; break;
            case PlayerType.Pessimist: goText.sprite = neg; break;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        if (player.GetComponent<PlayerHealth>().isDead())
        {
            animator.SetTrigger("gameover");
        }
	}
}
