using UnityEngine;

public class DialogeSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private DialogeWriter dialogeWriter;
    [SerializeField] private float pitchRadius;

    private float idlePitch;


    private void Awake()
    {
        idlePitch = audioSource.pitch;
    }

    private void OnEnable()
    {
        dialogeWriter.eCharacterWrited.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        dialogeWriter.eCharacterWrited.RemoveListener(PlaySound);
    }


    private void PlaySound() 
    {
        audioSource.pitch = idlePitch + Random.Range(0, pitchRadius) * ( (Random.Range(0, 2) * 2) - 1 );
        audioSource.Play();
    }
}
