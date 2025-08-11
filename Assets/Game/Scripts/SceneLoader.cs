using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public UIFader fader;

    public bool IsLoading { get; private set; }
    public string CurrentScene { get; private set; }
    public float FadeDuration { get; set; }

    private PauseService pauseService;


    public SceneLoader(PauseService pauseService)
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        this.pauseService = pauseService;
    }


    public async void LoadScene(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
            await Task.Yield();

        asyncLoad.allowSceneActivation = true;

        CurrentScene = sceneName;

        return;
    }

    public async void LoadSceneWithFade(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        var sceneFade = fader.FadeIn();
        pauseService.LockInterface();
        pauseService.StopGame();

        while (asyncLoad.progress < 0.9f)
            await Task.Yield();

        await sceneFade;

        fader = null;
        asyncLoad.allowSceneActivation = true;

        await Task.Yield();

        sceneFade = fader.FadeOut();

        await sceneFade;

        pauseService.UnlockInterface();
        pauseService.ResumeGame();

        CurrentScene = sceneName;

        return;
    }

}
