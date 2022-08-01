using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.InputSystem;
using Cinemachine;


public class Player : MonoBehaviour
{
  public MouseItem mouseItem = new MouseItem();
  [SerializeField] float attackRange = 1f;
  [SerializeField] Transform hurtBox;
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 50f;
  [SerializeField] float climbSpeed = 5f;
  [SerializeField] Vector2 hitKick = new Vector2(15f, 10f);
  [SerializeField] AudioClip attackSFX, runningSFX;
  [SerializeField] Camera mainCamera;
  [SerializeField] Camera UiCamera;
  [SerializeField] Camera WorldCamera;
  [SerializeField] Vector3 charPos, dashVector;
  Rigidbody2D myRigidBody2D;
  Animator myAnimator;
  BoxCollider2D myBoxCollider2D;
  PolygonCollider2D myPolygonCollider2D;
  AudioSource myAudioSource;

  Transform myTransform;
  float startingGravityScale = 0.0f;
  int jumpCnt = 0;
  bool stun = false;
  TrailRenderer trailRenderer;
  [SerializeField] GameObject FB_obj = null;
  [SerializeField] GameObject newBlink = null;

  [SerializeField] GameObject range_obj = null;
  [SerializeField] GameObject drop_skill_obj = null;

  private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
  private GameObject newRangeSkill = null;
  private GameObject newDropSkill = null;
  private GameObject newMapPin = null;
  private bool range_skill_activated = false;
  AsyncOperationHandle Handle;
  AsyncOperationHandle skillHandle;
  AsyncOperationHandle dropSkillHandle;
  AsyncOperationHandle MapPinHandle;
  private Vector3 fbPos;
  private Vector3 dropPos;

  private Vector3 newPinPos;
  private Vector2 newPinPosTracker;
  private Vector2 mouseTracker;
  RaycastHit2D hit;

  [Header("Dahsing")]
  [SerializeField] private float dashingSpeed = 10f;
  [SerializeField] private float dashingTime = 0.3f;
  private Vector2 dashingDir;
  private bool CS = false;
  private bool isDashing = false;
  //private bool canDash = true;
  private bool FB_activated = false;
  static public bool castling_possible = false;
  public HitAreaRangeSkill targetOn;
  private bool mapOn = false;
  [SerializeField] InventoryObject inventory;
  [SerializeField] InventoryObject equipmentInventory;
  [SerializeField] CinemachineConfiner2D _cache;

  [Header("Blink")]
  [SerializeField] LineRenderer _lineRenderer;

  [Header("WorldTest")]
  [SerializeField] BoxCollider2D cameraCollider;
  [SerializeField] PolygonCollider2D confinderCollider;
  [SerializeField] CanvasGroup worldMap;
  [SerializeField] CinemachineVirtualCamera WorldVcam;
  private DisplayInventory _displayInventory;
  [SerializeField] RectTransform worldMapImage;
  [SerializeField] Transform WorldCamTracker;

  [Header("UI SC")]
  [SerializeField] UIStateSystem InventoryManger;
  private bool inventoryUIState = false;

  [Header("Save Load")]
  private bool isLoaded = false;

  [Header("KeyTest")]
  private PlayerInput playerInput;
  private InputAction jumpAction;
  private InputAction DashAction;
  private InputAction movementAction;
  private InputAction ClimbAction;
  private InputAction AttackAction;
  private InputAction ImmortalAction;
  private InputAction WorldMapAction;
  private InputAction BlinkAction;
  private InputAction NextLevelAction;
  private InputAction RangeSkillAction;
  private InputAction PinCreateAction;
  private InputAction ZoomMapAction;
  private InputAction PanMapAction;
  private InputAction OpenInventoryAction;
  private InputAction SaveLoadAction;

