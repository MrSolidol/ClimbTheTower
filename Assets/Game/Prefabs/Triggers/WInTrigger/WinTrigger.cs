using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinTrigger : MonoBehaviour
{
    private const string FINAL_SCENE_NAME = "FinalScreen";

    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private float timeToMove = 1f;
    [Inject] private SceneLoader sceneLoader;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerMovement>().IsNegativeGravity = true;
            Invoke("MoveToFinal", timeToMove);
        }
    }

    private void MoveToFinal() 
    {
        sceneLoader.LoadSceneWithFade(FINAL_SCENE_NAME);
    }
}
