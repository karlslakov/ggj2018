using UnityEngine;

public class ConnectionLogic : MonoBehaviour {

    bool attemptedToConnect = false;
    bool createPlayer = false;

    void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
    }

    void Update()
    {
        if (!attemptedToConnect)
        {
            attemptedToConnect = true;
            PhotonNetwork.ConnectUsingSettings(1 + "." + SceneManagerHelper.ActiveSceneBuildIndex);
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
        createPlayer = true;
    }


    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room.");
        if (createPlayer)
        {
            Debug.Log("Creating player");
            PhotonNetwork.InstantiateSceneObject("Player", Vector3.zero, Quaternion.identity, 0, null);
        }
    }
}
