using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreMngr : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private OnStringChange scoreValue;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreValue?.Invoke("Score: " + score.ToString());
        }
    }
    public void ResetScore()
    {
        Score = 0;
    }
    public void AddScore(int scoreValue)
    {
        if (scoreValue > 0)
        {
            Score += scoreValue;
        }
    }
}
