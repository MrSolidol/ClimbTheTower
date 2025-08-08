using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinTrigger : MonoBehaviour
{
    private const string FINAL_SCENE_NAME = "FinalScreen";

    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private MusicController musicController;

    [SerializeField] private float waitToFade = 1f;
    [SerializeField] private float fadeDuration = 1f;
    [Inject] private SceneLoader sceneLoader;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerMovement>().IsNegativeGravity = true;
            musicController.RemoveMusic(fadeDuration/4f);
            Invoke("MoveToFinal", waitToFade);
        }
    }

    private void MoveToFinal() 
    {
        sceneLoader.fader.FadeDuration = fadeDuration;
        sceneLoader.LoadSceneWithFade(FINAL_SCENE_NAME);
    }
}
