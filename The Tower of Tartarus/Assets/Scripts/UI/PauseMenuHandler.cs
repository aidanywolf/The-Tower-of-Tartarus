using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{

    public AudioSource musicAudioSource; 
    public AudioSource sfxAudioSource;
    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value;
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }
    public void Quit(){
        Application.Quit();
    }

}
