using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  [SerializeField] float radius = 3f;
  [SerializeField] Vector2 explosionForce = new Vector2(5000f, 1000f);
  Animator bombAnimator;

  // Start is called before the first frame update
  void Start()
  {
    bombAnimator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }
  private void OnTriggerEnter2D(Collider2D collison)
  {
    bombAnimator.SetTrigger("Burn");
  }

  void DestroyBomb()
  {
    Destroy(gameObject);
  }

  void ExplodeBomb()
  {
    Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("player"));
    if (playerCollider)
    {
      playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);
      playerCollider.GetComponent<Player>().PlayerHit();
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, radius);
  }

}
