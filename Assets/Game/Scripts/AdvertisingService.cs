using UnityEngine;
using Zenject;
using YG;

public enum AdvertisingMode
{
    None,
    Yandex,
    Google
}

public class AdvertisingService : MonoBehaviour
{
    [SerializeField] private UIMenuBook menuBook;
    [SerializeField] private AdvertisingTimer adTimer;
    [SerializeField] private float advertisingCooldown = 300f;
    [SerializeField] private AdvertisingMode advertisingMode;

    [Inject] private PlayerAnimations playerAnimations;
    [Inject] private PauseService pauseService;

    private void Awake()
    {
        Invoke("SetKnockStatus", advertisingCooldown);
    }

    private void OnEnable()
    {
        playerAnimations.ePlayerKnocked.AddListener(ShowAdvertising);
        adTimer.eWarningTimeout.AddListener(YandexAd);
    }

    private void OnDisable()
    {
        playerAnimations.ePlayerKnocked.AddListener(ShowAdvertising);
        adTimer.eWarningTimeout.RemoveListener(YandexAd);
    }


    private void SetKnockStatus()
    {
        playerAnimations.IsKnockable = true;
    }

    private void ShowAdvertising()
    {
        menuBook.SetNewPopup("Advertising");
        menuBook.SetPause(true);
        //pauseService.LockInterface();
    }

    public void YandexAd()
    {
        YG2.InterstitialAdvShow();
        playerAnimations.IsKnockable = false;
        Invoke("SetKnockStatus", advertisingCooldown);

    }
}
