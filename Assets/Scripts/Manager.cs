using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Ball _ball;
    //[SerializeField]
    //private GameObject _ballManager;
    [SerializeField]
    private GameObject _resetButton;
    [SerializeField]
    private GameObject _waitMessage;

    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;

        Vector2 pos = Vector2.zero;

        if (players.Length == 1)
        {
            pos = new Vector2(-7.5f, 0);
        }
        else
        {
            pos = new Vector2(7.5f, 0);
        }
        
        PhotonNetwork.Instantiate(_playerPrefab.name, pos, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient == false)
        {
            //_ballManager.SetActive(false);
            _ball.JustFoolowToServer();
            _resetButton.SetActive(false);
        }

        CheckState();
    }

    // Update is called once per frame
    void Update()
    {
        var players = PhotonNetwork.PlayerList;
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Player " + newPlayer.NickName + " Joined");
        CheckState();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player " + otherPlayer.NickName + " Left");
        CheckState();
    }

    public void CheckState()
    {
        var players = PhotonNetwork.PlayerList;

        if (players.Length > 1)
        {
            _waitMessage.SetActive(false);
            BallManager.Instance.LoadBall();
        }
        else
        {
            //_ballManager.SetActive(true);
            _resetButton.SetActive(true);
            _waitMessage.SetActive(true);
            Ball.Instance.StopBall();
        }
    }
}
