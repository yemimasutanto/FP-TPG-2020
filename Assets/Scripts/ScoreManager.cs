using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        currentScore += amountToIncrease;
        SetScoreText();
    }

    void SetScoreText()
    {
        if (scoreText)
        {
            scoreText.text = "" + currentScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
