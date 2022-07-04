using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [SerializeField] float attackRange = 1f;
  [SerializeField] Transform hurtBox;
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 50f;
  [SerializeField] float climbSpeed = 5f;
  [SerializeField] Vector2 hitKick = new Vector2(15f, 10f);
  [SerializeField] AudioClip attackSFX, runningSFX;
  [SerializeField] private Camera mainCamera;
  [SerializeField] Vector3 charPos, dashVector;
  Rigidbody2D myRigidBody2D;
  Animator myAnimator;
  BoxCollider2D myBoxCollider2D;
  PolygonCollider2D myPolygonCollider2D;
  AudioSource myAudioSource;
  float startingGravityScale = 0.0f;
  int jumpCnt = 0;
  bool stun = false;
  TrailRenderer trailRenderer;
  private GameObject FB_obj = null;
  private Vector3 fbPos;

  [Header("Dahsing")]
  [SerializeField] private float dashingSpeed = 10f;
  [SerializeField] private float dashingTime = 0.3f;
  private Vector2 dashingDir;
  private bool CS = false;
  private bool isDashing = false;
  private bool canDash = true;
  private bool FB_activated = false;

  // Start is called before the first frame update
  void Start()
  {
    myRigidBody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myBoxCollider2D = GetComponent<BoxCollider2D>();
    myPolygonCollider2D = GetComponent<PolygonCollider2D>();
    myAudioSource = GetComponent<AudioSource>();
    trailRenderer = GetComponent<TrailRenderer>();
    mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    FB_obj = GameObject.Find("Magic Box");

    startingGravityScale = myRigidBody2D.gravityScale;
    myAnimator.SetTrigger("DoorOut");
  }

  // Update is called once per frame
  void Update()
  {
    if (!stun)
    {
      FlameBlink();
      Run();
      Dash();
      Jump();
      Climb();
      Attack();
      CounterSpear();

      if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("enemy")))
      {
        PlayerHit();
      }
      ExitLevel();
    }
  }

  public void PlayerHit()
  {
    myRigidBody2D.velocity = hitKick * new Vector2(-transform.localScale.x, 1f);
    myAnimator.SetTrigger("Hit");
    stun = true;

    FindObjectOfType<GameSession>().ProcessPlayerDeath();
    StartCoroutine(StopStun());
  }
  IEnumerator StopStun()
  {
    yield return new WaitForSeconds(2f);
    stun = false;
  }
  private void ExitLevel()
  {
    if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Interact")))
    {
      return;
    }
    if (CrossPlatformInputManager.GetButton("Vertical"))
    {
      myAnimator.SetTrigger("DoorIn");

    }
  }

  public void LoadingNextLevel()
  {
    FindObjectOfType<Door>().StartNextLevel();
    TurnOffRender();
  }
  public void TurnOffRender()
  {
    GetComponent<SpriteRenderer>().enabled = false;
  }

  private void CounterSpear()
  {
    bool csInput = CrossPlatformInputManager.GetButtonDown("CS");
    if (csInput)
    {
      CS = !CS;
      Physics2D.IgnoreLayerCollision(8, 10, CS);
    }
  }
  private void Dash()
  {
    bool dashInput = CrossPlatformInputManager.GetButtonDown("Dash");

    if (dashInput && canDash)
    {
      Vector3 mousePos = mainCamera.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
      charPos = GameObject.Find("Player").transform.position;
      mousePos.z = 0f;
      charPos.z = 0f;
      dashVector = (mousePos - charPos);
      isDashing = true;
      canDash = false;
      trailRenderer.emitting = true;
      dashingDir = dashVector;
      //dashingDir = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
      if (dashingDir == Vector2.zero)
      {
        dashingDir = new Vector2(transform.localScale.x, 0);
      }
      StartCoroutine(StopDashing());
    }


    if (isDashing)
    {
      myRigidBody2D.drag = 10;
      Physics2D.IgnoreLayerCollision(8, 10, true);
      Physics2D.IgnoreLayerCollision(8, 11, true);
      myAnimator.SetBool("Run", isDashing);
      myRigidBody2D.velocity = dashingDir.normalized * dashingSpeed;
      return;
    }

    if (onGround())
    {
      canDash = true;
    }
  }

  private IEnumerator StopDashing()
  {
    yield return new WaitForSeconds(dashingTime);
    myRigidBody2D.drag = 0;
    Physics2D.IgnoreLayerCollision(8, 10, false);
    Physics2D.IgnoreLayerCollision(8, 11, false);
    trailRenderer.emitting = false;
    isDashing = false;
  }
  private void Run()
  {
    float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
    myRigidBody2D.velocity = playerVelocity;
    FlipSprite();
    ChangeStateToRun();
  }

  void runSFX()
  {

    if (isWalking() && onGround())
    {
      myAudioSource.PlayOneShot(runningSFX);
    }
    else
    {
      myAudioSource.Stop();
    }
  }
  private void ChangeStateToRun()
  {
    bool runHorizontal = isWalking();
    myAnimator.SetBool("Run", runHorizontal);

  }
  private void ChangeStateToClimb()
  {
    bool climbHorizontal = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
    myAnimator.SetBool("Climb", climbHorizontal);
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
  private bool isWalking()
  {
    if (Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  private bool onGround()
  {
    if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
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

  private void Climb()
  {
    if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))
    {
      float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
      Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * climbSpeed);
      myRigidBody2D.velocity = climbVelocity;
      myRigidBody2D.gravityScale = 0f;
    }
    else
    {
      myRigidBody2D.gravityScale = startingGravityScale;
    }
    ChangeStateToClimb();
  }

  private void Attack()
  {
    bool isAttack = CrossPlatformInputManager.GetButtonDown("Fire1");
    if (isAttack)
    {
      myAnimator.SetTrigger("Attack");
      myAudioSource.PlayOneShot(attackSFX);
      Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRange, LayerMask.GetMask("enemy"));

      foreach (Collider2D enemy in enemiesHit)
      {
        enemy.GetComponent<Enemy>().Dying();
      }
    }

  }

  private void FlameBlink()
  {
    bool isFB = CrossPlatformInputManager.GetButtonDown("FB");

    if (isFB)
    {
      Debug.Log("q 눌렀음 ");
      Debug.Log("포탈 설치 여부 : " + FB_activated);
      if (FB_activated)
      {
        GameObject.Find("Player").transform.position = fbPos;
        Debug.Log("포탈로 이동 " + fbPos);
        fbPos = new(0f, 0f, 0f);
      }
      else
      {
        fbPos = GameObject.Find("Player").transform.position;
        Instantiate(FB_obj, fbPos, Quaternion.identity);
        Debug.Log("매직 박스 : " + FB_obj);
        Debug.Log("포탈 생성 위치 " + fbPos);
      }
      FB_activated = !FB_activated;
    };

  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(hurtBox.position, attackRange);
  }
}

