using Photon;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeover : PunBehaviour
{

    int takeoversRemaining = 3;
    Image takeoverTracker;

    PlayerType current;
    public Sprite op1, op2, op3, neg1, neg2, neg3;

    void Start()
    {
        takeoverTracker = GameObject.Find("TransferTracker").GetComponent<Image>();
        photonView.RPC("SetTakeovers", PhotonTargets.All, 3);
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            if (Input.GetKeyDown("j"))
            {
                if (takeoversRemaining > 0)
                {
                    photonView.RequestOwnership();
                    photonView.RPC("SetTakeovers", PhotonTargets.All, takeoversRemaining - 1);
                }
            }
        }
        
    }

    [PunRPC]
    public void SetTakeovers(int takeovers)
    {
        takeoversRemaining = takeovers;


        if (LocalState.PlayerType == PlayerType.Optimist)
        {
            switch (takeoversRemaining)
            {
                case 0: takeoverTracker.sprite = null; break;
                case 1: takeoverTracker.sprite = op1; break;
                case 2: takeoverTracker.sprite = op2; break;
                case 3: takeoverTracker.sprite = op3; break;
            }
        }
        else
        {
            switch (takeoversRemaining)
            {
                case 0: takeoverTracker.sprite = null; break;
                case 1: takeoverTracker.sprite = neg1; break;
                case 2: takeoverTracker.sprite = neg2; break;
                case 3: takeoverTracker.sprite = neg3; break;
            }
        }
        if (takeoversRemaining == 0) takeoverTracker.color = new Color(0, 0, 0, 0);
        else takeoverTracker.color = new Color(1, 1, 1, 1);
    }

}