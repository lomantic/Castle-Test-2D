using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaRangeSkill : MonoBehaviour
{
  [SerializeField] Transform skillRange;
  [SerializeField] float skillRange_float = 4f;
  private Collider2D[] enemiesHit = new Collider2D[10];
  SpriteRenderer mySpriteRenderer;
  private Enemy target = null;
  private Enemy newTarget = null;
  private Enemy lockedTarget = null;

  private float centerToEnemyDistance = -1f;
  private float targetDistance = -1f;
  private Color skillAvailable = new Color(0.1342f, 0.866f, 0f, 0.169f);
  // Start is called before the first frame update
  void Start()
  {
    mySpriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    targeting();
  }

  private void targeting()
  {
    StartCoroutine(targetEnemy());
  }
  IEnumerator targetEnemy()
  {
    yield return new WaitForSeconds(0.1f);
    if (mySpriteRenderer.color == skillAvailable)
    {
      enemiesHit = Physics2D.OverlapCircleAll(skillRange.position, skillRange_float, LayerMask.GetMask("enemy"));
      if (enemiesHit.Length != 0)
      {
        foreach (Collider2D enemy in enemiesHit)
        {
          newTarget = enemy.GetComponent<Enemy>();
          centerToEnemyDistance = (newTarget.transform.position - skillRange.position).magnitude;
          if (targetDistance > centerToEnemyDistance || targetDistance == -1f)
          {
            targetDistance = centerToEnemyDistance;
            target = newTarget;
          }
        }
        targetDistance = -1f;

        if (lockedTarget == null && target != null)
        {
          lockedTarget = target;
          lockedTarget.targeted();
        }//중복이 아닌 새로운 적
        else if (target != lockedTarget)
        {
          lockedTarget.targetDisabled();
          target.targeted();
          lockedTarget = target;
        }
      }
      else if (enemiesHit.Length == 0 && lockedTarget != null)
      {
        lockedTarget.targetDisabled();
        lockedTarget = null;
      }
    }
    else if (lockedTarget != null)
    {
      lockedTarget.targetDisabled();
      lockedTarget = null;
    }


  }
  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(skillRange.position, skillRange_float);
  }

}

