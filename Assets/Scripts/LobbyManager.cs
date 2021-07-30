using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _logText;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    private string _nickName;
    private string _version = "1";
    // Start is called before the first frame update
    void Start()
    {
        _nickName = "Player_" + Random.Range(1000, 9999);

        _bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
        PhotonNetwork.NickName = _nickName;
        Log("nick name" + _nickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = _version;
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Log("connect to mster");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Log("Joined to Room");
        PhotonNetwork.LoadLevel("Game");

    }

    public void Log(string message)
    {
        Debug.Log(message);
        _logText.text += "\n" + message;
    }
}
