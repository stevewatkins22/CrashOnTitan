using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    public AudioSource m_AudioSource;
    public bool m_IsPlaying;
    public float m_SliderValue;
    // Start is called before the first frame update
    void Start()
    {
        m_SliderValue = 0.5f;

       m_AudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        if(!m_IsPlaying)
        {
            m_IsPlaying = true;
            m_AudioSource.mute = !m_AudioSource.mute;
        }
        else
        {
            m_IsPlaying = false;
            m_AudioSource.mute = m_AudioSource.mute;
        }
    }
}
