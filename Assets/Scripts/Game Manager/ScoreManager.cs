using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;
    private static string HIGH_SCORE = "high score";

    public Text scoreText;

    private int score;

    private void Awake()
    {
        score = 0;
        MakeInstance();
        InitializeHighScore();
    }

    void InitializeHighScore()
    {
        if (!PlayerPrefs.HasKey(HIGH_SCORE))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
        } 
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return this.score;
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void CheckIfHighScore()
    {
        float highScore = GetHighScore();
        if (highScore < this.score)
        {
            SetHighScore(this.score);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
