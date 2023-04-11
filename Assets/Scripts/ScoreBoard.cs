using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "START";
    }

    public void IncreaseScore(int amountToIncrease)
    {        
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
}
