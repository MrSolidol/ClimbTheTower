using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class UIFader : MonoBehaviour
{
    private const float ANIMATION_DURATION = 2.5f;

    [SerializeField] private GameObject animRoot;
    [SerializeField] private float fadeDuration = 2.5f; 

    public bool IsFading { get; private set; } = false;
    public float FadeDuration
    {
        get { return fadeDuration; }
        set 
        {
            animRoot.GetComponent<Animator>().speed = ANIMATION_DURATION/value;     
        }
    }


    [Inject] public void Construct(SceneLoader sceneLoader)
    {
        sceneLoader.fader = this;
    }


    private void Awake()
    {
        animRoot.GetComponent<Animator>().speed = ANIMATION_DURATION/fadeDuration;
    }

    private void OnEnable()
    {
        animRoot.GetComponent<UIFaderHandler>().eFaderShowed.AddListener(OnFaderShowed);
        animRoot.GetComponent<UIFaderHandler>().eFaderHided.AddListener(OnFaderHided);
    }

    private void OnDisable()
    {
        animRoot.GetComponent<UIFaderHandler>().eFaderShowed.RemoveListener(OnFaderShowed);
        animRoot.GetComponent<UIFaderHandler>().eFaderHided.RemoveListener(OnFaderHided);
    }


    public async Task FadeIn()
    {
        if (IsFading) { Debug.Log("Not allow to fade"); return; }

        IsFading = true;
        animRoot.GetComponent<Animator>().SetBool("is_fade", true);
        while (IsFading)
        {
            await Task.Yield();
        }
    }

    public async Task FadeOut()
    {
        if (IsFading) { Debug.Log("Not allow to fade"); return; }

        IsFading = true;
        animRoot.GetComponent<Animator>().SetBool("is_fade", false);
        while (IsFading)
        {
            await Task.Yield();
        }
    }

    private void OnFaderHided()
    {
        IsFading = false;
    }

    private void OnFaderShowed()
    {
        IsFading = false;
    }

}
