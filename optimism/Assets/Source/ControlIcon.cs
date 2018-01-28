using UnityEngine;
using UnityEngine.UI;

public class ControlIcon : MonoBehaviour {

    GameObject player;
    PlayerType current = PlayerType.Optimist;
    Image image;

	void Start () {
        image = GetComponent<Image>();
        SwapType();
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
            image.color = Color.blue;
        }
        else
        {
            current = PlayerType.Optimist;
            image.color = Color.yellow;
        }
    }
}
