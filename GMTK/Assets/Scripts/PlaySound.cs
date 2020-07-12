using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Audio Settings")]
    public float volume;
    public float pitch;

    [Header("UI")]
    public AudioClip clickSFX;

    [Header("Movement")]
    public AudioClip jumpSFX;
    public AudioClip dashSFX;

    [Header("Combat")]
    public AudioClip attackSFX;
    public AudioClip collisionSFX;
    public AudioClip hurtSFX;
    public AudioClip deathSFX;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ui
    public void PlayClickSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(clickSFX);
    }

    // movement
    public void PlayJumpSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(jumpSFX);
    }

    public void PlayDashSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(dashSFX);
    }

    // combat
    public void PlayAttackSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(attackSFX);
    }

    public void PlayCollisionSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(collisionSFX);
    }

    public void PlayHurtSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(hurtSFX);
    }

    public void PlayDeathSFX()
    {
        SetupAudioSource();
        audioSource.PlayOneShot(deathSFX);
    }

    // setup
    void SetupAudioSource()
    {
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(pitch-0.05f, pitch+0.05f);
    }
}
