using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource[] BackGroundMusic;
    public AudioSource[] OneShotMusic;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayBackgroundMusic(SoundList sound)
    {
        if (sound == SoundList.BGMainMenu)
        {
            BackGroundMusic[0].Play();
        }
        else if (sound == SoundList.BGInGame)
        {
            BackGroundMusic[1].Play();
        }
    }

    public void PlayOneShot(SoundList sound)
    {
        if (sound == SoundList.Shot)
        {
            OneShotMusic[0].Play();
        }
        else if (sound == SoundList.Dead)
        {
            OneShotMusic[1].Play();
        }
        else if (sound == SoundList.Win)
        {
            OneShotMusic[2].Play();
        }
    }
}

public enum SoundList
{
    BGMainMenu,
    BGInGame,
    Shot,
    Dead,
    Win
}