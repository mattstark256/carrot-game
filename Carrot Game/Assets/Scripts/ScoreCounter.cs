using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score = 0;

    private void Awake()
    {
        UpdateScoreText();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (score == 1)
        {
            scoreText.text = "1 carrot collected";

        }
        else
        {
            scoreText.text = score + " carrots collected";
        }
    }

    public int GetScore()
    {
        return score;
    }
}
