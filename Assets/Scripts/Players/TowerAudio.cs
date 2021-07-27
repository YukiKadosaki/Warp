using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
    }

    //AudioChangerÇ…ÉvÉåÉCÉÑÅ[Ç™êGÇÍÇÈÇ∆åƒÇŒÇÍÇÈ
    public void AudioChange(AudioClip clip, float volume)
    {
        if(m_AudioSource.clip != clip)
        {
            m_AudioSource.clip = clip;
            m_AudioSource.volume = volume;
            m_AudioSource.Play();
        }
    }

    public void AudioChange()
    {
        m_AudioSource.Stop();
    }
}
