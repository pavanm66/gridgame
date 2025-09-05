using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private int score = 0;

	public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (score < 0)
            {
                score = 0;
            }
            DisplayScore(score);
        }
    }
    public void AddScore(int _score)
    {
        Score += _score;
    }
    public void ResetScore()
    {
        Score = 0;
    }
    void DisplayScore(int _score)
    {
        GameManager.instance.DisplayScore(_score);
    }
}
