using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerField : MonoBehaviour
{
    public static event Action OnGoal = delegate { };

    [SerializeField]
    private int _playerID;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            Ball.Instance.Launch();
            if(_playerID == 0)
            {
                ScoreManager.Instance.Player1Score++;
                //ScoreManager.Instance.UpdateScore();
                OnGoal.Invoke();
            }
            else
            {
                ScoreManager.Instance.Player2Score++;
                //ScoreManager.Instance.UpdateScore();
                OnGoal.Invoke();
            }
        }
    }
}
