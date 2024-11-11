using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip click;
    [SerializeField]
    private AudioClip death;
    [SerializeField]
    private AudioClip sigilCompletion;
    [SerializeField]
    private AudioClip sigilFormation;
    [SerializeField]
    private AudioClip spellCollect;
    [SerializeField]
    private AudioClip playerHit;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSource.clip = click;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayDeath()
    {
        audioSource.clip = death;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void PlaySigilCompletion()
    {
        audioSource.clip = sigilCompletion;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void PlaySigilFormation()
    {
        audioSource.clip = sigilFormation;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void StopSigilFormation()
    {
        audioSource.Stop();
    }

    public void PlaySpellCollect()
    {
        audioSource.clip = spellCollect;
        audioSource.volume = 0.25f;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void PlayPlayerHit()
    {
        audioSource.clip = playerHit;
        audioSource.volume = 0.25f;
        audioSource.loop = false;
        audioSource.Play();
    }
}
