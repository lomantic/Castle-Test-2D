using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
  [SerializeField] float secondsToLoad = 1f;
  private void OnTriggerEnter2D(Collider2D collison)
  {
    GetComponent<Animator>().SetTrigger("Open");
  }
  private void OnTriggerExit2D(Collider2D collison)
  {
    GetComponent<Animator>().SetTrigger("Close");
  }
  public void StartNextLevel()
  {
    GetComponent<Animator>().SetTrigger("Close");
    StartCoroutine(LoadNextLevel());
  }

  IEnumerator LoadNextLevel()
  {
    yield return new WaitForSeconds(secondsToLoad);

    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
}
