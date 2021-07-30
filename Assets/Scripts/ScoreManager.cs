using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private int _player1Score = 0;
    [SerializeField]
    private int _player2Score = 0;
    [SerializeField]
    private int _bestScore = 0;
    [SerializeField]
    private TextMeshProUGUI _player1ScoreText;
    [SerializeField]
    private TextMeshProUGUI _player2ScoreText;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    public int Player1Score 
    {
        get => _player1Score;
        set => _player1Score = value;
    }

    public int Player2Score
    {
        get => _player2Score;
        set => _player2Score = value;
    }

    public int BestScore
    {
        get => _bestScore;
        set => _bestScore = value;
    }

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
    }

    void Start()
    {
        PlayerField.OnGoal += UpdateScore;
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        _bestScoreText.text = "Best Score: " + BestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        _player1ScoreText.text = _player1Score.ToString();
        _player2ScoreText.text = _player2Score.ToString();
        _bestScoreText.text = "Best Score: " + BestScore.ToString();
    }
}
