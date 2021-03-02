using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//todos los manager suelen ser singlenton

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Text healthText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text finalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void UpdateUIScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateUIHelth(int newHealth)
    {
        healthText.text = newHealth.ToString();
    }

    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        finalScore.text = "SCORE" + GameManager.Instance.Score;
    }
}
