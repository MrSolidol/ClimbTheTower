using UnityEngine;
using UnityEngine.UI;

public class PointerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject rotateRoot;
    [SerializeField] private GameObject scaleRoot;
    [SerializeField] private Image pointerImage;

    private SwapCalculation swapCalculation;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        rotateRoot.SetActive(false);

        swapCalculation = playerObject.GetComponent<SwapCalculation>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        swapCalculation.eSwapContinued.AddListener(OnSwapContinued);
        swapCalculation.eSwapEnded.AddListener(OnSwapEnded);
    }

    private void OnDisable()
    {
        swapCalculation.eSwapContinued.RemoveListener(OnSwapContinued);
        swapCalculation.eSwapEnded.RemoveListener(OnSwapEnded);
    }


    private void OnSwapContinued(Vector2 vec, float value, bool flag) 
    {
        if (flag)
        {
            rotateRoot.SetActive(true);

            scaleRoot.transform.localScale = new Vector3(1f, 1f, 1f) * Mathf.Lerp(0.25f, 0.75f, value);

            pointerImage.color = new Color(1, 1, 1, value);

            var rotated = rotateRoot.transform.eulerAngles;
            rotateRoot.transform.eulerAngles = new Vector3(rotated.x, rotated.y, GetAngle(vec));

            transform.position = playerObject.transform.position;
        }
        else
        {
            rotateRoot.SetActive(false);
        }
    }

    private void OnSwapEnded(Vector2 vec, float value, bool flag) 
    {
        rotateRoot.SetActive(false);
    }

    private float GetAngle(Vector2 normVec)
    {
        float angleRad = Mathf.Atan2(normVec.x, -normVec.y);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        if (angleDeg < 0)
            angleDeg += 360;
        
        return angleDeg;
    }

}
