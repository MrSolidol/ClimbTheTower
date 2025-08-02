using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private float randomScale = 2;

    private float savedPitch;

    private void Start()
    {
        savedPitch = audio.pitch;
    }

    public void PlaySound() 
    {
        audio.pitch = Mathf.Clamp(savedPitch + randomScale * UnityEngine.Random.Range(-0.1f, 0.1f), -3, 3);
        audio.Play();
    }

}
