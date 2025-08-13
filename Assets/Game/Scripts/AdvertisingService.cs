using UnityEngine;
using Zenject;

public enum AdvertisingMode 
{
    None,
    Yandex,
    Google
}

public class AdvertisingService : MonoBehaviour
{
    [SerializeField] private float advertisingCooldown = 300f;
    [SerializeField] private AdvertisingMode advertisingMode;

    [Inject] private PlayerAnimations playerAnimations;



    private void Awake()
    {
        Invoke("SetKnockStatus", advertisingCooldown);
    }

    private void OnEnable()
    {
        playerAnimations.ePlayerKnocked.AddListener(ShowAdvertising);
    }

    private void OnDisable()
    {
        playerAnimations.ePlayerKnocked.AddListener(ShowAdvertising);
    }


    private void SetKnockStatus() 
    {
        playerAnimations.IsKnockable = true;
    }

    private void ShowAdvertising() 
    {
        Debug.Log("Advertising showing!");

        playerAnimations.IsKnockable = false;
        Invoke("SetKnockStatus", advertisingCooldown);
    }
}
