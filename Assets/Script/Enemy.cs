using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float enemyRunSpeed = 5f;
  Rigidbody2D enemyRigidBody;
  Animator enemyAnimator;

  // Start is called before the first frame update
  void Start()
  {
    enemyRigidBody = GetComponent<Rigidbody2D>();
    enemyAnimator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    EnemyMovement();
  }

  public void Dying()
  {
    enemyAnimator.SetTrigger("Die");
    GetComponent<CapsuleCollider2D>().enabled = false;
    GetComponent<BoxCollider2D>().enabled = false;
    enemyRigidBody.bodyType = RigidbodyType2D.Static;
    StartCoroutine(ClaenUpCorp());
  }
  IEnumerator ClaenUpCorp()
  {
    yield return new WaitForSeconds(2f);
    Destroy(gameObject);
  }

  private void EnemyMovement()
  {
    if (IsfacingLeft())
    {
      enemyRigidBody.velocity = new Vector2(-enemyRunSpeed, 0f);
    }
    else
    {
      enemyRigidBody.velocity = new Vector2(enemyRunSpeed, 0f);
    }
  }

  private void OnTriggerExit2D(Collider2D collison)
  {
    flipSprite();
  }

  private void flipSprite()
  {
    transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody.velocity.x), 1f);
  }
  private bool IsfacingLeft()
  {
    return transform.localScale.x > 0;
  }



}

