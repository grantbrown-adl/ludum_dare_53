using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _hurt;
    [SerializeField] private AudioClip[] _fire;
    [SerializeField] private AudioClip[] _bubble;
    [SerializeField] private AudioClip[] _claws;
    [SerializeField] private AudioClip[] _money;
    [SerializeField] private AudioClip[] _build;
    [SerializeField] private AudioClip[] _death;
    [SerializeField] private AudioClip[] _playerDamage;
    [SerializeField] private AudioClip[] _buy;
    [SerializeField] private AudioClip[] _waveStart;

    private static SoundManager _instance;

    public static SoundManager Instance { get => _instance; private set => _instance = value; }
    public AudioClip[] Hurt { get => _hurt; set => _hurt = value; }
    public AudioClip[] Fire { get => _fire; set => _fire = value; }
    public AudioClip[] Bubble { get => _bubble; set => _bubble = value; }
    public AudioClip[] Money { get => _money; set => _money = value; }
    public AudioClip[] Build { get => _build; set => _build = value; }
    public AudioClip[] Death { get => _death; set => _death = value; }
    public AudioClip[] Buy { get => _buy; set => _buy = value; }
    public AudioClip[] WaveStart { get => _waveStart; set => _waveStart = value; }
    public AudioClip[] PlayerDamage { get => _playerDamage; set => _playerDamage = value; }
    public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
    public AudioClip[] Claws { get => _claws; set => _claws = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }

    public void PlayClip(AudioClip[] clips)
    {
        int index = Random.Range(0, clips.Length);
        AudioClip clip = clips[index];
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