  private void Awake()
  {
    playerInput = GetComponent<PlayerInput>();
    jumpAction = playerInput.actions["Jump"];
    DashAction = playerInput.actions["Dash"];
    ClimbAction = playerInput.actions["Climb"];
    movementAction = playerInput.actions["Movement"];
    AttackAction = playerInput.actions["Attack"];
    ImmortalAction = playerInput.actions["Immortal"];
    WorldMapAction = playerInput.actions["Map"];
    BlinkAction = playerInput.actions["Blink"];
    RangeSkillAction = playerInput.actions["Castling"];
    NextLevelAction = playerInput.actions["NextLevel"];
    PinCreateAction = playerInput.actions["Pin create"];
    ZoomMapAction = playerInput.actions["Zoom Map"];
    PanMapAction = playerInput.actions["Pan Map"];
    OpenInventoryAction = playerInput.actions["Open Inventory"];
    SaveLoadAction = playerInput.actions["Save and Load"];
  }
  private void OnEnable()
  {
    movementAction.performed += Run;
    movementAction.canceled += Run;
    ClimbAction.performed += Climb;
    ClimbAction.canceled += Climb;
    DashAction.performed += Dash;
    movementAction.performed += Run;
    AttackAction.performed += Attack;
    jumpAction.performed += Jump;
    ImmortalAction.performed += CounterSpear;
    WorldMapAction.performed += CameraCheck;
    BlinkAction.performed += FlameBlink;
    RangeSkillAction.performed += RangeSkill;
    NextLevelAction.performed += ExitLevel;
    PinCreateAction.performed += CreatePin;
    ZoomMapAction.performed += ZoomMap;
    ZoomMapAction.canceled += ZoomMap;
    PanMapAction.performed += WorldMapPan;
    PanMapAction.canceled += WorldMapPan;
    OpenInventoryAction.performed += InventoryUIStateChange;
    SaveLoadAction.performed += SaveLoad;
  }



  private void OnDisable()
  {
    movementAction.performed -= Run;
    movementAction.canceled -= Run;
    ClimbAction.performed -= Climb;
    ClimbAction.canceled -= Climb;
    DashAction.performed -= Dash;
    AttackAction.performed -= Attack;
    jumpAction.performed -= Jump;
    ImmortalAction.performed -= CounterSpear;
    WorldMapAction.performed -= CameraCheck;
    BlinkAction.performed -= FlameBlink;
    RangeSkillAction.performed -= RangeSkill;
    NextLevelAction.performed -= ExitLevel;
    PinCreateAction.performed -= CreatePin;
    ZoomMapAction.performed -= ZoomMap;
    ZoomMapAction.canceled -= ZoomMap;
    PanMapAction.performed -= WorldMapPan;
    PanMapAction.canceled -= WorldMapPan;
    OpenInventoryAction.performed -= InventoryUIStateChange;
    SaveLoadAction.performed -= SaveLoad;
  }
  // Start is called before the first frame update
  void Start()
  {

    _lineRenderer = GetComponent<LineRenderer>();
    _lineRenderer.enabled = false;
    myRigidBody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myBoxCollider2D = GetComponent<BoxCollider2D>();
    myPolygonCollider2D = GetComponent<PolygonCollider2D>();
    myAudioSource = GetComponent<AudioSource>();
    trailRenderer = GetComponent<TrailRenderer>();
    myTransform = GetComponent<Transform>();
    startingGravityScale = myRigidBody2D.gravityScale;
    myAnimator.SetTrigger("DoorOut");
  }

