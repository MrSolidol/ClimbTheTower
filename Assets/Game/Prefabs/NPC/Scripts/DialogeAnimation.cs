using UnityEngine;
using Zenject;

public class DialogeAnimation : MonoBehaviour
{
    [SerializeField] private Animator dialogeAnimator;
    [SerializeField] private DialogeWriter dialogeWriter;

    [Inject] private PauseService pauseService;


    private void Awake()
    {
        dialogeWriter.eDisplayTalk.AddListener(SetParameters);
    }

    private void OnEnable()
    {
        pauseService.eGameStopped.AddListener(OnStoppedGame);
    }

    private void OnDisable()
    {
        pauseService.eGameStopped.RemoveListener(OnStoppedGame);
    }


    private void OnStoppedGame(bool flag) 
    {
        if (flag)
        { dialogeAnimator.speed = 0f; }
        else
        { dialogeAnimator.speed = 1f; }
    }

    private void SetParameters(bool flag) 
    {
        dialogeAnimator.SetBool("is_talk", flag);
    }
}
