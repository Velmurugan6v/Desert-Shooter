using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioClip[] _sfxClips;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayerFX(int sfxNo)
    {
        _audioSource.clip = _sfxClips[sfxNo];
        _audioSource.Play();
        print("PlaySFX");
    }

    public void EnemyFx(int sfxNo)
    {
        _enemyAudioSource.clip = _sfxClips[sfxNo];
        print("EnemySFX");
    }
}