  // Update is called once per frame
  void Update()
  {
    if (!stun)
    {
      //RangeSkill();
      //FlameBlink();
      //Run();
      //Dash();
      //Jump();
      //Climb();
      //Attack();
      //CameraCheck();


      if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("enemy")))
      {
        PlayerHit();
      }
      //ExitLevel();
    }
  }

  private void SaveLoad(InputAction.CallbackContext _)
  {
    //default isLoaded :false => Load ->Save ->Load ->Save ...
    if (isLoaded)
    {
      Debug.Log("saved");
      inventory.Save();
    }
    else
    {
      Debug.Log("Loaded");
      inventory.Load();
    }
    isLoaded = !isLoaded;
  }
  private void InventoryUIStateChange(InputAction.CallbackContext _)
  {
    inventoryUIState = !inventoryUIState;
    if (inventoryUIState)
    {
      playerInput.actions.FindActionMap("UI").Enable();
    }
    else
    {
      playerInput.actions.FindActionMap("UI").Disable();
    }

    InventoryManger.ChangeUIState();
  }
  private void OnTriggerEnter2D(Collider2D collison)
  {
    if (collison.TryGetComponent<GroundItem>(out GroundItem item))
    {
      inventory.AddItem(new Item(item.item), 1);
      Destroy(collison.gameObject);
    }
  }
  private void OnApplicationQuit()
  {
    inventory.Container.Clear();
    equipmentInventory.Container.Clear();
  }
  private void WorldMapPan(InputAction.CallbackContext ctx)
  {
    if (ctx.ReadValue<float>() != 0 && mapOn)
    {
      StartCoroutine(PanWorldMap(WorldCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
    }

  }

  private IEnumerator PanWorldMap(Vector3 initialMousePosition)
  {

    Vector3 differencePos;
    while (PanMapAction.ReadValue<float>() != 0)
    {

      if (Physics2D.IsTouching(cameraCollider, confinderCollider))
      {
        differencePos = WorldCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - initialMousePosition;
        WorldCamTracker.position = WorldCamTracker.position - differencePos / 20;
        yield return waitForFixedUpdate;
      }
      else
      {
        Debug.Log("Caught");
        yield return waitForFixedUpdate;
      }


    }
  }

  private void ZoomMap(InputAction.CallbackContext ctx)
  {
    Vector2 ZoomCoordinate = ctx.ReadValue<Vector2>();
    if (mapOn && ZoomCoordinate.y != 0f)
    {
      if (ZoomCoordinate.y < 0 && WorldVcam.m_Lens.OrthographicSize < 11)
      {
        WorldVcam.m_Lens.OrthographicSize++;
      }
      else if (ZoomCoordinate.y > 0 && WorldVcam.m_Lens.OrthographicSize > 5)
      {
        WorldVcam.m_Lens.OrthographicSize--;
      }
    }

  }
  private void CreatePin(InputAction.CallbackContext _)
  {
    if (mapOn)
    {
      if (newMapPin != null)
      {
        Addressables.Release(MapPinHandle);
        Destroy(newMapPin);
      }
      else
      {
        Addressables.LoadAssetAsync<GameObject>("Map Marker").Completed +=
        (AsyncOperationHandle<GameObject> Obj) =>
        {
          MapPinHandle = Obj;
          newMapPin = Obj.Result;
          //newPinPos = WorldCamera.ViewportToWorldPoint(Mouse.current.position.ReadValue());
          newPinPos = WorldCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
          Vector3 newPinPos2 = WorldCamera.ScreenToViewportPoint(Mouse.current.position.ReadValue());
          // Ray ray = WorldCamera.ScreenPointToRay(newPinPos);

          // if (hit = Physics2D.Raycast(newPinPos, transform.forward, Mathf.Infinity))
          // {
          //   //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);
          //   Debug.Log("newPinPos == " + newPinPos);
          //   Debug.Log("hit point ray == " + hit.point);
          // }
          // else
          // {
          //   Debug.Log("반응없음 왜지?");
          // }
          //newPinPos = Mouse.current.position.ReadValue();
          // mouseTracker = new Vector2(newPinPos.x, newPinPos.y);
          // Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(WorldCamera, worldMapImage.position);
          // Debug.Log("screeenPos == " + screenPos);
          // //RectTransformUtility.ScreenPointToLocalPointInRectangle(worldMapImage, mouseTracker, WorldCamera, out newPinPosTracker);
          // RectTransformUtility.ScreenPointToLocalPointInRectangle(worldMapImage, screenPos, WorldCamera, out newPinPosTracker);
          float worldCameraHeight = WorldCamera.orthographicSize * 2f;
          float worldCameraWidth = worldCameraHeight * WorldCamera.aspect;
          Vector3 worldCameraCenter = WorldCamera.transform.position;
          float ratioAdujust = 1 / 1.03f;
          //Debug.Log("world camera  width : " + worldCameraWidth + " height " + worldCameraHeight);
          Debug.Log("카메라 상대좌표 (비보정) == " + newPinPos2);
          //Debug.Log("카메라 상대좌표 (보정) == " + newPinPos2 * ratioAdujust);
          //Debug.Log(" 월드맵 카메라 중앙 위치 : " + worldCameraCenter);
          // Debug.Log("newPinPosTracker == " + newPinPosTracker);
          //newPinPos = UiCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
          //newPinPos = new Vector3(newPinPosTracker.x, newPinPosTracker.y, 0);
          //Debug.Log("월드캠 기준 위치 : " + WorldCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + "메인캠 기준 위치 : " + mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
          //newPinPos = new Vector3(newPinPosTracker.x, newPinPosTracker.y, 0);
          newPinPos = new Vector3(worldCameraCenter.x + ((newPinPos2 * ratioAdujust).x - 0.5f) * worldCameraWidth, WorldCamera.transform.position.y + ((newPinPos2 * ratioAdujust).y - 0.5f) * worldCameraHeight, 0);
          newMapPin = Instantiate(newMapPin, newPinPos, Quaternion.identity);
        };
      }

    }
  }
  private void moveWorldCamera()
  {

  }
  public void CameraCheck(InputAction.CallbackContext _)
  {

    mapOn = !mapOn;
    if (mapOn)
    {
      worldMap.alpha = 1;
    }
    else
    {
      WorldCamTracker.position = myTransform.position;
      WorldVcam.m_Lens.OrthographicSize = 6;
      worldMap.alpha = 0;

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
  private void ExitLevel(InputAction.CallbackContext ctx)
  {
    if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Interact")))
    {
      return;
    }
    if (ctx.performed)
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

  private void CounterSpear(InputAction.CallbackContext _)
  {
    //bool csInput = CrossPlatformInputManager.GetButtonDown("CS");
    CS = !CS;
    Physics2D.IgnoreLayerCollision(8, 10, CS);
  }
  private void Dash(InputAction.CallbackContext _)
  {
    //bool dashInput = CrossPlatformInputManager.GetButtonDown("Dash");


    //Vector3 mousePos = mainCamera.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
    Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    charPos = GameObject.Find("Player").transform.position;
    //Debug.Log("마우스 위치: " + mousePos + "  사람위치 : " + charPos);
    mousePos.z = 0f;
    charPos.z = 0f;
    dashVector = (mousePos - charPos);
    isDashing = true;

    trailRenderer.emitting = true;
    dashingDir = dashVector;
    //dashingDir = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
    if (dashingDir == Vector2.zero)
    {
      dashingDir = new Vector2(transform.localScale.x, 0);
    }
    myRigidBody2D.drag = 10;
    Physics2D.IgnoreLayerCollision(8, 10, true);
    Physics2D.IgnoreLayerCollision(8, 11, true);
    myAnimator.SetBool("Run", isDashing);
    myRigidBody2D.velocity = dashingDir.normalized * dashingSpeed;
    StartCoroutine(StopDashing());


  }

  private IEnumerator StopDashing()
  {
    yield return new WaitForSeconds(dashingTime);
    Physics2D.IgnoreLayerCollision(8, 10, false);
    Physics2D.IgnoreLayerCollision(8, 11, false);
    trailRenderer.emitting = false;
    isDashing = false;
    myRigidBody2D.velocity = Vector2.zero;
    myRigidBody2D.drag = 0;
    myAnimator.SetBool("Run", isDashing);
  }
  private void Run(InputAction.CallbackContext ctx)
  {
    Vector2 inputHorizontalCoordinate = ctx.ReadValue<Vector2>();
    //float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    Vector2 playerVelocity = new Vector2(inputHorizontalCoordinate.x * runSpeed, myRigidBody2D.velocity.y);
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
  private void Jump(InputAction.CallbackContext _)
  {
    //bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
    //Debug.Log("점프 누름 ");
    if (!onGround() && jumpCnt == 2)
    {
      return;
    }
    Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
    myRigidBody2D.velocity = jumpVelocity;
    jumpCnt++;




  }

  private void Climb(InputAction.CallbackContext ctx)
  {
    Vector2 inputVerticalCoordinate = ctx.ReadValue<Vector2>();
    if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))
    {
      //float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
      Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, inputVerticalCoordinate.y * climbSpeed);
      myRigidBody2D.velocity = climbVelocity;
      myRigidBody2D.gravityScale = 0f;
    }
    else
    {
      myRigidBody2D.gravityScale = startingGravityScale;
    }
    ChangeStateToClimb();
  }

  private void Attack(InputAction.CallbackContext ctx)
  {
    //temporary since attack and map pin create uses same key
    if (mapOn) return;

    myAnimator.SetTrigger("Attack");
    myAudioSource.PlayOneShot(attackSFX);
    Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRange, LayerMask.GetMask("enemy"));

    foreach (Collider2D enemy in enemiesHit)
    {
      enemy.GetComponent<Enemy>().Dying();
    }


  }
  private void RangeSkill(InputAction.CallbackContext ctx)
  {

    if (range_skill_activated && newRangeSkill != null)
    {
      newRangeSkill.transform.position = GameObject.Find("Player").transform.position;
      dropPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      dropPos = new Vector3(dropPos.x, dropPos.y, 0);
      newDropSkill.transform.position = dropPos;
      if (skillRangeDetecter(newRangeSkill.transform.localScale.x / 2, (GameObject.Find("Player").transform.position - dropPos).magnitude))
      {
        newDropSkill.GetComponent<SpriteRenderer>().color = new Color(0.1342f, 0.866f, 0f, 0.169f);
      }
      else
      {
        newDropSkill.GetComponent<SpriteRenderer>().color = new Color(1f, 0.023f, 0.023f, 0.169f);
      }
    }


    if (range_skill_activated)
    {
      Addressables.Release(skillHandle);
      Destroy(newRangeSkill);
      Addressables.Release(dropSkillHandle);
      Destroy(newDropSkill);
      if (castling_possible)
      {
        Vector3 tmpPos = HitAreaRangeSkill.lockedTarget.transform.position;
        HitAreaRangeSkill.lockedTarget.transform.position = GameObject.Find("Player").transform.position;
        GameObject.Find("Player").transform.position = tmpPos;
        HitAreaRangeSkill.lockedTarget.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      }
    }
    else
    {
      Addressables.LoadAssetAsync<GameObject>("Skill Range").Completed +=
      (AsyncOperationHandle<GameObject> Obj) =>
      {
        skillHandle = Obj;
        range_obj = Obj.Result;
        newRangeSkill = Instantiate(range_obj, GameObject.Find("Player").transform.position, Quaternion.identity);
      };
      Addressables.LoadAssetAsync<GameObject>("Skill Drop Point").Completed +=
      (AsyncOperationHandle<GameObject> Obj) =>
      {
        dropSkillHandle = Obj;
        drop_skill_obj = Obj.Result;
        dropPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        dropPos = new Vector3(dropPos.x, dropPos.y, 0);
        newDropSkill = Instantiate(drop_skill_obj, dropPos, Quaternion.identity);


      };
    }
    range_skill_activated = !range_skill_activated;

  }

  private bool skillRangeDetecter(float skillRange, float skillDropRange)
  {
    if (skillRange > skillDropRange)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  private void FlameBlink(InputAction.CallbackContext _)
  {
    //bool isFB = CrossPlatformInputManager.GetButtonDown("FB");

    if (FB_activated)
    {
      _lineRenderer.SetPosition(1, GameObject.Find("Player").transform.position);
      if ((_lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1)).magnitude > 10f)
      {
        _lineRenderer.enabled = false;
      }
      else
      {
        _lineRenderer.enabled = true;
      }
    }

    //Debug.Log("q 눌렀음 ");
    //Debug.Log("포탈 설치 여부 : " + FB_activated);
    if (FB_activated)
    {
      GameObject.Find("Player").transform.position = fbPos;
      //Debug.Log("포탈로 이동 " + fbPos);
      _lineRenderer.enabled = false;
      fbPos = new(0f, 0f, 0f);
      Addressables.Release(Handle);
      Destroy(newBlink);

    }
    else
    {
      Addressables.LoadAssetAsync<GameObject>("Magic Box").Completed +=
      (AsyncOperationHandle<GameObject> Obj) =>
      {
        Handle = Obj;
        FB_obj = Obj.Result;
        fbPos = GameObject.Find("Player").transform.position;
        newBlink = Instantiate(FB_obj, fbPos, Quaternion.identity);
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, fbPos);
        _lineRenderer.SetPosition(1, fbPos);
        _lineRenderer.enabled = true;

        //Debug.Log("매직 박스 : " + FB_obj);
        //Debug.Log("포탈 생성 위치 " + fbPos);
      };
    }
    FB_activated = !FB_activated;

  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(hurtBox.position, attackRange);
  }
}

