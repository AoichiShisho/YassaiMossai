using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        Debug.Log("Score updated: " + ScoreManager.Instance.Score);
        int score = ScoreManager.Instance.Score;
        scoreText.text = "Score: " + score.ToString();
    }
}
