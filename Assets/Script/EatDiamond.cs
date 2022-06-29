using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatDiamond : MonoBehaviour
{
  [SerializeField] AudioClip diamondEatSFX;
  [SerializeField] int diamondPoint = 500;
  private void OnTriggerEnter2D(Collider2D collison)
  {
    AudioSource.PlayClipAtPoint(diamondEatSFX, Camera.main.transform.position);
    FindObjectOfType<GameSession>().AddPoint(diamondPoint);
    Destroy(gameObject);
  }

}
