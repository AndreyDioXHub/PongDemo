using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public static PlayerControll Instance;

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private PhotonView _playerView;

    [SerializeField]
    private int _myScore;
    [SerializeField]
    private int _myBestScore;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        PlayerField.OnGoal += UpdateScore;
       _myBestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerView.IsMine == false)
        {
            return;
        }

        //_player.position = new Vector2(_player.position.x, Input.mousePosition.y / Screen.height * 7f - 3f);
        _player.position = new Vector2(_player.position.x, _player.position.y + Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime);
        //Debug.Log(Input.mousePosition.y);
    }

    public void UpdateScore()
    {
        if (PhotonNetwork.LocalPlayer.UserId == "1")
        {
            //ScoreManager.Instance.Player1Score++;
            _myScore = ScoreManager.Instance.Player1Score;
        }
        else
        {
            //ScoreManager.Instance.Player2Score++;
            _myScore = ScoreManager.Instance.Player2Score;
        }

        if(_myScore > _myBestScore)
        {
            _myBestScore = _myScore;
            ScoreManager.Instance.BestScore = _myBestScore;
            PlayerPrefs.SetInt("BestScore", _myBestScore);
        }
    }
}
