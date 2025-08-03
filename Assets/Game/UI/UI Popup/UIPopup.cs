using System.Threading.Tasks;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private string popupName;
    [SerializeField] private GameObject animRoot;

    private UIPopupHandler handler;
    private Animator animator;
    private bool isAnimPlay = true;

    public string PopupName { get { return popupName; } }
    

    private void Awake()
    {
        handler = animRoot.GetComponent<UIPopupHandler>();
        animator = animRoot.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        handler.ePopupShowed.AddListener(OnPopupShowed);
        handler.ePopupHided.AddListener(OnPopupHided);
    }

    private void OnDisable()
    {
        handler.ePopupShowed.RemoveListener(OnPopupShowed);
        handler.ePopupHided.RemoveListener(OnPopupHided);
    }


    private void OnPopupHided()
    {
        animRoot.SetActive(false);
        isAnimPlay = false;
    }

    private void OnPopupShowed()
    {
        isAnimPlay = false;
    }

    public async Task Show()
    {
        animRoot.SetActive(true);
        isAnimPlay = true;

        while (isAnimPlay)
        {
            await Task.Yield();
        }
    }

    public async Task Hide()
    {
        animator.SetTrigger("hide");
        isAnimPlay = true;

        while (isAnimPlay)
        {
            await Task.Yield();
        }
    }

}
