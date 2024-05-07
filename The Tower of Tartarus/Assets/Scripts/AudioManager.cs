using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip boulderRoll;
    public AudioClip itemPickup;
    public AudioClip playerDeath;
    public AudioClip enemyDeath;
    public AudioClip song;

    private void Start(){
        musicSource.clip = song;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
