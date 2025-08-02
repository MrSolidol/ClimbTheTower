using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMove;

    [SerializeField] private AudioSource StoneSlabSound;
    [SerializeField] private AudioSource WoodSlabSound;
    [SerializeField] private AudioSource MetalSlabSound;
    [SerializeField] private AudioSource WebbSplitSound;

    private Dictionary<string, AudioSource> soundsMap;

    private void Awake()
    {
        soundsMap = new Dictionary<string, AudioSource>()
        {
            { "STONE", StoneSlabSound },
            { "WOOD", WoodSlabSound },
            { "METAL", MetalSlabSound },
            { "WEBB", WebbSplitSound }
        };
    }

    private void OnEnable()
    {
        playerMove.ePlaySound.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        playerMove.ePlaySound.RemoveListener(PlaySound);
    }

    private void PlaySound(string name) 
    {
        var sound = soundsMap[name];
        sound.pitch = 1.1f + Random.Range(0f, .3f) * ((Random.Range(0, 2) * 2) - 1);
        sound.Play();
    }
}
