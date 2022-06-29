using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatHeart : MonoBehaviour
{
  [SerializeField] AudioClip heartEatSFX;
  [SerializeField] int additionHeart = 1;
  private void OnTriggerEnter2D(Collider2D collison)
  {
    AudioSource.PlayClipAtPoint(heartEatSFX, Camera.main.transform.position);
    FindObjectOfType<GameSession>().AddHeart(additionHeart);
    Destroy(gameObject);
  }


}
