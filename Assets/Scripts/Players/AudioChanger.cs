using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    const float normalStage = 0.2f;
    const float lastStage = 0.4f;
    const float end = 0.2f;

    public enum AudioChangePlace
    {
        under,
        last,
        no,
        end
    }
    private TowerAudio m_TowerAudio;
    [SerializeField]
    private AudioChangePlace m_AudioChangePlace;



    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        m_TowerAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TowerAudio>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (m_AudioChangePlace)
            {
                case AudioChangePlace.under:
                    AudioClip clip1 = Resources.Load<AudioClip>("Audio/BGM/normalstage");
                    m_TowerAudio.AudioChange(clip1, normalStage);
                    break;
                case AudioChangePlace.last:
                    AudioClip clip2 = Resources.Load<AudioClip>("Audio/BGM/laststage");
                    m_TowerAudio.AudioChange(clip2, lastStage);
                    break;
                case AudioChangePlace.no:
                    m_TowerAudio.AudioChange();
                    break;
                case AudioChangePlace.end:
                    AudioClip clip3 = Resources.Load<AudioClip>("Audio/BGM/end");
                    m_TowerAudio.AudioChange(clip3, end);
                    break;
            }


        }
    }

}
