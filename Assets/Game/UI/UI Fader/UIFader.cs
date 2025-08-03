using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class UIFader : MonoBehaviour
{
    [SerializeField] private GameObject animRoot;

    public bool isFading { get; private set; } = false;


    [Inject] public void Construct(SceneLoader sceneLoader)
    {
        sceneLoader.fader = this;
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
        if (isFading) { Debug.Log("Not allow to fade"); return; }

        isFading = true;
        animRoot.GetComponent<Animator>().SetBool("is_fade", true);
        while (isFading)
        {
            await Task.Yield();
        }
    }

    public async Task FadeOut()
    {
        if (isFading) { Debug.Log("Not allow to fade"); return; }

        isFading = true;
        animRoot.GetComponent<Animator>().SetBool("is_fade", false);
        while (isFading)
        {
            await Task.Yield();
        }
    }

    private void OnFaderHided()
    {
        isFading = false;
    }

    private void OnFaderShowed()
    {
        isFading = false;
    }

}
