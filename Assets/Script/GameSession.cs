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
  [SerializeField] Image[] hearts;
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
    if (playerLives < 3)
    {
      playerLives += value;
    }
    UpdateHearts();
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
    UpdateHearts();
    livesText.text = playerLives.ToString();
  }

  private void UpdateHearts()
  {
    for (int i = 0; i < hearts.Length; i++)
    {
      if (i < playerLives)
      {
        hearts[i].enabled = true;
      }
      else
      {
        hearts[i].enabled = false;
      }
    }
  }

  private void ResetGame()
  {
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
}
