using UnityEngine;


[CreateAssetMenu(fileName = "MusicInfo", menuName = "Gameplay/Music Info")]
public class MusicInfo : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _playIndex;
    [SerializeField] private float _maxVolume;

    public AudioClip AudioClip { get { return _audioClip; } }
    public int PlayIndex { get { return _playIndex; } }
    public float MaxVolume { get { return _maxVolume; } }

}
