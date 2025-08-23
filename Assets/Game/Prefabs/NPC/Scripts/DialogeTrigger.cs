using UnityEngine;
using UnityEngine.Events;

public class DialogeTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent eDialogeStarted;

    [SerializeField] private DialogeWriter dialogeWriter;
    [SerializeField] private float dialogeCooldown;

    private bool isReady = true;


    private void OnEnable()
    {
        dialogeWriter.eDialogeEnded.AddListener(OnDialogeEnded);
    }

    private void OnDisable()
    {
        dialogeWriter.eDialogeEnded.RemoveListener(OnDialogeEnded);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isReady && collision.gameObject.CompareTag("Player"))
        {
            eDialogeStarted?.Invoke();
            isReady = false;
        }
    }


    private void OnDialogeEnded() 
    {
        Invoke("SetToReady", dialogeCooldown);
    }

    private void SetToReady() 
    {
        isReady = true;
    }

}
