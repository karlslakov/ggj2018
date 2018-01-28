using Photon;
using UnityEngine;

public class PlayerTakeover : PunBehaviour {

    int takeoversRemaining = 5;


	void Start () {
		
	}

    void Update()
    {
        if (!photonView.isMine)
        {
            if (Input.GetKeyDown("j"))
            {
                if (takeoversRemaining > 0)
                {
                    photonView.RPC("SetTakeovers", PhotonTargets.All, takeoversRemaining - 1);
                    photonView.RequestOwnership();
                }
            }
        }
    }



    [PunRPC]
    public void SetTakeovers(int takeovers)
    {
        takeoversRemaining = takeovers;
    }
}
