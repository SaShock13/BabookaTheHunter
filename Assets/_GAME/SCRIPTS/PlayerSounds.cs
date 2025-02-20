using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip jumpInit;
    [SerializeField] private AudioClip jumpLanding;
    [SerializeField] private AudioClip hearted;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip clubSwish;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayJumpInitSound()
    {
        audioSource.Stop();
        audioSource.clip = jumpInit;
        audioSource.Play();
    }

    public void PlayJumpLandingSound()
    {
        audioSource.clip = jumpLanding;
        audioSource.Play();
    }

    public void PlaySwish()
    {
        audioSource.Stop();
        audioSource.clip = clubSwish;
        audioSource.Play();
    }

    public void PlayDeath()
    {
        audioSource.Stop();
        audioSource.clip = death;
        audioSource.Play();
    }
    public void PlayHearted()
    {
        audioSource.Stop();
        audioSource.clip = hearted;
        audioSource.Play();
    }

    public void Step()
    {
        if (stepSound != null)
        {
            if (audioSource.isPlaying) return;
            audioSource.clip = stepSound;
            audioSource.Play();
        }
    }

}
