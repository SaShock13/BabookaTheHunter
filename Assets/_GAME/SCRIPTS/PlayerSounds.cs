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


    void Start()
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

    public void Step()
    {
        if (stepSound != null)
        {
            if (audioSource.isPlaying) return;            
                //audioSource.clip = stepSound;
                //audioSource.Play(); 
        }
    }
}
