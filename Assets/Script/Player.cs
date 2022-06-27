using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 10f;
  Rigidbody2D myRigidBody2D;
  Animator myAnimator;
  BoxCollider2D myBoxCollider2D;
  PolygonCollider2D myPolygonCollider2D;
  int jumpCnt = 0;

  // Start is called before the first frame update
  void Start()
  {
    myRigidBody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myBoxCollider2D = GetComponent<BoxCollider2D>();
    myPolygonCollider2D = GetComponent<PolygonCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    Run();
    Jump();
  }
  private void Run()
  {
    float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
    myRigidBody2D.velocity = playerVelocity;
    FlipSprite();
    ChangeStateToRun();


  }
  private void ChangeStateToRun()
  {
    bool runHorizontal = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
    myAnimator.SetBool("Run", runHorizontal);
  }
  private void FlipSprite()
  {
    bool runHorizontal = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
    float runDirection = Mathf.Sign(myRigidBody2D.velocity.x);

    if (runHorizontal)
    {
      transform.localScale = new Vector2(runDirection, 1f);
    }

  }
  private bool onGround()
  {
    if (myPolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
    {
      jumpCnt = 0;
      return true;
    }
    else
    {
      return false;
    }
  }
  private void Jump()
  {
    bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

    if (isJumping)
    {
      if (!onGround() && jumpCnt == 2)
      {
        return;
      }
      Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
      myRigidBody2D.velocity = jumpVelocity;
      jumpCnt++;

    }


  }

}
