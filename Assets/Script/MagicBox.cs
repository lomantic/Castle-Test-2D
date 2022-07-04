using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBox : MonoBehaviour
{
  [SerializeField] float radius = 30f;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  bool ExecuteMagic()
  {
    Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("player"));
    if (playerCollider)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
