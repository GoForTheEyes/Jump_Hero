using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public static GameOverManager instance;

    public GameObject gameOverPanel;
    public Animator gameOverAnim;
    public Button playAgainBtn, backBtn;
    public Text finalScoreText;
    public Text highScoreText;


    private void Awake()
    {
        MakeInstance();
        InitializeVariables();
    }

    void MakeInstance()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    void InitializeVariables()
    {
        playAgainBtn.onClick.AddListener(() => PlayAgain());
        backBtn.onClick.AddListener(() => BackToMenu());
        gameOverPanel.SetActive(false);
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver(int finalScore, int highScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = finalScore.ToString();
        highScoreText.text = highScore.ToString();
        gameOverAnim.Play("FadeIn");
    }



}
