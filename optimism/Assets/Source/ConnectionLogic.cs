using UnityEngine;

public class ConnectionLogic : MonoBehaviour {

    bool attemptedToConnect = false;
    bool createdPlayer = false;
    bool createdLight = false;

    public GameObject PointLightBase;

    void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
    }

    void Update()
    {
        if (PhotonNetwork.connected && !attemptedToConnect)
        {
            attemptedToConnect = true;
            SetVisible();
        }

        if (!attemptedToConnect)
        {
            attemptedToConnect = true;
            PhotonNetwork.ConnectUsingSettings(1 + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }

        if (PhotonNetwork.isMasterClient && !createdPlayer)
        {
            createdPlayer = true;
            PhotonNetwork.InstantiateSceneObject("Player", Vector3.zero, Quaternion.identity, 0, null);
        }

        if (!createdLight)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                GameObject pointlight = GameObject.Instantiate(PointLightBase, player.transform);
                pointlight.transform.localPosition = new Vector3(0, 0, -1);
                Light light = pointlight.GetComponent<Light>();
                light.type = LightType.Point;
                switch (LocalState.PlayerType)
                {
                    case PlayerType.Optimist: light.range = 12; light.intensity = 1.4f; break;
                    case PlayerType.Pessimist: light.range = 3; light.intensity = 2; break;
                }
                createdLight = true;
            }
        }
    }

    void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Creating room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
    }


    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room.");

        if (PhotonNetwork.isMasterClient)
        {
            LocalState.PlayerType = PlayerType.Optimist;
        }
        else
        {
            LocalState.PlayerType = PlayerType.Pessimist;
        }
        //Camera.main.gameObject.AddComponent<CameraEffect>();

        SetVisible();
    }

    void SetVisible()
    {
        if (LocalState.PlayerType == PlayerType.Optimist)
        {
            Camera.main.cullingMask = LayerMask.GetMask("Optimist", "Default", "UI");
        }
        else
        {
            Camera.main.cullingMask = LayerMask.GetMask("Pessimist", "Default", "UI");
        }

    }
}
