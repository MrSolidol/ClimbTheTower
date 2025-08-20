using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    #region AUDIO_AND_VISUAL_EVENTS
    [HideInInspector] public UnityEvent<bool> eDisplayFlip;
    [HideInInspector] public UnityEvent<float> eDisplaySpring;
    [HideInInspector] public UnityEvent eDisplayJump;
    [HideInInspector] public UnityEvent eDisplayTrap;
    [HideInInspector] public UnityEvent eDisplayWallHit;
    [HideInInspector] public UnityEvent eDisplayRoofHit;
    [HideInInspector] public UnityEvent eDisplaySlide;
    [HideInInspector] public UnityEvent eDisplayGrounded;
    [HideInInspector] public UnityEvent<Vector2> eDisplayShake;

    [HideInInspector] public UnityEvent<string> ePlaySound;

    #endregion

    #region DISSPLATFORM_VARIABLES
    private DisappearPlatform disPlatform;
    #endregion

    [HideInInspector] public UnityEvent eFloorContact;

    [SerializeField] private float PushForceMult = 300f;
    [SerializeField] AnimationCurve curveJumpValue;

    [SerializeField] private float limitBounceDown = 10f;
    [SerializeField] private float bounceFactor = 0.75f;
    [SerializeField] private float minWallBounce = 1.5f;

    [Inject] PauseService pauseService;

    public Vector2 preVelocity = Vector2.zero;
    public Vector2 prePosition = Vector2.zero;
    public bool isGrounded = false;

    private SwapCalculation swapCalculation;
    private Rigidbody2D playerBody;
    private float absoluteGravity;
    private bool isFreezed = false;
    private bool isSleeped = false;
    private Vector2 rememberedVelocity = Vector2.zero;

    public bool IsNegativeGravity 
    {
        get { return Mathf.Sign(playerBody.gravityScale) < 0; }
        set { playerBody.gravityScale = value ? -absoluteGravity : absoluteGravity; }
    }


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        swapCalculation = GetComponent<SwapCalculation>();

        absoluteGravity = playerBody.gravityScale;
    }

    private void OnEnable()
    {
        swapCalculation.eSwapContinued.AddListener(DifferenceCheck);
        swapCalculation.eSwapEnded.AddListener(BodyPush);
        swapCalculation.eSwapBlocked.AddListener(JumpBugCrutchFix);
        pauseService.eGameStopped.AddListener(OnGameStopped);
    }

    private void OnDisable()
    {
        swapCalculation.eSwapContinued.RemoveListener(DifferenceCheck);
        swapCalculation.eSwapEnded.RemoveListener(BodyPush);
        swapCalculation.eSwapBlocked.RemoveListener(JumpBugCrutchFix);
        pauseService.eGameStopped.RemoveListener(OnGameStopped);
    }

    private void Update()
    {

        #region CONTACT_UPDATE

            preVelocity = playerBody.velocity;
            prePosition = playerBody.position;
        
        #endregion

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        eDisplayShake?.Invoke(preVelocity);
        switch (collision.transform.parent.tag) 
        {
            case "STONE":
                ePlaySound?.Invoke("STONE");
                break;
            case "WOOD":
                ePlaySound?.Invoke("WOOD");
                break;
            case "METAL":
                ePlaySound?.Invoke("METAL");
                break;
            default:
                break;
        } 

        Vector2 _vecContact = collision.contacts[0].normal;
        float _degContact = Vector2.Angle(Vector2.up, _vecContact);

        switch (_degContact)
        {
            case <=2.5f and >= 0:
                FloorReact(collision.gameObject);
                break;
            case <=92.5f and >= 87.5f:
                WallReact(collision.gameObject);
                break;
            case >= 177.5f:
                RoofReact(collision.gameObject);
                break;
            default:
                InclineReact(collision.gameObject);
                break;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isSleeped) { Debug.Log("Incline exit"); isSleeped = false; return; }

        if (isGrounded && collision.gameObject.CompareTag("SLIPPERY")) 
        {
            isGrounded = false;
            swapCalculation.enabled = false;
            eDisplaySlide?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JumpTrapCheck(collision);
    }


    public void JumpBugCrutchFix()
    {
        if (!isGrounded && playerBody.gravityScale != 0 && playerBody.velocity.magnitude == 0 && preVelocity.magnitude == 0)
        {
            isGrounded = true;
            swapCalculation.enabled = isGrounded;
            eDisplayGrounded?.Invoke();
        }
    }

    public void SetSwapActive(bool flag) 
    {
        swapCalculation.enabled = flag;
    }


    private void OnGameStopped(bool flag) 
    {
        if (flag)
        { 
            FreezePlayer();
            swapCalculation.enabled = false;
        }
        else 
        { 
            UnFreezePlayer(true);
            swapCalculation.enabled = true;
        }
    }

    private void DifferenceCheck(Vector2 vec, float value, bool flag)
    {
        if (flag) 
            eDisplaySpring?.Invoke(value);
        else
            eDisplaySpring?.Invoke(0);
    }

    private void BodyPush(Vector2 vec, float value, bool flag) 
    {
        if (!flag) { return; }
        
        if (!isGrounded) { return; }

        value = curveJumpValue.Evaluate(value);
        
        isGrounded = false;
        swapCalculation.enabled = isGrounded;

        UnFreezePlayer(false);

        playerBody.velocity = Vector2.zero;
        playerBody.AddForce(vec * value * -PushForceMult);

        eDisplayFlip?.Invoke(vec.x < 0);
        eDisplayJump?.Invoke();
        eDisplaySpring?.Invoke(0);

        if (disPlatform != null)
        {
            disPlatform.eSpawn.RemoveListener(DisPlatformControl);
            disPlatform = null;
        }
    }

    private void FloorReact(GameObject gm)
    {
        switch (gm.tag)
        {
            case "SOLID":
                playerBody.velocity = new Vector2(0, playerBody.velocity.y);
                break;
            case "BOUNCY":
                if (preVelocity.y < -limitBounceDown)
                {
                    playerBody.velocity = new Vector2(preVelocity.x, -preVelocity.y*bounceFactor);
                    return;
                }
                break;
            case "DisappearPlatform":
                disPlatform = gm.GetComponent<DisappearPlatform>();
                disPlatform.eSpawn.AddListener(DisPlatformControl);
                break;
        }

        isGrounded = true;
        swapCalculation.enabled = isGrounded;
        isSleeped = false;

        eFloorContact?.Invoke();

        eDisplayFlip?.Invoke(preVelocity.x < 0);
        eDisplayGrounded?.Invoke();
        }

    private void InclineReact(GameObject gm) 
    {
        Debug.Log("Incline");
        if (isGrounded) { isSleeped = false; return; }
        isSleeped = true;
        eDisplaySlide?.Invoke();
    }

    private void WallReact(GameObject gm)
    {
        string _tag = gm.tag;

        switch (_tag)
        {
            case ("BOUNCY"):
                playerBody.velocity = new Vector2(-preVelocity.x, preVelocity.y);
                break;
            default:
                var sign = Mathf.Sign(preVelocity.x);
                playerBody.velocity = new Vector2(-sign * minWallBounce, preVelocity.y / 2);
                eDisplayWallHit?.Invoke();
                break;
        }
        isSleeped = false;
        eDisplayFlip?.Invoke(playerBody.velocity.x < 0);
    }

    private void RoofReact(GameObject gm) 
    {
        string _tag = gm.tag;
        switch (_tag) 
        {
            case "OneWayPlatform":
                return;
            default:
                playerBody.velocity = new Vector2(preVelocity.x/4, 0);
                break;
        }

        isSleeped = false;
        eDisplayRoofHit?.Invoke();
    }

    private void FreezePlayer() 
    {
        if (isFreezed) { return; }
        isFreezed = true;
        playerBody.gravityScale = 0;
        rememberedVelocity = playerBody.velocity;
        playerBody.velocity = Vector2.zero;
    }

    private void UnFreezePlayer(bool keepVelocity) 
    {
        if (!isFreezed) { return; }
        isFreezed = false;
        if (keepVelocity) 
        {
            playerBody.velocity = rememberedVelocity;
        }
        playerBody.gravityScale = absoluteGravity;
    }


    #region JUMPTRAP_FUNCTIONS

    private void JumpTrapCheck(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("JumpTrap"))
        {
            isGrounded = true;
            swapCalculation.enabled = isGrounded;

            swapCalculation.IsFullHorizon = true;
            FreezePlayer();
            transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y);

            ePlaySound?.Invoke("WEBB");
            eDisplayTrap?.Invoke();
        }
    }

    #endregion

    #region DISSPLATFORM_FUNCTIONS
    public void DisPlatformControl()
    {
        isGrounded = false;
        swapCalculation.enabled = isGrounded;
    }

    #endregion

}