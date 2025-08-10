using System;
using UnityEngine;
using UnityEngine.Events;

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


    [SerializeField] private float PushForceMult = 300f;
    [SerializeField] AnimationCurve curveJumpValue;

    [SerializeField] private float solidWallBounce = 10f;
    [SerializeField] private float limitBounceDown = 10f;
    [SerializeField] private float bounceFactor = 0.75f;
    [SerializeField] private float minWallBounce = .1f;

    public Vector2 preVelocity = Vector2.zero;
    public Vector2 prePosition = Vector2.zero;
    public bool isGrounded = false;

    private SwapCalculation swapCalculation;
    private Rigidbody2D playerBody;
    private float absoluteGravity;


    public bool IsNegativeGravity 
    {
        get { return Mathf.Sign(playerBody.gravityScale) < 0; }
        set { playerBody.gravityScale = value ? -absoluteGravity : absoluteGravity; }
    }

    public bool IsFreezeGravity 
    {
        get { return Mathf.Sign(playerBody.gravityScale) == 0f; }
        set { playerBody.gravityScale = value ? 0 : absoluteGravity; }
    }

    public bool IsFreezeVelosity 
    {
        get { return Mathf.Sign(playerBody.velocity.magnitude) == 0f; }
        set 
        {
            playerBody.velocity = value ? Vector2.zero : preVelocity;
        }
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
    }

    private void OnDisable()
    {
        swapCalculation.eSwapContinued.RemoveListener(DifferenceCheck);
        swapCalculation.eSwapEnded.RemoveListener(BodyPush);
        swapCalculation.eSwapBlocked.RemoveListener(JumpBugCrutchFix);
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
        isGrounded = false;
        swapCalculation.IsEnabled = isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JumpTrapCheck(collision);
    }


    public void JumpBugCrutchFix() 
    {
        if (playerBody.velocity.magnitude == 0 && preVelocity.magnitude == 0)
        {
            isGrounded = true;
            swapCalculation.IsEnabled = isGrounded;
            eDisplayGrounded?.Invoke();
        }
        //else 
        //{
        //    Debug.Log(playerBody.velocity.magnitude.ToString() + "  " + preVelocity.magnitude.ToString());
        //}
    }

    public void SetSwapActive(bool flag) 
    {
        swapCalculation.IsEnabled = flag;
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
        swapCalculation.IsEnabled = isGrounded;

        playerBody.velocity = Vector2.zero;
        playerBody.gravityScale = absoluteGravity;
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
        swapCalculation.IsEnabled = isGrounded;

        eDisplayFlip?.Invoke(preVelocity.x < 0);
        eDisplayGrounded?.Invoke();
        }

    private void InclineReact(GameObject gm) 
    {
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
                playerBody.velocity = new Vector2(-sign * Mathf.Clamp(Mathf.Abs(preVelocity.x) / 2, minWallBounce, solidWallBounce), preVelocity.y / 2);
                eDisplayWallHit?.Invoke();
                break;
        }
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

        eDisplayRoofHit?.Invoke();
    }


    #region JUMPTRAP_FUNCTIONS

    private void JumpTrapCheck(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("JumpTrap"))
        {
            isGrounded = true;
            swapCalculation.IsEnabled = isGrounded;

            swapCalculation.IsFullHorizon = true;
            playerBody.gravityScale = 0;
            playerBody.velocity = Vector2.zero;
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
        swapCalculation.IsEnabled = isGrounded;
    }

    #endregion

}