using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        BallManager.OnBallCreated += Launch;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch() 
    {
        _speed = PlayerPrefs.GetFloat("Speed", 10f);
        _rb.transform.position = Vector2.zero;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        _rb.velocity = new Vector2(_speed * x, _speed * y);
    }

    public void JustFoolowToServer()
    {
        _rb.isKinematic = true;
        this.enabled = false;
    }

    public void StopBall()
    {
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = false;
        this.enabled = true;
        _rb.transform.position = Vector2.zero;
    }

}
