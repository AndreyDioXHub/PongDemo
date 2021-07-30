using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour, IOnEventCallback
{
    public static BallManager Instance;

    public static event Action OnBallCreated = delegate { };

    [SerializeField]
    private PhysicsMaterial2D _bounce;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void LoadBall()
    {
        float colorr = PlayerPrefs.GetFloat("ColorR", 1);
        float colorg = PlayerPrefs.GetFloat("ColorG", 1);
        float colorb = PlayerPrefs.GetFloat("ColorB", 1);

        float bounciness = PlayerPrefs.GetFloat("Bounciness", 1);

        _spriteRenderer.color = new Color(colorr, colorg, colorb, 1);
        _bounce.bounciness = bounciness;

        OnBallCreated.Invoke();
    }

    public void GenerateNewBall( )
    {
        float colorr = Random.Range(0, 100) / 100f;
        float colorg = Random.Range(0, 100) / 100f;
        float colorb = Random.Range(0, 100) / 100f;

        float bounciness = Random.Range(50, 100) / 100f;

        float speed = Random.Range(70, 200) / 10f;

        _spriteRenderer.color = new Color(colorr, colorg, colorb, 1);
        _bounce.bounciness = bounciness;

        PlayerPrefs.SetFloat("ColorR", colorr);
        PlayerPrefs.SetFloat("ColorG", colorg);
        PlayerPrefs.SetFloat("ColorB", colorb);
        PlayerPrefs.SetFloat("Bounciness", bounciness);
        PlayerPrefs.SetFloat("Speed", speed);


        if (PhotonNetwork.IsMasterClient == true)
        {
            RaiseEventOptions option = new RaiseEventOptions { Receivers = ReceiverGroup.Others};
            SendOptions senoptiond = new SendOptions { Reliability = true };
            byte eventid = 0;
            Vector3 bp = new Vector3(colorr, colorg, colorb);
            PhotonNetwork.RaiseEvent(eventid, bp, option, senoptiond);
        }

        OnBallCreated.Invoke();
        
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 0:
                Vector3 c = (Vector3)photonEvent.CustomData;
                _spriteRenderer.color = new Color(c.x, c.y, c.z, 1);
                PlayerPrefs.SetFloat("ColorR", c.x);
                PlayerPrefs.SetFloat("ColorG", c.y);
                PlayerPrefs.SetFloat("ColorB", c.z);

                break;
        }
       
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
