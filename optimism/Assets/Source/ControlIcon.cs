using UnityEngine;
using UnityEngine.UI;

public class ControlIcon : MonoBehaviour {

    GameObject player;
    PlayerType current = PlayerType.Pessimist;
    Image image;

    Animator animator;

	void Start () {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
	}
	
	
	void Update () {
		if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        bool isLocalPlayerInControl = player.GetPhotonView().isMine;
        if (isLocalPlayerInControl && LocalState.PlayerType != current)
        {
            SwapType();
        }
        else if (!isLocalPlayerInControl && LocalState.PlayerType == current)
        {
            SwapType();
        }
	}

    void SwapType()
    {
        if (current == PlayerType.Optimist)
        {
            current = PlayerType.Pessimist;
        }
        else
        {
            current = PlayerType.Optimist;
        }

        animator.SetTrigger("flip");
    }
}
