using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Threading.Tasks;
public class Bomb : MonoBehaviour
{
  [SerializeField] float radius = 3f;
  [SerializeField] Vector2 explosionForce = new Vector2(5000f, 1000f);
  [SerializeField] AnimationHandler animationHandler;
  Animator bombAnimator;

  const string BOMB_BURN = "Bomb On";
  const string BOMB_EXPLODE = "Bomb Explode";

  // Start is called before the first frame update
  void Start()
  {
    bombAnimator = GetComponent<Animator>();
  }

  // Update is called once per frame
  private void OnTriggerEnter2D(Collider2D collison)
  {
    animationHandler.ChangeAnimationState(BOMB_BURN);
    //Debug.Log("Before Couroutiine : " + bombAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    StartCoroutine(waitToExplode());
  }

  IEnumerator waitToExplode()
  {
    yield return null;
    Invoke("Explosion", bombAnimator.GetCurrentAnimatorStateInfo(0).length);
    //Debug.Log("After couroutine : " + bombAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
  }

  private void Explosion()
  {
    animationHandler.ChangeAnimationState(BOMB_EXPLODE);
  }

  void DestroyBomb()
  {
    Destroy(gameObject);
  }

  void ExplodeBomb()
  {
    Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("player"));
    if (playerCollider && !Physics2D.GetIgnoreLayerCollision(8, 11))
    {
      playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce, ForceMode2D.Force);
      playerCollider.GetComponent<Player>().PlayerHit();
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, radius);
  }

}
