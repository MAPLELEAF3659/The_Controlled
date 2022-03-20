using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public AudioSource bgmPlayer;
    public AudioClip bgmAudio;
    public GameObject soundPlayer;
    public float bgmTargetVolume,bgmFadeInSpeed;

    void Start()
    {
        bgmPlayer.volume = 0;
        if (bgmAudio)
        {
            bgmPlayer.clip = bgmAudio;
            bgmPlayer.Play();
            StartCoroutine(FadeInBGM(bgmTargetVolume,0.01f, bgmFadeInSpeed));
        }
    }

    public void CreateSound(AudioClip clip)
    {
        soundPlayer.GetComponent<AudioSource>().clip = clip;
        Instantiate(soundPlayer);
        //Destroy(soundPlayer,2f);
    }

    public IEnumerator FadeInBGM(float targetVolume,float addPerWait, float speed)
    {
        while (bgmPlayer.volume < targetVolume)
        {
            bgmPlayer.volume += addPerWait;
            yield return new WaitForSeconds(speed);
        }
        bgmPlayer.volume = targetVolume;
    }
}
