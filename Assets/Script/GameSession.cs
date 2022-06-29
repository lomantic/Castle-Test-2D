using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
  [SerializeField] int playerLives = 4, score = 0;
  [SerializeField] TMP_Text scoreText, livesText;
  //[SerializeField] Text scoreText, livesText;
  private void Awake()
  {
    int numGameSession = FindObjectsOfType<GameSession>().Length;
    livesText.text = playerLives.ToString();
    scoreText.text = score.ToString();
    if (numGameSession > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }
  private void start()
  {
    // livesText.text = playerLives.ToString();
    // scoreText.text = score.ToString();
  }
  public void AddPoint(int value)
  {
    score += value;
    scoreText.text = score.ToString();
  }
  public void AddHeart(int value)
  {
    playerLives += value;
    livesText.text = playerLives.ToString();
  }
  public void ProcessPlayerDeath()
  {
    if (playerLives > 1)
    {
      TakeLife();
    }
    else
    {
      ResetGame();
    }
  }

  private void TakeLife()
  {
    playerLives--;
    livesText.text = playerLives.ToString();
  }

  private void ResetGame()
  {
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
}
