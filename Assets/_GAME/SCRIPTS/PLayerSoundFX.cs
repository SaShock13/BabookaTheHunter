using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PLayerSoundFX : MonoBehaviour
{
    [SerializeField] private AudioClip jumpInit;
    [SerializeField] private AudioClip jumpLanding;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayJumpInitSound()
    {
        audioSource.clip = jumpInit;
        audioSource.Play();
    }

    public void PlayJumpLandingSound()
    {
        audioSource.clip = jumpLanding;
        audioSource.Play();
    }
}

