using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text scoretext;
    public Text highScore;
    public static int score;
    public static int highscore;

    void Awake()
    {
        scoretext = GetComponent<Text>();
        score = 0;
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "Score : " + score;

        if (score > highscore)
        {
            highscore = score;
            saveHighScore();
        }

        highScore.text = "High Score : " + highscore.ToString();
    }

    void saveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highscore);
        PlayerPrefs.Save();
    }
}
