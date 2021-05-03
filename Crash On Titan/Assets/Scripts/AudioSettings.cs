using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    public AudioSource m_AudioSource;
    public bool m_IsPlaying;


    // Start is called before the first frame update
    private void Start()
    {
        m_IsPlaying = true;
    }

    public void Toggle()
    {
        if(m_IsPlaying)
        {
        m_IsPlaying = !m_IsPlaying;
            Mute();
        }
        else
        {
            m_IsPlaying = true;
            UnMute();
        }
    }

    public void Mute()
    {
        m_AudioSource.Pause();
    }

    public void UnMute()
    {
        m_AudioSource.UnPause();
    }
}
