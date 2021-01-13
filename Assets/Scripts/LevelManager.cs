using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentEXP = 0;
    [SerializeField] private int maxLevel = 10;
    [SerializeField] private TextMeshProUGUI levelText, EXPText;

    private int maxEXP = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetLevelText();
        SetEXPText();
    }

    public void getEXP(int exp)
    {
        currentEXP += exp;
        SetEXPText();
    }

    public void LevelUp(int newMaxEXP)
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            currentEXP = 0;
            maxEXP = newMaxEXP;
            SetLevelText();
            SetEXPText();
        }
    }

    void SetLevelText()
    {
        if (levelText)
        {
            levelText.text = "Level " + currentLevel;
        }
    }

    void SetEXPText()
    {
        if (EXPText)
        {
            EXPText.text = "EXP " + currentEXP + " / " + maxEXP;
        }
    }
}
