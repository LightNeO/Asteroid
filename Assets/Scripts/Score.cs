using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float scoreValue = 0;
    private int scoreTotal;
    private Text score;
    private void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = " " + scoreValue;
        scoreTotal = int.Parse(score.text);

        if (scoreValue > PlayerPrefs.GetInt("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore", scoreTotal);
        }
        
    }
}
